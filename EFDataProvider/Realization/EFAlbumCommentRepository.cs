using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFAlbumCommentRepository : EFRepository<AlbumComment>, IAlbumCommentRepository
	{
		public EFAlbumCommentRepository(PhotoAlbumsContext context) : base(context) {}

		public override void Update(AlbumComment item)
		{
			var albumComment = Context.CommentsOfAlbums.Find(item.Id);

			if (albumComment != null)
			{
				albumComment.CommentText = item.CommentText ?? albumComment.CommentText;
				Context.Entry(albumComment).State = EntityState.Modified; 
			}
		}
	}
}
