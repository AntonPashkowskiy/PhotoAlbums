using System.Data.Entity.ModelConfiguration;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class PhotoEntityConfiguration : EntityTypeConfiguration<Photo>
	{
		public PhotoEntityConfiguration()
		{
			ToTable("Photo");

			HasKey(p => p.Id);

			Property(p => p.SmallPhotoUrl)
				.IsRequired();

			Property(p => p.MediumPhotoUrl)
				.IsRequired();

			Property(p => p.FullPhotoUrl)
				.IsRequired();

			Property(p => p.Description)
				.HasColumnType("ntext");

			Property(p => p.Rating)
				.IsRequired();

			Property(p => p.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();

			// foreign keys configuration

			HasMany(p => p.Comments)
				.WithRequired(c => c.TargetPhoto)
				.HasForeignKey(c => c.PhotoId);
		}
	}
}
