using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataContext
{
	class PhotoEntityConfiguration : EntityTypeConfiguration<Photo>
	{
		public PhotoEntityConfiguration()
		{
			this.ToTable("Photo");

			this.HasKey<int>(p => p.Id);

			this.Property(p => p.SmallPhotoUrl)
				.IsRequired();

			this.Property(p => p.MediumPhotoUrl)
				.IsRequired();

			this.Property(p => p.FullPhotoUrl)
				.IsRequired();

			this.Property(p => p.Description)
				.HasColumnType("ntext");

			this.Property(p => p.Rating)
				.IsRequired();

			this.Property(p => p.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();

			// foreign keys configuration

			this.HasMany(p => p.Comments)
				.WithRequired(c => c.TargetPhoto)
				.HasForeignKey(c => c.PhotoId);
		}
	}
}
