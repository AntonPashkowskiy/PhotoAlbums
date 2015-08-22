using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class AlbumTag
	{
		// navigation property for Entity Framework
		public virtual ICollection<PhotoAlbum> Albums { get; set; }
	}
}
