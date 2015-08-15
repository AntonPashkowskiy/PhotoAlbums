using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Phone
	{
		public int Id { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsHidden { get; set; }

		public virtual User PhoneUser { get; set; }
	}
}
