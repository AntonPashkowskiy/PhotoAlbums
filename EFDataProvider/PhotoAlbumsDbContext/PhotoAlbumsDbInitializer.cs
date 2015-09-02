using System.Data.Entity;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class PhotoAlbumsDbInitializer : CreateDatabaseIfNotExists<PhotoAlbumsContext>
	{
	}
}
