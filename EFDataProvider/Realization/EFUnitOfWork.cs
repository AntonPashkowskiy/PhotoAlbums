using System;
using EFDataProvider.PhotoAlbumsDbContext;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private readonly PhotoAlbumsContext _context;
		private IUserRepository _userRepository;
		private IAlbumRepository _albumRepository;
		private IPhotoRepository _photoRepository;
		private IAlbumCommentRepository _albumCommentRepository;
		private IPhotoCommentRepository _photoCommentRepository;

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
			try
			{
				_context.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception("Save changes failed.", e);
			}
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
