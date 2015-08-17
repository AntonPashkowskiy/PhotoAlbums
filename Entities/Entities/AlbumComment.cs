using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class AlbumComment : Comment
	{
		public int AlbumId { get; set; }
		public int AuthorId { get; set; }
	}
}
