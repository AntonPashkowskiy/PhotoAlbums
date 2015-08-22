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

		public void NewAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags)
		{
			_unitOfWork.Albums.AddAlbum(item, tags);
			_unitOfWork.Save();
		}

		public void NewPhoto(Photo item)
		{
			_unitOfWork.Photo.Add(item);
			_unitOfWork.Save();
		}

		public void NewAlbumComment(AlbumComment item)
		{
			_unitOfWork.AlbumComments.Add(item);
			_unitOfWork.Save();
		}

		public void NewPhotoComment(PhotoComment item)
		{
			_unitOfWork.PhotoComments.Add(item);
			_unitOfWork.Save();
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

		public IEnumerable<AlbumComment> GetAlbumComments(string userId, int albumId)
		{
			return _unitOfWork.AlbumComments.Find(c => c.AuthorId == userId && c.AlbumId == albumId);
		}

		public IEnumerable<PhotoComment> GetPhotoComments(string userId, int photoId)
		{
			return _unitOfWork.PhotoComments.Find(c => c.AuthorId == userId && c.PhotoId == photoId);
		}

		public UserStatistic GetUserStatistic(string userId)
		{
			UserStatistic statistic = new UserStatistic();
			statistic.NumberOfAlbums = _unitOfWork.Albums.NumberOfAlbums(userId);
			statistic.NumberOfPhotos = _unitOfWork.Photo.NumberOfPhotos(userId);
			statistic.OverallRatingOfAlbums = _unitOfWork.Albums.OverallRatingForAlbums(userId);
			statistic.OverallRatingOfPhotos = _unitOfWork.Photo.OverallRatingForPhotos(userId);

			return statistic;
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}
	}
}
