using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities.Interfaces
{
	public interface IAlbumRepository : IRepository<PhotoAlbum>
	{
		void AddTagsToAlbum(int albumId, IEnumerable<AlbumTag> tags);
		int NumberOfAlbums(string userId);
		int OverallRatingForAlbums(string userId);
	}
}
