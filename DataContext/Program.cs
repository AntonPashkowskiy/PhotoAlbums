using System;
using System.Collections.Generic;
using System.Configuration;
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
			string connectionString = ConfigurationManager.ConnectionStrings["PhotoAlbumsDB"].ConnectionString;
			
			using (var context = new PhotoAlbumsContext(connectionString))
			{
				context.Roles.Add(new Role() { Id = 5, Name = "Admin" });
				context.SaveChanges();
			}
		}
	}
}
