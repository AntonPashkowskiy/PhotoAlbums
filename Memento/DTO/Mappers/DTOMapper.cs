using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace Memento.DTO
{
	public static class DTOMapperExtention
	{
		public static Photo ToPhoto(this PhotoDTO photoDto)
		{
			return new Photo()
			{
				Id = photoDto.Id,
				SmallPhotoUrl = photoDto.SmallPhotoUrl,
				MediumPhotoUrl = photoDto.MediumPhotoUrl,
				Description = photoDto.Description,
				Rating = photoDto.Rating
			};
		}

		public static PhotoAlbum ToPhotoAlbum(this PhotoAlbumDTO photoAlbumDto)
		{
			return new PhotoAlbum()
			{
				Id = photoAlbumDto.Id,
				Name = photoAlbumDto.Name,
				Description = photoAlbumDto.Description,
				Rating = photoAlbumDto.Rating
			};
		}

		public static AlbumComment ToAlbumComment(this AlbumCommentDTO albumCommentDto)
		{
			return new AlbumComment()
			{
				Id = albumCommentDto.Id,
				CommentText = albumCommentDto.CommentText,
				AlbumId = albumCommentDto.TargetAlbumId
			};
		}

		public static PhotoComment ToPhotoComment(this PhotoCommentDTO photoCommentDto)
		{
			return new PhotoComment()
			{
				Id = photoCommentDto.Id,
				CommentText = photoCommentDto.CommentText,
				PhotoId = photoCommentDto.TargetPhotoId
			};
		}
	}
}