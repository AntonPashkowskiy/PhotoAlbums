using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class TagEntityConfiguration : EntityTypeConfiguration<AlbumTag>
	{
		public TagEntityConfiguration()
		{
			this.ToTable("Tag");

			this.HasKey<int>(t => t.Id);

			this.Property(t => t.TagName)
				.HasMaxLength(20)
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName,
						new IndexAnnotation(new IndexAttribute("IX_TagName") { IsUnique = true })
				)
				.IsRequired();	
		}
	}
}
