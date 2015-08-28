﻿using System;
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

		public static CommentDTO ToCommentDTO(this PhotoComment comment)
		{
			return new CommentDTO()
			{
				Id = comment.Id,
				CommentText = comment.CommentText,
				AuthorFullName = string.Join(" ", comment.Author.FirstName, comment.Author.LastName),
				AuthorEmail = comment.Author.Email,
				ResourseId = comment.PhotoId,
				ResourseType = CommentTypeMapper.GetResourseType(CommentType.PhotoComment)
			};
		}

		public static CommentDTO ToCommentDTO(this AlbumComment comment)
		{
			return new CommentDTO()
			{
				Id = comment.Id,
				CommentText = comment.CommentText,
				AuthorFullName = string.Join(" ", comment.Author.FirstName, comment.Author.LastName),
				AuthorEmail = comment.Author.Email,
				ResourseId = comment.AlbumId,
				ResourseType = CommentTypeMapper.GetResourseType(CommentType.AlbumComment)
			};
		}
	}
}