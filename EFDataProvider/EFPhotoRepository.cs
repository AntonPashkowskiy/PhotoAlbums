using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class EFPhotoRepository : IPhotoRepository
	{
		private EFPhotoRepository() { }
		
		private readonly PhotoAlbumsContext _dbContext = null;

		public EFPhotoRepository(string connectionString)
		{
			if (String.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentNullException("Connection string can't be null.");
			}
			_dbContext = new PhotoAlbumsContext(connectionString);
		}

		public bool AddPhoto(Photo photo)
		{
			_dbContext.Photo.Add(photo);
			_dbContext.SaveChanges();

			return true;
		}

		public bool AddComment(PhotoComment comment)
		{
			_dbContext.CommentsOfPhotos.Add(comment);
			_dbContext.SaveChanges();

			return true;
		}

		public int CountPhotosByUser(int userId)
		{
			return _dbContext.Photo.Count(p => p.AuthorId == userId);
		}

		public int CountPhotosInAlbum(int albumId)
		{
			return _dbContext.Photo.Count(p => p.AlbumId == albumId);
		}

		public bool DeleteComment(PhotoComment comment)
		{
			PhotoComment commentFound = _dbContext.CommentsOfPhotos
				.Where(c => c.AuthorId == comment.AuthorId && c.Id == comment.Id)
				.FirstOrDefault();

			if (commentFound != null)
			{
				_dbContext.Entry(commentFound).State = EntityState.Deleted;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public bool DeletePhoto(int photoId)
		{
			Photo photoFound = _dbContext.Photo.Find(photoId);

			if (photoFound != null)
			{
				_dbContext.Entry(photoFound).State = EntityState.Deleted;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public Photo GetPhoto(int photoId)
		{
			return _dbContext.Photo.Find(photoId);
		}

		public IEnumerable<Photo> GetPhotos(int userId)
		{
			return _dbContext.Photo.Where(p => p.AuthorId == userId);
		}

		public IEnumerable<Photo> GetPhotosFromAlbum(int albumId)
		{
			return _dbContext.Photo.Where(p => p.AlbumId == albumId);
		}

		public IEnumerable<PhotoComment> GetComments(int photoId)
		{
			return _dbContext.CommentsOfPhotos.Where(c => c.PhotoId == photoId);
		}

		public int OverallRatingForPhotos(int userId)
		{
			return _dbContext.Photo
				.Where(p => p.AuthorId == userId)
				.Select(p => p.Rating)
				.Aggregate(0, (s, i) => s + i);
		}

		public bool UpdateComment(PhotoComment comment)
		{
			PhotoComment commentFound = _dbContext.CommentsOfPhotos
				.Where(c => c.AuthorId == comment.AuthorId && c.Id == comment.Id)
				.FirstOrDefault();

			if (commentFound != null)
			{
				commentFound.CommentText = comment.CommentText ?? commentFound.CommentText;
				_dbContext.Entry(commentFound).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public bool UpdatePhoto(Photo photo)
		{
			Photo photoFound = _dbContext.Photo.Find(photo.Id);

			if (photoFound != null)
			{
				photoFound.Description = photo.Description ?? photoFound.Description;
				photoFound.Rating = photo.Rating;
				_dbContext.Entry(photoFound).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
