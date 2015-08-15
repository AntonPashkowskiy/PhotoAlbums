using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
	class PhotoAlbumsDbInitializer : CreateDatabaseIfNotExists<PhotoAlbumsContext>
	{
		protected override void Seed(PhotoAlbumsContext context)
		{
			// set test data to database
			base.Seed(context);
		}
	}
}
