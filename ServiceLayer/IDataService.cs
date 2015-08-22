using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceLayer
{
	interface IDataService : IDisposable
	{
		Task<bool> NewUser(User item);
		Task<bool> NewAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags);
		Task<bool> NewPhoto(Photo item);
		Task<bool> NewAlbumComment(AlbumComment item);
		Task<bool> NewPhotoComment(PhotoComment item);

		Task<bool> UpdateAlbum(PhotoAlbum item);
		Task<bool> UpdatePhoto(Photo item);
		Task<bool> UpdateAlbumComment(AlbumComment item);
		Task<bool> UpdatePhotoComment(PhotoComment item);

		Task<bool> DeleteAlbum(PhotoAlbum item);
		Task<bool> DeletePhoto(Photo item);
		Task<bool> DeleteAlbumComment(AlbumComment item);
		Task<bool> DeletePhotoComment(PhotoComment item);

		Task<IEnumerable<PhotoAlbum>> GetAlbums(string userId);
		Task<IEnumerable<PhotoAlbum>> GetAlbums(AlbumTag tag);
		Task<IEnumerable<Photo>> GetPhotos(string userId, int albumId);
		Task<IEnumerable<Photo>> GetPhotos(string userId, int pageNumber, int pageSize);
		Task<IEnumerable<AlbumComment>> GetAlbumComments(string userId, int albumId);
		Task<IEnumerable<PhotoComment>> GetPhotoComments(string userId, int photoId);

		Task<UserStatistic> GetUserStatistic(string userId);
	}
}
