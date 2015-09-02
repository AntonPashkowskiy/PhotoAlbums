using System.Data.Entity.ModelConfiguration;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class AlbumCommentEntityConfiguration : EntityTypeConfiguration<AlbumComment>
	{
		public AlbumCommentEntityConfiguration()
		{
			ToTable("AlbumComment");

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
