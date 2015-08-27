using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Interfaces;

namespace ServiceLayer
{
	public class DataService : IDataService
	{
		private IUnitOfWork _unitOfWork = null;

		private DataService() {}

		public DataService(IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null)
			{
				throw new ArgumentNullException("Unit of work can't be null.");
			}
			_unitOfWork = unitOfWork;
		}

		public void NewUser(User item)
		{
			_unitOfWork.Users.Add(item);
			_unitOfWork.Save();
		}

		public int CreateAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags)
		{
			_unitOfWork.Albums.AddAlbum(item, tags);
			_unitOfWork.Save();

			var album = _unitOfWork.Albums
				.Find(a => a.CreationDate == item.CreationDate && a.UserId == item.UserId)
				.First();
			return album.Id;
		}

		public int CreatePhoto(Photo item)
		{
			_unitOfWork.Photo.Add(item);
			_unitOfWork.Save();

			var photo = _unitOfWork.Photo
				.Find(p => p.CreationDate == item.CreationDate && p.AuthorId == item.AuthorId)
				.First();
			return photo.Id;
		}

		public int CreateAlbumComment(AlbumComment item)
		{
			_unitOfWork.AlbumComments.Add(item);
			_unitOfWork.Save();

			var albumComment = _unitOfWork.AlbumComments
				.Find(c => c.CreationDate == item.CreationDate && c.AuthorId == item.AuthorId)
				.First();
			return albumComment.Id;
		}

		public int CreatePhotoComment(PhotoComment item)
		{
			_unitOfWork.PhotoComments.Add(item);
			_unitOfWork.Save();

			var photoComment = _unitOfWork.PhotoComments
				.Find(c => c.CreationDate == item.CreationDate && c.AuthorId == item.AuthorId)
				.First();
			return photoComment.Id;
		}

		public void UpdateAlbum(PhotoAlbum item)
		{
			_unitOfWork.Albums.Update(item);
			_unitOfWork.Save();
		}

		public void UpdatePhoto(Photo item)
		{
			_unitOfWork.Photo.Update(item);
			_unitOfWork.Save();
		}

		public void UpdateAlbumComment(AlbumComment item)
		{
			_unitOfWork.AlbumComments.Update(item);
			_unitOfWork.Save();
		}

		public void UpdatePhotoComment(PhotoComment item)
		{
			_unitOfWork.PhotoComments.Update(item);
			_unitOfWork.Save();
		}

		public bool CheckPossibilityOfChangingComment(string userId, int commentId, CommentType type)
		{
			switch (type)
			{
				case CommentType.AlbumComment:
					var albumComment = _unitOfWork.AlbumComments.Get(commentId);
					return albumComment != null ? albumComment.TargetAlbum.UserId == userId : false;

				case CommentType.PhotoComment:
				default:
					var photoComment = _unitOfWork.PhotoComments.Get(commentId);
					return photoComment != null ? photoComment.TargetPhoto.AuthorId == userId : false;
			}
		}

		public bool CheckPossibilityOfChangingPhoto(string userId, int photoId)
		{
			var photo = _unitOfWork.Photo.Get(photoId);
			return photo != null ? photo.AuthorId == userId : false;
		}

		public bool CheckPossibilityOfChangingAlbum(string userId, int albumId)
		{
			var album = _unitOfWork.Albums.Get(albumId);
			return album != null ? album.UserId == userId : false;
		}

		public void DeleteAlbum(int albumId)
		{
			_unitOfWork.Albums.Delete(albumId);
			_unitOfWork.Save();
		}

		public void DeletePhoto(int photoId)
		{
			_unitOfWork.Photo.Delete(photoId);
			_unitOfWork.Save();
		}

		public void DeleteAlbumComment(int albumCommentId)
		{
			_unitOfWork.AlbumComments.Delete(albumCommentId);
			_unitOfWork.Save();
		}

		public void DeletePhotoComment(int photoCommentId)
		{
			_unitOfWork.PhotoComments.Delete(photoCommentId);
			_unitOfWork.Save();
		}

		public Photo GetPhoto(int albumId)
		{
			return _unitOfWork.Photo.Find(p => p.AlbumId == albumId).FirstOrDefault();
		}

		public IEnumerable<PhotoAlbum> GetAlbums(string userId)
		{
			return _unitOfWork.Albums.Find(a => a.UserId == userId);
		}

		public IEnumerable<PhotoAlbum> GetAlbums(AlbumTag tag)
		{
			return _unitOfWork.Albums.GetAlbumsByTag(tag); 
		}

		public IEnumerable<Photo> GetPhotos(string userId, int albumId)
		{
			return _unitOfWork.Photo.Find(p => p.AuthorId == userId && p.AlbumId == albumId);
		}

		public IEnumerable<Photo> GetPhotos(string userId, int pageNumber, int pageSize)
		{
			return _unitOfWork.Photo.GetPhotos(userId, pageNumber, pageSize);
		}

		public IEnumerable<AlbumComment> GetAlbumComments(int albumId)
		{
			return _unitOfWork.AlbumComments.Find(c => c.AlbumId == albumId);
		}

		public IEnumerable<PhotoComment> GetPhotoComments(int photoId)
		{
			return _unitOfWork.PhotoComments.Find(c => c.PhotoId == photoId);
		}

		public UserStatistic GetUserStatistic(string userId)
		{
			return new UserStatistic()
			{
				NumberOfAlbums = _unitOfWork.Albums.NumberOfAlbums(userId),
				NumberOfPhotos = _unitOfWork.Photo.NumberOfPhotos(userId),
				OverallRatingOfAlbums = _unitOfWork.Albums.OverallRatingForAlbums(userId),
				OverallRatingOfPhotos = _unitOfWork.Photo.OverallRatingForPhotos(userId)
			};
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}
	}
}
