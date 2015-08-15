using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class User
	{
		public User()
		{
			this.UserRoles = new List<Role>();
			this.PhotoAlbums = new List<PhotoAlbum>();
			this.PublishedPhoto = new List<Photo>();
		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhotoUrl { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string Job { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsRemoved { get; set; }

		public virtual Phone Phone { get; set; }
		public virtual ICollection<Role> UserRoles { get; set; }
		public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
		public virtual ICollection<Photo> PublishedPhoto { get; set; }
		public virtual ICollection<AlbumComment> CommnetsOfAlbums { get; set; }
		public virtual ICollection<PhotoComment> CommentsOfPhoto { get; set; }
	}
}
