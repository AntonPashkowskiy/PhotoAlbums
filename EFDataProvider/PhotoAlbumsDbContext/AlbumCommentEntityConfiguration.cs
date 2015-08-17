using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class AlbumCommentEntityConfiguration : EntityTypeConfiguration<AlbumComment>
	{
		public AlbumCommentEntityConfiguration()
		{
			this.ToTable("AlbumComment");

			this.HasKey<int>(c => c.Id);

			this.Property(c => c.CommentText)
				.HasColumnType("ntext")
				.IsRequired();

			this.Property(c => c.CreationDate)
				.HasColumnType("datetime")
				.IsRequired();
		}
	}
}
