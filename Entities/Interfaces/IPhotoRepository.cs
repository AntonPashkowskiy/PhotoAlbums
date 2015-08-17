﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public interface IPhotoRepository : IDisposable
	{
		bool AddPhoto(Photo photo);
		bool UpdatePhoto(Photo photo);
		bool DeletePhoto(int photoId);

		// Work with comments of photo
		bool AddComment(PhotoComment comment);
		bool DeleteComment(PhotoComment comment);
		bool UpdateComment(PhotoComment comment);
		IEnumerable<PhotoComment> GetComments(int photoId);

		// Get-methods with different parameters
		Photo GetPhoto(int photoId);
		IEnumerable<Photo> GetPhotos(int userId);
		IEnumerable<Photo> GetPhotosFromAlbum(int albumId);

		// Photos statistic
		int CountPhotosByUser(int userId);
		int CountPhotosInAlbum(int albumId);
		int OverallRatingForPhotos(int userId);
	}
}