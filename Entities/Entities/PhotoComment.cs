using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class PhotoComment : Comment
	{
		public int PhotoId { get; set; }
		public string AuthorId { get; set; }
	}
}
