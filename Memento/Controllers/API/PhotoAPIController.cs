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
    public class PhotoApiController : BaseApiController
    {
		public PhotoApiController(IDataService dataService) : base(dataService)
		{
		}

		[Route("api/photos/own/{pageNumber}/{pageCount}")]
		[HttpGet]
		public IEnumerable<PhotoDTO> GetPhotos(int pageNumber, int pageSize)
		{
			return DataService.GetPhotos(CurrentUserId, pageNumber, pageSize).Select(p => p.ToPhotoDTO());
		}

		[Route("api/photos/{albumId}")]
		[HttpGet]
		public IEnumerable<PhotoDTO> GetPhotos(int albumId)
		{
			return DataService.GetPhotos(CurrentUserId, albumId).Select(p => p.ToPhotoDTO());
		}

		[Route("api/photos/update")]
		[HttpPut]
		public bool UpdatePhoto(PhotoDTO photo)
		{
			bool isOwnerOfPhoto = DataService.CheckPossibilityOfChangingPhoto(CurrentUserId, photo.Id);

			if (isOwnerOfPhoto)
			{
				DataService.UpdatePhoto(photo.ToPhoto());
				return true;
			}
			return false;
		}

		[Route("api/photos/{photoId}/delete")]
		[HttpDelete]
		public bool DeletePhoto(int photoId)
		{
			bool isOwnerOfPhoto = DataService.CheckPossibilityOfChangingPhoto(CurrentUserId, photoId);

			if (isOwnerOfPhoto)
			{
				DataService.DeletePhoto(photoId);
				return true;
			}
			return false;
		}
    }
}
