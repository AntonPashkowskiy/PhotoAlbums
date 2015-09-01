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
			return DataService.GetAlbums(CurrentUserId)
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDTO());
		}

		[Route("api/albums/tag/{tag}")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDTO> GetAlbumsByTag(string tag)
		{
			return DataService.GetAlbums(new AlbumTag() { TagName = tag })
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDTO());
		}

		[Route("api/albums/name/{fullName}")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDTO> GetAlbumsByName(string fullName)
		{
			return DataService.GetAlbumsByName(fullName)
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDTO());
		}

		[Route("api/albums/update")]
		[HttpPut]
		public void UpdateAlbum(PhotoAlbumDTO album)
		{
			bool isOwnerOfAlbum = DataService.CheckPossibilityOfChangingAlbum(CurrentUserId, album.Id);

			if (isOwnerOfAlbum)
			{
				DataService.UpdateAlbum(album.ToPhotoAlbum());
			}
		}

		[Route("api/albums/{albumId}/delete")]
		[HttpDelete]
		public void DeleteAlbum(int albumId)
		{
			bool isOwnerOfAlbum = DataService.CheckPossibilityOfChangingAlbum(CurrentUserId, albumId);

			if (isOwnerOfAlbum)
			{
				DataService.DeleteAlbum(albumId);
			}
		}
    }
}
