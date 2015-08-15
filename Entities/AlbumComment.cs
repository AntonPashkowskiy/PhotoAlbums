using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class AlbumComment : Comment
	{
		public int AlbumId { get; set; }
		public virtual PhotoAlbum TargetAlbum { get; set; }
		public int AuthorId { get; set; }
		public virtual User Author { get; set; }
	}
}
