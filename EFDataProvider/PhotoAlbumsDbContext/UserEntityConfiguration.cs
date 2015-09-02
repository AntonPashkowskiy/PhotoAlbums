using System.Data.Entity.ModelConfiguration;
using Entities;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class UserEntityConfiguration : EntityTypeConfiguration<User>
	{
		public UserEntityConfiguration()
		{
			ToTable("UserInfo");
			HasKey(u => u.Id);

			Property(u => u.FirstName)
				.IsRequired();

			Property(u => u.LastName)
				.IsRequired();

			Property(u => u.Email)
				.IsRequired();

			// Foreign keys configuration
			HasMany(u => u.PhotoAlbums)
				.WithRequired(a => a.AlbumUser)
				.HasForeignKey(a => a.UserId);

			HasMany(u => u.PublishedPhoto)
				.WithRequired(p => p.Author)
				.HasForeignKey(p => p.AuthorId)
				.WillCascadeOnDelete(false);

			HasMany(u => u.CommentsOfPhoto)
				.WithRequired(c => c.Author)
				.HasForeignKey(c => c.AuthorId)
				.WillCascadeOnDelete(false);

			HasMany(u => u.CommnetsOfAlbums)
				.WithRequired(c => c.Author)
				.HasForeignKey(c => c.AuthorId)
				.WillCascadeOnDelete(false);
		}
	}
}
