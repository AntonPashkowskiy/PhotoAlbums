using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFPhotoRepository : EFRepository<Photo>, IPhotoRepository
	{
		public EFPhotoRepository(PhotoAlbumsContext context) : base(context) {}

		public IEnumerable<Photo> GetPhotos(string userId, int pageNumber, int pageSize)
		{
			if (String.IsNullOrEmpty(userId) || pageNumber < 0 || pageSize < 0)
			{
				return null;
			}

			return Context.Photo
						  .Where(p => p.AuthorId == userId)
						  .OrderByDescending(p => p.CreationDate)
						  .Skip((pageNumber - 1) * pageSize)
						  .Take(pageSize);
		}

		public int NumberOfPhotos(string userId)
		{
			return Context.Photo.Count(p => p.AuthorId == userId);
		}

		public int OverallRatingForPhotos(string userId)
		{
			return Context.Photo
						  .Where(p => p.AuthorId == userId)
						  .Select(p => p.Rating)
						  .Aggregate(0, (s, i) => s + i);
		}

		public override void Update(Photo item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null.");				
			}

			var photo = Context.Photo.Find(item.Id);

			if (photo != null)
			{
				photo.Rating = item.Rating;
				photo.Description = item.Description ?? photo.Description;
				Context.Entry(photo).State = EntityState.Modified;
			}
		}
	}
}
