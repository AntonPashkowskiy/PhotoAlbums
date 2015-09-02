using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EFDataProvider.PhotoAlbumsDbContext;
using Entities.Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFAlbumCommentRepository : EFRepository<AlbumComment>, IAlbumCommentRepository
	{
		public EFAlbumCommentRepository(PhotoAlbumsContext context) : base(context) { }

		public IEnumerable<AlbumComment> GetComments(int albumId)
		{
			return Context.CommentsOfAlbums
						  .Where(c => c.AlbumId == albumId)
						  .Include(c => c.Author);
		}

		public override void Update(AlbumComment item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null");
			}

			var albumComment = Context.CommentsOfAlbums.Find(item.Id);

			if (albumComment != null)
			{
				albumComment.CommentText = item.CommentText ?? albumComment.CommentText;
				Context.Entry(albumComment).State = EntityState.Modified; 
			}
		}
	}
}
