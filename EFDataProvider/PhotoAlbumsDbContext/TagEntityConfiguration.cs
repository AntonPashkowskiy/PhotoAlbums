using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Entities.Entities;

namespace EFDataProvider.PhotoAlbumsDbContext
{
	class TagEntityConfiguration : EntityTypeConfiguration<AlbumTag>
	{
		public TagEntityConfiguration()
		{
			ToTable("Tag");

			HasKey(t => t.Id);

			Property(t => t.TagName)
				.HasMaxLength(20)
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName,
						new IndexAnnotation(new IndexAttribute("IX_TagName") { IsUnique = true })
				)
				.IsRequired();	
		}
	}
}
