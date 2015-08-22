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
			base.Seed(context);
		}
	}
}
