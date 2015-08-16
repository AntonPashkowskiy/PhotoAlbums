using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class PhotoAlbum
	{
		// navigation properties for Entity Framework
		public PhotoAlbum()
		{
			this.Tags = new List<AlbumTag>();
			this.Photo = new List<Photo>();
			this.Comments = new List<AlbumComment>();
		}

		public virtual User AlbumUser { get; set; }
		public virtual ICollection<AlbumTag> Tags { get; set; }
		public virtual ICollection<Photo> Photo { get; set; }
		public virtual ICollection<AlbumComment> Comments { get; set; }
	}
}
