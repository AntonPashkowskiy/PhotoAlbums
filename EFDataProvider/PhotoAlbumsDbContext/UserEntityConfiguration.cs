using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Entities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataProvider
{
	class UserEntityConfiguration : EntityTypeConfiguration<User>
	{
		public UserEntityConfiguration()
		{
			this.ToTable("UserInfo");
			this.HasKey<string>(u => u.Id);

			// Foreign keys configuration
			this.HasMany(u => u.PhotoAlbums)
				.WithRequired(a => a.AlbumUser)
				.HasForeignKey<string>(a => a.UserId);

			this.HasMany(u => u.PublishedPhoto)
				.WithRequired(p => p.Author)
				.HasForeignKey<string>(p => p.AuthorId)
				.WillCascadeOnDelete(false);

			this.HasMany(u => u.CommentsOfPhoto)
				.WithRequired(c => c.Author)
				.HasForeignKey<string>(c => c.AuthorId)
				.WillCascadeOnDelete(false);

			this.HasMany(u => u.CommnetsOfAlbums)
				.WithRequired(c => c.Author)
				.HasForeignKey<string>(c => c.AuthorId)
				.WillCascadeOnDelete(false);
		}
	}
}
