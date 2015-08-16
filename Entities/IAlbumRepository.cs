using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public interface IAlbumRepository
	{
		bool AddAlbum(PhotoAlbum album);
		bool UpdateAlbum(PhotoAlbum album);
		bool DeleteAlbum(int albumId);

		// Work with tags of albums
		bool AddTag(AlbumTag tag);
		bool AddTagToAlbum(PhotoAlbum album, AlbumTag tag);
		bool RemoveTagFromAlbum(PhotoAlbum album, AlbumTag tag);

		// Work with comments of albums
		bool AddComment(AlbumComment comment);
		bool DeleteComment(AlbumComment comment);
		bool UpdateComment(AlbumComment comment);
		IEnumerable<AlbumComment> GetComments(int albumId);

		// Get-methods by different parameters
		PhotoAlbum GetAlbum(int albumId);
		PhotoAlbum GetAlbum(string albumName, int userId);
		IEnumerable<PhotoAlbum> GetAlbums(int userId);
		IEnumerable<PhotoAlbum> GetAlbums(AlbumTag[] tags);

		// Album statistic
		int CountAlbums(int userId);
		int OverallRatingForAlbums(int userId);
	}
}
