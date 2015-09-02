namespace Memento.Models
{
	public class AlbumViewModel
	{
		public int AlbumId { get; set; }
		public string AuthorId { get; set; }
		public string AlbumName { get; set; }
		public int AlbumRating { get; set; }
		public bool IsAlbumOfUser { get; set; }
	}
}