using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Role
	{
		public Role()
		{
			this.UsersInRole = new List<User>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<User> UsersInRole { get; set; }
	}
}
