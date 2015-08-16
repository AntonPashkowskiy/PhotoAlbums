using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class AlbumComment
	{
		// navigation properties for Entity Framework
		public virtual PhotoAlbum TargetAlbum { get; set; }
		public virtual User Author { get; set; }
	}
}
