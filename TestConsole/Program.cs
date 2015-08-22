using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using EFDataProvider;
using Entities;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["PhotoAlbumsDBWork"].ConnectionString;

			using (var context = new PhotoAlbumsContext(connectionString))
			{
				User user = new User() { Id = "Simple added user." };
				context.Users.Add(user);
				context.SaveChanges();
			}
		}
	}
}
