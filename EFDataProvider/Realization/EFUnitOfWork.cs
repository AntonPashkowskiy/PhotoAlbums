using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private PhotoAlbumsContext _context = null;
		private IUserRepository _userRepository = null;
		private IAlbumRepository _albumRepository = null;
		private IPhotoRepository _photoRepository = null;
		private IAlbumCommentRepository _albumCommentRepository = null;
		private IPhotoCommentRepository _photoCommentRepository = null;

		private EFUnitOfWork() {}

		public EFUnitOfWork(string connectionString)
		{
			if (String.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException("Connection string can't be null or empty.");
			}
			_context = new PhotoAlbumsContext(connectionString);
		}

		public IUserRepository Users
		{
			get 
			{
				if (_userRepository == null)
				{
					_userRepository = new EFUserRepository(_context);
					return _userRepository;
				}
				return _userRepository;
			}
		}

		public IAlbumRepository Albums
		{
			get 
			{
				if (_albumRepository == null)
				{
					_albumRepository = new EFAlbumRepository(_context);
					return _albumRepository;
				}
				return _albumRepository;
			}
		}

		public IPhotoRepository Photo
		{
			get 
			{
				if (_photoRepository == null)
				{
					_photoRepository = new EFPhotoRepository(_context);
					return _photoRepository;
				}
				return _photoRepository;
			}
		}

		public IPhotoCommentRepository PhotoComments
		{
			get 
			{
				if (_photoCommentRepository == null)
				{
					_photoCommentRepository = new EFPhotoCommentRepository(_context);
					return _photoCommentRepository;
				}
				return _photoCommentRepository;
			}
		}

		public IAlbumCommentRepository AlbumComments
		{
			get 
			{
				if (_albumCommentRepository == null)
				{
					_albumCommentRepository = new EFAlbumCommentRepository(_context);
					return _albumCommentRepository;
				}
				return _albumCommentRepository;
			}
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
