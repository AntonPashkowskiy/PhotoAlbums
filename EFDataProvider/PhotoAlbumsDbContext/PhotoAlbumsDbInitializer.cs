using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class PhotoAlbumsDbInitializer : CreateDatabaseIfNotExists<PhotoAlbumsContext>
	{
		protected override void Seed(PhotoAlbumsContext context)
		{
			User[] users = new User[5] 
			{
				new User() 
				{
					FirstName = "Anton",
					LastName = "Pashkouski",
					Job = "Programmer",
					Login = "AntonLogin",
					Password = "PASSWORD",
					Email = "Wolendrang@gmail.com",
					CreationDate = DateTime.Now,
					IsRemoved = false
				},
				new User()
				{
					FirstName = "Alexei",
					LastName = "Buzuma",
					Job = "Programmer",
					Login = "AlexeiLogin",
					Password = "PASSWORD",
					Email = "Leha@gmail.com",
					CreationDate = DateTime.Now,
					IsRemoved = false
				},
				new User()
				{
					FirstName = "Victor",
					LastName = "Makoed",
					Job = "Programmer",
					Login = "VictorLogin",
					Password = "PASSWORD",
					Email = "Victor@gmail.com",
					CreationDate = DateTime.Now,
					IsRemoved = false
				},
				new User()
				{
					FirstName = "Konstantin",
					LastName = "Nikitin",
					Job = "Programmer",
					Login = "KostyaLogin",
					Password = "PASSWORD",
					Email = "Kostya@gmail.com",
					CreationDate = DateTime.Now,
					IsRemoved = false
				},
				new User()
				{
					FirstName = "Jevgeniy",
					LastName = "Sasin",
					Job = "Programmer",
					Login = "JevgenLogin",
					Password = "PASSWORD",
					Email = "sasin@gmail.com",
					CreationDate = DateTime.Now,
					IsRemoved = false
				}
			};

			Role role = new Role()
			{
				Name = "User",
				UsersInRole = users.ToList()
			};

			Role adminRole = new Role()
			{
				Name = "Admin",
				UsersInRole = users.ToList()
			};

			foreach (var user in users)
			{
				context.Users.Add(user);
			}
			context.Roles.Add(role);
			context.Roles.Add(adminRole);

			context.SaveChanges();

			base.Seed(context);
		}
	}
}
