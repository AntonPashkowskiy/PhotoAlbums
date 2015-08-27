using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using Memento.DTO;
using ServiceLayer;

namespace Memento.Controllers.API
{
	[Authorize]
    public class AlbumsApiController : BaseApiController
    {
		public AlbumsApiController(IDataService dataService) : base(dataService)
		{
		}

		[Route("api/albums/own")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDTO> GetAlbums()
		{
			return DataService.GetAlbums(CurrentUserId).Select(a => a.ToPhotoAlbumDTO());
		}

		[Route("api/albums/tag/{tag}")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDTO> GetAlbumsByTag(string tag)
		{
			return DataService.GetAlbums(new AlbumTag() { TagName = tag })
							  .Select(a => a.ToPhotoAlbumDTO());
		}

		[Route("api/albums/{albumId}/comments")]
		[HttpGet]
		public IEnumerable<AlbumCommentDTO> GetCommentsOfAlbum(int albumId)
		{
			return DataService.GetAlbumComments(albumId).Select(c => c.ToAlbumCommentDTO());
		}

		[Route("api/albums/update")]
		[HttpPut]
		public bool UpdateAlbum(PhotoAlbumDTO album)
		{
			try
			{
				DataService.UpdateAlbum(album.ToPhotoAlbum());
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		[Route("api/albums/comments/update")]
		[HttpPut]
		public bool UpdateComment(AlbumCommentDTO albumComment)
		{
			try
			{
				DataService.UpdateAlbumComment(albumComment.ToAlbumComment());
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		[Route("api/albums/{albumId}/delete")]
		[HttpDelete]
		public bool DeleteAlbum(int albumId)
		{
			bool isOwnerOfAlbum = DataService.CheckPossibilityOfDeletingAlbum(CurrentUserId, albumId);

			if (isOwnerOfAlbum)
			{
				DataService.DeleteAlbum(albumId);
				return true;
			}
			return false;
		}

		[Route("api/albums/comments/{commentId}/delete")]
		[HttpDelete]
		public bool DeleteComment(int commentId)
		{
			bool isOwnerOfResourse = DataService.CheckPossibilityOfDeletingComment(
				CurrentUserId,
				commentId,
				CommentType.AlbumComment);

			if (isOwnerOfResourse)
			{
				DataService.DeleteAlbumComment(commentId);
				return true;
			}
			return false;
		}
    }
}
