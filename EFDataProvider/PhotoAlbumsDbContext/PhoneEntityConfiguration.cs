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
	class PhoneEntityConfiguration : EntityTypeConfiguration<Phone>
	{
		public PhoneEntityConfiguration()
		{
			this.ToTable("Phone");

			this.HasKey<int>(p => p.Id);

			this.Property(p => p.PhoneNumber)
				.HasMaxLength(20)
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName,
						new IndexAnnotation(new IndexAttribute("IX_PhoneNumber") { IsUnique = true })
				)
				.IsRequired();

			this.Property(p => p.IsHidden)
				.IsRequired();
		}
	}
}
