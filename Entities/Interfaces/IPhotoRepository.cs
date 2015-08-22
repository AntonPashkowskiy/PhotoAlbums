using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities.Interfaces
{
	public interface IPhotoRepository : IRepository<Photo>
	{
		IEnumerable<Photo> GetPhotos(string userId, int pageNumber, int pageSize);
		int NumberOfPhotos(string userId);
		int OverallRatingForPhotos(string userId);
	}
}
