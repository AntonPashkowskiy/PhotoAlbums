using System.Data.Entity.ModelConfiguration;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class AlbumEntityConfiguration : EntityTypeConfiguration<PhotoAlbum>
	{
		public AlbumEntityConfiguration()
		{
			ToTable("Album");

			HasKey(a => a.Id);

			Property(a => a.Name)
				.HasMaxLength(40)
				.IsRequired();

			Property(a => a.Description)
				.HasColumnType("ntext");

			Property(a => a.Rating)
				.IsRequired();

			Property(a => a.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();

			Property(a => a.IsPrivate)
				.IsRequired();

			// foreign keys configuration

			HasMany(a => a.Photo)
				.WithRequired(p => p.PhotoAlbum)
				.HasForeignKey(p => p.AlbumId);

			HasMany(a => a.Comments)
				.WithRequired(c => c.TargetAlbum)
				.HasForeignKey(c => c.AlbumId);

			HasMany(a => a.Tags)
				.WithMany(t => t.Albums)
				.Map(at =>
				{
					at.MapLeftKey("AlbumRefId");
					at.MapRightKey("TagRefId");
					at.ToTable("AlbumTag");
				});
		}
	}
}
