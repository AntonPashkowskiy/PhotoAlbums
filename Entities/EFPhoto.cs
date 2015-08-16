using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class Photo
	{
		// navigation properties for Entity Framework
		public Photo()
		{
			this.Comments = new List<PhotoComment>();
		}

		public virtual PhotoAlbum PhotoAlbum { get; set; }
		public virtual User Author { get; set; }
		public virtual ICollection<PhotoComment> Comments { get; set; }
	}
}
