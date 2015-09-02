using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Entities.Entities;
using Memento.DTO;
using Memento.DTO.Mappers;
using ServiceLayer;

namespace Memento.Controllers.API
{
    public class CommentsApiController : BaseApiController
    {
		public CommentsApiController(IDataService dataService) : base(dataService)
		{
		}

		[Route("api/albums/{albumId}/comments")]
		[HttpGet]
		public IEnumerable<CommentDTO> GetCommentsOfAlbum(int albumId)
		{
			return DataService.GetAlbumComments(albumId).Select(c => c.ToCommentDto());
		}

		[Route("api/photos/{photoId}/comments")]
		[HttpGet]
		public IEnumerable<CommentDTO> GetCommentsOfPhoto(int photoId)
		{
			return DataService.GetPhotoComments(photoId).Select(c => c.ToCommentDto());
		}

		[Route("api/comments/add")]
		[HttpPost]
		public CommentDTO AddComment(CommentDTO comment)
		{
			CommentType type = CommentTypeMapper.GetCommentType(comment.ResourseType);

			switch (type)
			{
				case CommentType.AlbumComment:
					AlbumComment informationAboutComment = comment.ToAlbumComment();
					informationAboutComment.AuthorId = CurrentUserId;
					informationAboutComment.Id = DataService.CreateAlbumComment(informationAboutComment);
					return informationAboutComment.ToCommentDto();

				case CommentType.PhotoComment:
				default:
					PhotoComment information = comment.ToPhotoComment();
					information.AuthorId = CurrentUserId;
					information.Id = DataService.CreatePhotoComment(information);
					return information.ToCommentDto();
			}
		}

		[Route("api/comments/update")]
		[HttpPut]
		public void UpdateComment(CommentDTO comment)
		{
			CommentType type = CommentTypeMapper.GetCommentType(comment.ResourseType);
			bool isCommentOwner = DataService.CheckPossibilityOfChangingComment(CurrentUserId, comment.Id, type);

			if (isCommentOwner)
			{
				switch (type)
				{
					case CommentType.AlbumComment:
						DataService.UpdateAlbumComment(comment.ToAlbumComment());
						break;

					case CommentType.PhotoComment:
					default:
						DataService.UpdatePhotoComment(comment.ToPhotoComment());
						break;
				}
			}
		}

		[Route("api/albums/comments/{commentId}/delete")]
		[HttpDelete]
		public void DeleteAlbumComment(int commentId)
		{
			bool isOwnerOfResourse = DataService.CheckPossibilityOfChangingComment(
				CurrentUserId,
				commentId,
				CommentType.AlbumComment);

			if (isOwnerOfResourse)
			{
				DataService.DeleteAlbumComment(commentId);
			}
		}

		[Route("api/photos/comments/{commentId}/delete")]
		[HttpDelete]
		public void DeletePhotoComment(int commentId)
		{
			bool isOwnerOfResourse = DataService.CheckPossibilityOfChangingComment(
				CurrentUserId,
				commentId,
				CommentType.PhotoComment);

			if (isOwnerOfResourse)
			{
				DataService.DeletePhotoComment(commentId);
			}
		}
	}
}
