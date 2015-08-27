using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Entities;
using Memento.DTO;
using Memento.Environment.DataManagement;
using Memento.Models;
using ServiceLayer;

namespace Memento.Controllers
{
	[Authorize]
    public class AlbumsController : BaseController
    {
		public AlbumsController(IDataService dataService) : base(dataService) {}

		// POST: /Manage/CreateAlbum
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateAlbum(CreateAlbumViewModel model)
		{
			if (ModelState.IsValid)
			{
				PhotoAlbum album = new PhotoAlbum()
				{
					UserId = CurrentUser.Id,
					Name = model.AlbumName,
					Description = model.AlbumDescription,
					CreationDate = DateTime.Now,
					IsPrivate = model.IsPrivate
				};
				var tags = ParseTagString(model.Tags);
				var information = SaveAlbumInformationInDB(album, tags);

				if (tags != null)
				{
					information.Tags = tags.Select(t => t.TagName).ToArray();
				}
				information.PresentationPhotoUrl = Url.Content("~/Content/Images/default-album-photo.png");

				return Json(information);
			}
			return new EmptyResult();
		}

		// GET: /Albums/Album/Id
		[HttpGet]
		public ActionResult Album(int? id)
		{
			ViewBag.CurrentUser = CurrentUser;

			AlbumViewModel model = new AlbumViewModel() {
				AlbumId = 2,
				AlbumName = "My favorite album.",
				IsAlbumOfUser = true
			};
			return View(model);
		}

		// POST: /Albums/UploadPhoto
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UploadPhoto(HttpPostedFileBase file, int albumId)
		{
			if (file == null)
			{
				return Json("Upload failed.");
			}
			Photo photoInformation = SavePhotoInUserDirectory(file);
			photoInformation.AlbumId = albumId;
			photoInformation.AuthorId = CurrentUser.Id;
			photoInformation.CreationDate = DateTime.Now;
		
			PhotoDTO information = SavePhotoInformationInDB(photoInformation);
			information.IsEditable = true;

			return Json(information);
		}
		
		// Restructuring need. Violated the principle of DRY.
		// Ajax POST: /Albums/AddCommentToAlbum
		[HttpPost]
		public ActionResult AddCommentToAlbum(AlbumCommentDTO albumComment)
		{
			if (albumComment.CommentText == null)
			{
				return new EmptyResult();
			}
			var comment = albumComment.ToAlbumComment();
			comment.AuthorId = CurrentUser.Id;
			var result = SaveAlbumCommentInDB(comment);
			result.AuthorFullName = CurrentUser.FirstName + " " + CurrentUser.LastName;
			result.AuthorEmail = CurrentUser.Email;

			return Json(result);
		}

		// Restructuring need. Violated the principle of DRY.
		// Ajax POST: /Albums/AddCommentToPhoto
		[HttpPost]
		public ActionResult AddCommentToPhoto(PhotoCommentDTO photoComment)
		{
			if (photoComment.CommentText == null)
			{
				return new EmptyResult();
			}
			var comment = photoComment.ToPhotoComment();
			comment.AuthorId = CurrentUser.Id;
			var result = SavePhotoCommentInDB(comment);
			result.AuthorFullName = CurrentUser.FirstName + " " + CurrentUser.LastName;
			result.AuthorEmail = CurrentUser.Email;

			return Json(result);
		}

		#region Helpers
		private Photo SavePhotoInUserDirectory(HttpPostedFileBase file)
		{
			Size smallSize = PhotoManager.GetThumbailSize(PhotoSize.Small);
			Size mediumSize = PhotoManager.GetThumbailSize(PhotoSize.Standart);

			string extention = Path.GetExtension(file.FileName);
			ServerPath smallPhotoPath = HttpContext.GetFilePath(DirectoryType.PhotoDirectory, CurrentUser.UserName, extention);
			ServerPath mediumPhotoPath = HttpContext.GetFilePath(DirectoryType.PhotoDirectory, CurrentUser.UserName, extention);
			ServerPath fullPhotoPath = HttpContext.GetFilePath(DirectoryType.PhotoDirectory, CurrentUser.UserName, extention);

			using (var stream = new MemoryStream())
			{
				file.InputStream.CopyTo(stream);
				Image fullPhoto = Image.FromStream(stream);
				Image mediumPhoto = fullPhoto.GetThumbnailImage(mediumSize.Width, mediumSize.Height, null, IntPtr.Zero);
				Image smallPhoto = fullPhoto.GetThumbnailImage(smallSize.Width, smallSize.Height, null, IntPtr.Zero);

				fullPhoto.Save(fullPhotoPath.AbsolutePath);
				mediumPhoto.Save(mediumPhotoPath.AbsolutePath);
				smallPhoto.Save(smallPhotoPath.AbsolutePath);
			}

			return new Photo()
			{
				SmallPhotoUrl = smallPhotoPath.LocalPath,
				MediumPhotoUrl = mediumPhotoPath.LocalPath,
				FullPhotoUrl = fullPhotoPath.LocalPath
			};
		}

		private PhotoDTO SavePhotoInformationInDB(Photo information)
		{
			try
			{
				int photoId = DataService.CreatePhoto(information);
				information.Id = photoId;
				return information.ToPhotoDTO();
			}
			catch (Exception e)
			{
				string smallPhotoPath = HttpContext.Server.MapPath(information.SmallPhotoUrl);
				string mediumPhotoPath = HttpContext.Server.MapPath(information.MediumPhotoUrl);
				string fullPhotoPath = HttpContext.Server.MapPath(information.FullPhotoUrl);

				System.IO.File.Delete(smallPhotoPath);
				System.IO.File.Delete(mediumPhotoPath);
				System.IO.File.Delete(fullPhotoPath);

				throw new Exception("Save photo failed.", e);
			}
		}

		private IEnumerable<AlbumTag> ParseTagString(string tagString)
		{
			if (tagString == null)
			{
				return null;
			}
			string pattern = @"#(\w)+";
			Regex tagRegex = new Regex(pattern);
			var result = new List<AlbumTag>();
			
			foreach (var match in tagRegex.Matches(tagString))
			{
				result.Add(new AlbumTag() { TagName = match.ToString() });
			}
			return result;
		}

		private PhotoAlbumDTO SaveAlbumInformationInDB(PhotoAlbum album, IEnumerable<AlbumTag> tags)
		{
			try
			{
				album.Id = DataService.CreateAlbum(album, tags);
				return album.ToPhotoAlbumDTO();
			}
			catch (Exception e)
			{
				throw new Exception("Save album failed", e);
			}
		}

		private AlbumCommentDTO SaveAlbumCommentInDB(AlbumComment comment)
		{
			try
			{
				comment.Id = DataService.CreateAlbumComment(comment);
				return comment.ToAlbumCommentDTO();
			}
			catch (Exception e)
			{
				throw new Exception("Save comment failed.", e);
			}
		}

		private PhotoCommentDTO SavePhotoCommentInDB(PhotoComment comment)
		{
			try
			{
				comment.Id = DataService.CreatePhotoComment(comment);
				return comment.ToPhotoCommentDTO();
			}
			catch (Exception e)
			{
				throw new Exception("Save comment failed.", e);
			}
		}
		#endregion
	}
}