using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Photo
	{
		public Photo()
		{
			this.Comments = new List<PhotoComment>();
		}

		public int Id { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string MediumPhotoUrl { get; set; }
		public string FullPhotoUrl { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public DateTime CreationDate { get; set; }

		public int AlbumId { get; set; }
		public virtual PhotoAlbum PhotoAlbum { get; set; }
		public int AuthorId { get; set; }
		public virtual User Author { get; set; }
		public virtual ICollection<PhotoComment> Comments { get; set; }
	}
}
