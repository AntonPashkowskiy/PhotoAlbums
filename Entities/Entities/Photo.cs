using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class Photo
	{
		public int Id { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string MediumPhotoUrl { get; set; }
		public string FullPhotoUrl { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public DateTime CreationDate { get; set; }
		public string AuthorId { get; set; }
		public int AlbumId { get; set; }
	}
}
