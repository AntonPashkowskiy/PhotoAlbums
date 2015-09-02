using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EFDataProvider.PhotoAlbumsDbContext;
using Entities.Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFAlbumRepository : EFRepository<PhotoAlbum>, IAlbumRepository
	{
		public EFAlbumRepository(PhotoAlbumsContext context) : base(context) {}

		public void AddAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null");
			}

			if (tags != null)
			{
				tags = AddTagsToContext(tags);
				item.Tags = (ICollection<AlbumTag>)tags;
			}
			Context.PhotoAlbums.Add(item);
		}

		public void AddTagsToAlbum(int albumId, IEnumerable<AlbumTag> tags)
		{
			var album = Context.PhotoAlbums.Find(albumId);

			if (album != null)
			{
				if (tags != null)
				{
					var addedTags = AddTagsToContext(tags);

					foreach (var tag in addedTags)
					{
						album.Tags.Add(tag);
					}
				}
				Context.Entry(album).State = EntityState.Modified;
			}
		}


		public IEnumerable<PhotoAlbum> GetAlbumsByTag(AlbumTag tag)
		{
			if (tag == null)
			{
				return null;
			}

			var tagFounded = Context.AlbumTags.FirstOrDefault(t => t.TagName == tag.TagName);

			return tagFounded != null ? (IEnumerable<PhotoAlbum>)tagFounded.Albums : null;
		}

		public IEnumerable<PhotoAlbum> GetAlbumsByName(string fullUserName)
		{
			if (fullUserName == null)
			{
				return null;
			}

			return Context.PhotoAlbums.Where(a =>
				string.Join(" ", a.AlbumUser.FirstName, a.AlbumUser.LastName) == fullUserName
			);
		}

		public int NumberOfAlbums(string userId)
		{
			return Context.PhotoAlbums.Count(a => a.UserId == userId);
		}

		public int OverallRatingForAlbums(string userId)
		{
			var result = Context.PhotoAlbums
						  .Where(a => a.UserId == userId)
						  .Select(a => a.Rating);

			return ((IEnumerable<int>)result).Aggregate(0, (s, i) => s + i);
		}

		public override void Update(PhotoAlbum item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null");
			}

			var album = Context.PhotoAlbums.Find(item.Id);

			if (album != null)
			{
				album.Rating = item.Rating;
				album.Description = item.Description ?? album.Description;
				Context.Entry(album).State = EntityState.Modified;
			}
		}

		#region Private
		private IEnumerable<AlbumTag> AddTagsToContext(IEnumerable<AlbumTag> tags)
		{
			var existingTags = Context.AlbumTags;
			var result = new List<AlbumTag>();

			foreach (var tag in tags)
			{
				var tagFound = existingTags.FirstOrDefault(t => t.TagName == tag.TagName);

				if(tagFound == null)
				{
					Context.AlbumTags.Add(tag);
					result.Add(tag);
				}
				else 
				{
					result.Add(tagFound);
				}
			}

			return result;
		}
		#endregion
	}
}
