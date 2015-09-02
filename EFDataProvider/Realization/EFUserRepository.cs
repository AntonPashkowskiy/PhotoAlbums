using System;
using EFDataProvider.PhotoAlbumsDbContext;
using Entities.Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFUserRepository : IUserRepository
	{
		private readonly PhotoAlbumsContext _context;

		private EFUserRepository() { }
		public EFUserRepository(PhotoAlbumsContext context)
		{
			_context = context;
		}

		public void Add(User item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Item can't be null");
			}

			try
			{
				_context.Users.Add(item);
			}
			catch (Exception e)
			{
				throw new Exception("User addign is failed.", e);
			}
		}
	}
}
