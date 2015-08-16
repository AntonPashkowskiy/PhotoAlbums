using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public partial class Role
	{
		// navigation property for Entity Framework
		public Role()
		{
			this.UsersInRole = new List<User>();
		}

		public virtual ICollection<User> UsersInRole { get; set; }
	}
}
