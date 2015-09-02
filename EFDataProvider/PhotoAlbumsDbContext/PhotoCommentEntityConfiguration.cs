using System.Data.Entity.ModelConfiguration;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class PhotoCommentEntityConfiguration : EntityTypeConfiguration<PhotoComment>
	{
		public PhotoCommentEntityConfiguration()
		{
			ToTable("PhotoComment");

			HasKey(c => c.Id);

			Property(c => c.CommentText)
				.HasColumnType("ntext")
				.IsRequired();

			Property(c => c.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();
		}
	}
}
