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
		void AddAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags);
		void AddTagsToAlbum(int albumId, IEnumerable<AlbumTag> tags);
		IEnumerable<PhotoAlbum> GetAlbumsByTag(AlbumTag tag);
		int NumberOfAlbums(string userId);
		int OverallRatingForAlbums(string userId);
	}
}
