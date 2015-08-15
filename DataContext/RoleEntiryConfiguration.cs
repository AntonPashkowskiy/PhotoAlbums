using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataContext
{
	class RoleEntiryConfiguration : EntityTypeConfiguration<Role>
	{
		public RoleEntiryConfiguration()
		{
			this.ToTable("Role");

			this.HasKey<int>(r => r.Id);

			this.Property(r => r.Name)
				.HasMaxLength(20)
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName,
						new IndexAnnotation(new IndexAttribute("IX_RoleName") { IsUnique = true })
				)
				.IsRequired();
		}
	}
}
