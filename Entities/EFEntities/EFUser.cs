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
		public User()
		{
			this.UserRoles = new List<Role>();
			this.PhotoAlbums = new List<PhotoAlbum>();
			this.PublishedPhoto = new List<Photo>();
			this.CommentsOfPhoto = new List<PhotoComment>();
			this.CommnetsOfAlbums = new List<AlbumComment>();
		}

		public virtual Phone Phone { get; set; }
		public virtual ICollection<Role> UserRoles { get; set; }
		public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
		public virtual ICollection<Photo> PublishedPhoto { get; set; }
		public virtual ICollection<AlbumComment> CommnetsOfAlbums { get; set; }
		public virtual ICollection<PhotoComment> CommentsOfPhoto { get; set; }
	}
}
