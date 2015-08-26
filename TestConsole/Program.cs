using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using EFDataProvider;
using Entities;
using Entities.Interfaces;
using EFDataProvider.Realization;
using ServiceLayer;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["PhotoAlbumsDBHome"].ConnectionString;
			IUnitOfWork unitOfWork = new EFUnitOfWork(connectionString);
		}
	}
}
