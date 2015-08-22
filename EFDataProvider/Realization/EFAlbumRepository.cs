using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFAlbumRepository : EFRepository<PhotoAlbum>, IAlbumRepository
	{
		public EFAlbumRepository(PhotoAlbumsContext context) : base(context) {}

		public void AddTagsToAlbum(int albumId, IEnumerable<AlbumTag> tags)
		{
			var album = Context.PhotoAlbums.Find(albumId);

			if (album != null)
			{
				if (tags != null)
				{
					AddTagsToContext(tags);
				}

				foreach (var tag in tags)
				{
					album.Tags.Add(tag);
				}
				Context.Entry(album).State = EntityState.Modified;
			}
		}

		public int NumberOfAlbums(string userId)
		{
			return Context.PhotoAlbums.Count(a => a.UserId == userId);
		}

		public int OverallRatingForAlbums(string userId)
		{
			return Context.PhotoAlbums
						  .Where(a => a.UserId == userId)
						  .Select(a => a.Rating)
						  .Aggregate(0, (s, i) => s + i);
		}

		public override void Update(PhotoAlbum item)
		{
			var album = Context.PhotoAlbums.Find(item.Id);

			if (album != null)
			{
				album.Rating = item.Rating;
				album.Description = item.Description ?? album.Description;
				Context.Entry(album).State = EntityState.Modified;
			}
		}

		#region Private
		private void AddTagsToContext(IEnumerable<AlbumTag> tags)
		{
			var existingTags = Context.AlbumTags;

			foreach (var tag in tags)
			{
				if(existingTags.FirstOrDefault(t => t.TagName == tag.TagName) == null)
				{
					Context.AlbumTags.Add(tag);
				}
			}
		}
		#endregion
	}
}
