using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFPhotoCommentRepository : EFRepository<PhotoComment>, IPhotoCommentRepository
	{
		public EFPhotoCommentRepository(PhotoAlbumsContext context) : base(context) { }

		public override void Update(PhotoComment item)
		{
			var photoComment = Context.CommentsOfPhotos.Find(item.Id);

			if (photoComment != null)
			{
				photoComment.CommentText = item.CommentText ?? photoComment.CommentText;
				Context.Entry(photoComment).State = EntityState.Modified;
			}
		}
	}
}
