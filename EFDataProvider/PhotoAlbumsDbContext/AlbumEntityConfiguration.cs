using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class AlbumEntityConfiguration : EntityTypeConfiguration<PhotoAlbum>
	{
		public AlbumEntityConfiguration()
		{
			this.ToTable("Album");

			this.HasKey<int>(a => a.Id);

			this.Property(a => a.Name)
				.HasMaxLength(40)
				.IsRequired();

			this.Property(a => a.Description)
				.HasColumnType("ntext");

			this.Property(a => a.Rating)
				.IsRequired();

			this.Property(a => a.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();

			this.Property(a => a.IsPrivate)
				.IsRequired();

			// foreign keys configuration

			this.HasMany(a => a.Photo)
				.WithRequired(p => p.PhotoAlbum)
				.HasForeignKey(p => p.AlbumId);

			this.HasMany(a => a.Comments)
				.WithRequired(c => c.TargetAlbum)
				.HasForeignKey(c => c.AlbumId);

			this.HasMany(a => a.Tags)
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
