using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFUserRepository : IUserRepository
	{
		private PhotoAlbumsContext _context = null;

		private EFUserRepository() { }
		public EFUserRepository(PhotoAlbumsContext context)
		{
			_context = context;
		}

		public void Add(User item)
		{
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
