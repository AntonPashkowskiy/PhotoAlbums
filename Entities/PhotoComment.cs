using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class PhotoComment : Comment
	{
		public int PhotoId { get; set; }
		public virtual Photo TargetPhoto { get; set; }
		public int AuthorId { get; set; }
		public virtual User Author { get; set; }
	}
}
