using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Entities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext
{
	class UserEntityConfiguration : EntityTypeConfiguration<User>
	{
		public UserEntityConfiguration()
		{
			this.ToTable("UserInfo");

			this.HasKey<int>(u => u.Id);

			this.Property(u => u.FirstName)
				.HasMaxLength(40)
				.IsRequired();

			this.Property(u => u.LastName)
				.HasMaxLength(40)
				.IsRequired();

			this.Property(u => u.Login)
				.HasMaxLength(40)
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName, 
						new IndexAnnotation(
							new IndexAttribute("IX_Login") { IsUnique = true })
				)
				.IsRequired();

			this.Property(u => u.Email)
				.HasMaxLength(40)
				.IsRequired()
				.HasColumnAnnotation(
						IndexAnnotation.AnnotationName, 
						new IndexAnnotation(
							new IndexAttribute("IX_Email") { IsUnique = true })
				)
				.IsRequired();

			this.Property(u => u.Password)
				.HasMaxLength(50)
				.IsRequired();

			this.Property(u => u.Job)
				.HasMaxLength(40)
				.IsOptional();

			this.Property(u => u.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();

			this.Property(u => u.IsRemoved)
				.IsRequired();

			// Foreign keys configuration

			this.HasOptional(u => u.Phone)
				.WithRequired(p => p.PhoneUser);

			this.HasMany(u => u.PhotoAlbums)
				.WithRequired(a => a.AlbumUser)
				.HasForeignKey(a => a.UserId);

			this.HasMany(u => u.PublishedPhoto)
				.WithRequired(p => p.Author)
				.HasForeignKey(p => p.AuthorId)
				.WillCascadeOnDelete(false);

			this.HasMany(u => u.CommentsOfPhoto)
				.WithRequired(c => c.Author)
				.HasForeignKey(c => c.AuthorId)
				.WillCascadeOnDelete(false);

			this.HasMany(u => u.CommnetsOfAlbums)
				.WithRequired(c => c.Author)
				.HasForeignKey(c => c.AuthorId)
				.WillCascadeOnDelete(false);

			this.HasMany(u => u.UserRoles)
				.WithMany(r => r.UsersInRole)
				.Map(ur =>
				{
					ur.MapLeftKey("UserRefId");
					ur.MapRightKey("RoleRefId");
					ur.ToTable("UserRole");
				});
		}
	}
}
