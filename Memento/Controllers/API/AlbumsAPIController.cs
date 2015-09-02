using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Entities.Entities;
using Memento.DTO;
using Memento.DTO.Mappers;
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
		public IEnumerable<PhotoAlbumDto> GetAlbums()
		{
			return DataService.GetAlbums(CurrentUserId)
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDto());
		}

		[Route("api/albums/tag/{tag}")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDto> GetAlbumsByTag(string tag)
		{
			return DataService.GetAlbums(new AlbumTag() { TagName = tag })
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDto());
		}

		[Route("api/albums/name/{fullName}")]
		[HttpGet]
		public IEnumerable<PhotoAlbumDto> GetAlbumsByName(string fullName)
		{
			return DataService.GetAlbumsByName(fullName)
							  .Where(a => !a.IsPrivate)
							  .Select(a => a.ToPhotoAlbumDto());
		}

		[Route("api/albums/update")]
		[HttpPut]
		public void UpdateAlbum(PhotoAlbumDto album)
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
