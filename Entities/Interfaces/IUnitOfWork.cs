using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

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
