using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class PhotoComment
	{
		// navigation properties for Entity Framework
		public virtual Photo TargetPhoto { get; set; }
		public virtual User Author { get; set; }
	}
}
