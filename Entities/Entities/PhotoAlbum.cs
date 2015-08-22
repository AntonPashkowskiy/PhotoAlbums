using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class PhotoAlbum
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsPrivate { get; set; }
	}
}
