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
		void NewUser(User item);
		void NewAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags);
		void NewPhoto(Photo item);
		void NewAlbumComment(AlbumComment item);
		void NewPhotoComment(PhotoComment item);

		void UpdateAlbum(PhotoAlbum item);
		void UpdatePhoto(Photo item);
		void UpdateAlbumComment(AlbumComment item);
		void UpdatePhotoComment(PhotoComment item);

		void DeleteAlbum(int albumId);
		void DeletePhoto(int photoId);
		void DeleteAlbumComment(int albumCommentId);
		void DeletePhotoComment(int photoCommentId);

		Photo GetPhoto(int albumId);
		IEnumerable<PhotoAlbum> GetAlbums(string userId);
		IEnumerable<PhotoAlbum> GetAlbums(AlbumTag tag);
		IEnumerable<Photo> GetPhotos(string userId, int albumId);
		IEnumerable<Photo> GetPhotos(string userId, int pageNumber, int pageSize);
		IEnumerable<AlbumComment> GetAlbumComments(string userId, int albumId);
		IEnumerable<PhotoComment> GetPhotoComments(string userId, int photoId);

		UserStatistic GetUserStatistic(string userId);
	}
}
