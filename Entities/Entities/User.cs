using System.Collections.Generic;

namespace Entities.Entities
{
	public class User
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		// navigation properties for Entity Framework
		public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
		public virtual ICollection<Photo> PublishedPhoto { get; set; }
		public virtual ICollection<AlbumComment> CommnetsOfAlbums { get; set; }
		public virtual ICollection<PhotoComment> CommentsOfPhoto { get; set; }
	}
}
