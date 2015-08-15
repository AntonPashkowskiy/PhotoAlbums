using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class PhotoAlbum
	{
		public PhotoAlbum()
		{
			this.Tags = new List<AlbumTag>();
			this.Photo = new List<Photo>();
			this.Comments = new List<AlbumComment>();
		}

		public int Id { get; set; }
		public int Rating { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsPrivate { get; set; }

		public int UserId { get; set; }
		public virtual User AlbumUser { get; set; }
		public virtual ICollection<AlbumTag> Tags { get; set; }
		public virtual ICollection<Photo> Photo { get; set; }
		public virtual ICollection<AlbumComment> Comments { get; set; }
	}
}
