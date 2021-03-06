﻿using System;
using System.Collections.Generic;
using Entities.Entities;

namespace ServiceLayer
{
	public interface IDataService : IDisposable
	{
		void NewUser(User item);
		int CreateAlbum(PhotoAlbum item, IEnumerable<AlbumTag> tags);
		int CreatePhoto(Photo item);
		int CreateAlbumComment(AlbumComment item);
		int CreatePhotoComment(PhotoComment item);

		void UpdateAlbum(PhotoAlbum item);
		void UpdatePhoto(Photo item);
		void UpdateAlbumComment(AlbumComment item);
		void UpdatePhotoComment(PhotoComment item);

		bool CheckPossibilityOfChangingComment(string userId, int commentId, CommentType type);
		bool CheckPossibilityOfChangingPhoto(string userId, int photoId);
		bool CheckPossibilityOfChangingAlbum(string userId, int albumId);

		void DeleteAlbum(int albumId);
		void DeletePhoto(int photoId);
		void DeleteAlbumComment(int albumCommentId);
		void DeletePhotoComment(int photoCommentId);

		Photo GetPhoto(int albumId);
		IEnumerable<PhotoAlbum> GetAlbums(string userId);
		IEnumerable<PhotoAlbum> GetAlbums(AlbumTag tag);
		IEnumerable<PhotoAlbum> GetAlbumsByName(string fullUserName);
		IEnumerable<Photo> GetPhotos(string userId, int albumId);
		IEnumerable<Photo> GetPhotos(string userId, int pageNumber, int pageSize);
		IEnumerable<AlbumComment> GetAlbumComments(int albumId);
		IEnumerable<PhotoComment> GetPhotoComments(int photoId);

		UserStatistic GetUserStatistic(string userId);
	}
}
