using System;
using System.Collections.Generic;

namespace Entities.Entities
{
	public class PhotoAlbum
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsPrivate { get; set; }

		// navigation properties for Entity Framework
		public virtual User AlbumUser { get; set; }
		public virtual ICollection<AlbumTag> Tags { get; set; }
		public virtual ICollection<Photo> Photo { get; set; }
		public virtual ICollection<AlbumComment> Comments { get; set; }
	}
}
