namespace Entities.Entities
{
	public class AlbumComment : Comment
	{
		public int AlbumId { get; set; }
		public string AuthorId { get; set; }

		// navigation properties for Entity Framework
		public virtual PhotoAlbum TargetAlbum { get; set; }
		public virtual User Author { get; set; }
	}
}
