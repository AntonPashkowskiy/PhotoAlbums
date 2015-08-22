using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class User
	{
		// navigation properties for Entity Framework
		public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
		public virtual ICollection<Photo> PublishedPhoto { get; set; }
		public virtual ICollection<AlbumComment> CommnetsOfAlbums { get; set; }
		public virtual ICollection<PhotoComment> CommentsOfPhoto { get; set; }
	}
}
