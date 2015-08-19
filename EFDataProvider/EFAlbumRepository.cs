using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class EFAlbumRepository : IAlbumRepository
	{
		private EFAlbumRepository() { }

		private readonly PhotoAlbumsContext _dbContext = null;

		public EFAlbumRepository(string connectionString)
		{
			if (String.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentNullException("Connection string can't be null.");
			}
			_dbContext = new PhotoAlbumsContext(connectionString);
		}

		public bool AddAlbum(PhotoAlbum album)
		{
			_dbContext.PhotoAlbums.Add(album);
			_dbContext.SaveChanges();

			return true;
		}

		public bool AddComment(AlbumComment comment)
		{
			_dbContext.CommentsOfAlbums.Add(comment);
			_dbContext.SaveChanges();

			return true;
		}

		public bool AddTag(AlbumTag tag)
		{
			_dbContext.AlbumTags.Add(tag);
			_dbContext.SaveChanges();

			return true;
		}

		public bool AddTagToAlbum(PhotoAlbum album, AlbumTag tag)
		{
			AlbumTag tagFound = _dbContext.AlbumTags
				.Where(t => t.TagName == tag.TagName)
				.FirstOrDefault();
			PhotoAlbum albumFound = _dbContext.PhotoAlbums.Find(album.Id);

			if (albumFound == null)
			{
				return false;
			}

			if (tagFound == null)
			{
				_dbContext.AlbumTags.Add(tag);
				tagFound = tag;
			}

			albumFound.Tags.Add(tagFound);
			_dbContext.Entry(albumFound).State = EntityState.Modified;
			_dbContext.SaveChanges();

			return true;
		}

		public int CountAlbums(int userId)
		{
			return _dbContext.PhotoAlbums.Count(a => a.UserId == userId);
		}

		public bool DeleteAlbum(int albumId)
		{
			PhotoAlbum album = _dbContext.PhotoAlbums.Find(albumId);

			if (album != null)
			{
				_dbContext.Entry(album).State = EntityState.Deleted;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public bool DeleteComment(AlbumComment comment)
		{
			AlbumComment commentFound = _dbContext.CommentsOfAlbums
				.Where(c => c.Id == comment.Id && c.AlbumId == comment.AlbumId)
				.FirstOrDefault();

			if (comment != null)
			{
				_dbContext.Entry(comment).State = EntityState.Deleted;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public PhotoAlbum GetAlbum(string albumName, int userId)
		{
			return _dbContext.PhotoAlbums
				.Where(a => a.Name == albumName && a.UserId == userId)
				.FirstOrDefault();
		}

		public PhotoAlbum GetAlbum(int albumId)
		{
			return _dbContext.PhotoAlbums.Find(albumId);
		}

		public IEnumerable<PhotoAlbum> GetAlbums(int userId)
		{
			return _dbContext.PhotoAlbums.Where(a => a.UserId == userId);
		}

		public IEnumerable<PhotoAlbum> GetAlbums(AlbumTag tag)
		{
			return (IEnumerable<PhotoAlbum>)_dbContext.AlbumTags
				.Where(t => t.TagName == tag.TagName)
				.Select(t => t.Albums);
		}

		public IEnumerable<AlbumComment> GetComments(int albumId)
		{
			return _dbContext.CommentsOfAlbums.Where(c => c.AlbumId == albumId);
		}

		public int OverallRatingForAlbums(int userId)
		{
			return _dbContext.PhotoAlbums
				.Where(a => a.UserId == userId)
				.Select(a => a.Rating)
				.Aggregate(0, (s, i) => s + i);
		}

		public bool RemoveTagFromAlbum(PhotoAlbum album, AlbumTag tag)
		{
			PhotoAlbum albumFound = _dbContext.PhotoAlbums.Find(album.Id);

			if (album != null && album.Tags != null)
			{
				AlbumTag tagFound = albumFound.Tags
					.Where(t => t.TagName == tag.TagName)
					.FirstOrDefault();

				if(tagFound != null)
				{
					albumFound.Tags.Remove(tagFound);
					_dbContext.Entry(albumFound).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return true;
				}
			}
			return false;
		}

		public bool UpdateAlbum(PhotoAlbum album)
		{
			PhotoAlbum albumFound = _dbContext.PhotoAlbums.Find(album.Id);

			if (albumFound != null)
			{
				albumFound.Name = album.Name ?? albumFound.Name;
				albumFound.Rating = album.Rating;
				albumFound.IsPrivate = album.IsPrivate;
				_dbContext.Entry(albumFound).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public bool UpdateComment(AlbumComment comment)
		{
			AlbumComment commentFound = _dbContext.CommentsOfAlbums
				.Where(c => c.Id == comment.Id && c.AlbumId == comment.AlbumId)
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

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
