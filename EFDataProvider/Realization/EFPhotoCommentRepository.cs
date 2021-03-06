﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EFDataProvider.PhotoAlbumsDbContext;
using Entities.Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFPhotoCommentRepository : EFRepository<PhotoComment>, IPhotoCommentRepository
	{
		public EFPhotoCommentRepository(PhotoAlbumsContext context) : base(context) { }

		public IEnumerable<PhotoComment> GetComments(int photoId)
		{
			return Context.CommentsOfPhotos
						  .Where(c => c.PhotoId == photoId)
						  .Include(c => c.Author);
		}

		public override void Update(PhotoComment item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null");
			}

			var photoComment = Context.CommentsOfPhotos.Find(item.Id);

			if (photoComment != null)
			{
				photoComment.CommentText = item.CommentText ?? photoComment.CommentText;
				Context.Entry(photoComment).State = EntityState.Modified;
			}
		}
	}
}
