using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Memento.DTO
{
	public static class EntitiesMapperExtention
	{
		public static PhotoDTO ToPhotoDTO(this Photo photo)
		{
			return new PhotoDTO()
			{
				Id = photo.Id,
				SmallPhotoUrl = photo.SmallPhotoUrl,
				MediumPhotoUrl = photo.MediumPhotoUrl,
				FullPhotoUrl = photo.FullPhotoUrl,
				Description = photo.Description,
				Rating = photo.Rating
			};
		}

		public static PhotoAlbumDTO ToPhotoAlbumDTO(this PhotoAlbum album)
		{
			return new PhotoAlbumDTO()
			{
				Id = album.Id,
				Name = album.Name,
				Description = album.Description,
				Rating = album.Rating
			};
		}

		public static PhotoCommentDTO ToPhotoCommentDTO(this PhotoComment comment)
		{
			return new PhotoCommentDTO()
			{
				Id = comment.Id,
				CommentText = comment.CommentText,
				TargetPhotoId = comment.PhotoId
			};
		}

		public static AlbumCommentDTO ToAlbumCommentDTO(this AlbumComment comment)
		{
			return new AlbumCommentDTO()
			{
				Id = comment.Id,
				CommentText = comment.CommentText,
				TargetAlbumId = comment.AlbumId
			};
		}
	}
}