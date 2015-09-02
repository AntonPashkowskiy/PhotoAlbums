using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Entities;
using Entities.Entities;
using Memento.DTO;
using Memento.DTO.Mappers;
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
			if (!ModelState.IsValid) return new EmptyResult();

			var album = new PhotoAlbum()
			{
				UserId = CurrentUser.Id,
				Name = model.AlbumName,
				Description = model.AlbumDescription,
				CreationDate = DateTime.Now,
				IsPrivate = model.IsPrivate
			};
			var tags = ParseTagString(model.Tags);
			var information = SaveAlbumInformationInDb(album, tags);

			if (tags != null)
			{
				information.Tags = tags.Select(t => t.TagName).ToArray();
			}
			information.PresentationPhotoUrl = Url.Content("~/Content/Images/default-album-photo.png");

			return Json(information);
		}

		// GET: /Albums/Album/Id
		[HttpGet]
		public ActionResult Album(int? id)
		{
			ViewBag.CurrentUser = CurrentUser;

			var model = new AlbumViewModel() {
				AlbumId = 2,
				AlbumName = "My favorite album.",
				IsAlbumOfUser = true
			};
			return View(model);
		}

		// POST: /Albums/UploadPhoto
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UploadPhoto(HttpPostedFileBase file, UploadingPhotoViewModel model)
		{
			if (file == null)
			{
				return Json("Upload failed.");
			}
			Photo photoInformation = SavePhotoInUserDirectory(file);
			photoInformation.AlbumId = model.AlbumId;
			photoInformation.AuthorId = CurrentUser.Id;
			photoInformation.Description = model.PhotoDescription;
			photoInformation.CreationDate = DateTime.Now;
		
			PhotoDTO information = SavePhotoInformationInDb(photoInformation);
			information.IsEditable = true;

			return Json(information);
		}

		#region Helpers
		private Photo SavePhotoInUserDirectory(HttpPostedFileBase file)
		{
			var smallSize = PhotoManager.GetThumbailSize(PhotoSize.Small);
			var mediumSize = PhotoManager.GetThumbailSize(PhotoSize.Standart);

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

		private PhotoDTO SavePhotoInformationInDb(Photo information)
		{
			try
			{
				information.Id = DataService.CreatePhoto(information);
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
			const string pattern = @"#(\w)+";
			var tagRegex = new Regex(pattern);

			return (from object match 
					in tagRegex.Matches(tagString) 
					select new AlbumTag()
					{
						TagName = match.ToString()
					}).ToList();
		}

		private PhotoAlbumDto SaveAlbumInformationInDb(PhotoAlbum album, IEnumerable<AlbumTag> tags)
		{
			try
			{
				album.Id = DataService.CreateAlbum(album, tags);
				return album.ToPhotoAlbumDto();
			}
			catch (Exception e)
			{
				throw new Exception("Save album failed", e);
			}
		}
		#endregion
	}
}