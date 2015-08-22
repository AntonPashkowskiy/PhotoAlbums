using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	class PhotoAlbumsContext : DbContext
	{
		public PhotoAlbumsContext(string connectionString) : base(connectionString) 
		{
			Database.SetInitializer(new PhotoAlbumsDbInitializer());
		}

		public DbSet<User> Users { get; set; }
		public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
		public DbSet<AlbumTag> AlbumTags { get; set; }
		public DbSet<Photo> Photo { get; set; }
		public DbSet<AlbumComment> CommentsOfAlbums { get; set; }
		public DbSet<PhotoComment> CommentsOfPhotos { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserEntityConfiguration());
			modelBuilder.Configurations.Add(new AlbumEntityConfiguration());
			modelBuilder.Configurations.Add(new TagEntityConfiguration());
			modelBuilder.Configurations.Add(new PhotoEntityConfiguration());
			modelBuilder.Configurations.Add(new AlbumCommentEntityConfiguration());
			modelBuilder.Configurations.Add(new PhotoCommentEntityConfiguration());
		}
	}
}
