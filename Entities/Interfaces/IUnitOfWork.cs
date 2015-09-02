using System;

namespace Entities.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository Users { get; }
		IAlbumRepository Albums { get; }
		IPhotoRepository Photo { get; }
		IPhotoCommentRepository PhotoComments { get; }
		IAlbumCommentRepository AlbumComments { get; }

		void Save();
	}
}
