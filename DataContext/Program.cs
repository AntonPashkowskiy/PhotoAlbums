using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataContext
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var context = new PhotoAlbumsContext("PhotoAlbumsDB"))
			{
				context.Roles.Add(new Role() { Id = 5, Name = "Admin" });
				context.SaveChanges();
			}
		}
	}
}
