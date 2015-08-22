using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace EFDataProvider.Realization
{
	class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private PhotoAlbumsContext _context = null;

		private EFRepository() {}

		public EFRepository(PhotoAlbumsContext context)
		{
			Context = context;
		}

		protected PhotoAlbumsContext Context 
		{ 
			get
			{
				return _context;
			}
			private set
			{
				if (value == null)
				{
					throw new NullReferenceException("Database context can't be null.");
				}
				_context = value;
			} 
		}

		public IEnumerable<TEntity> GetAll()
		{
			return Context.Set<TEntity>();
		}

		public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
		{
			return Context.Set<TEntity>().Where(predicate);
		}

		public TEntity Get(int id)
		{
			return Context.Set<TEntity>().Find(id);
		}

		public void Add(TEntity item)
		{
			try
			{
				Context.Set<TEntity>().Add(item);
			}
			catch (Exception e)
			{
				string message = string.Format("{0} entity adding is failed.", item.GetType().ToString());
				throw new Exception(message, e);
			}
		}

		public virtual void Update(TEntity item)
		{
			return;
		}

		public void Delete(int id)
		{
			TEntity item = Context.Set<TEntity>().Find(id);

			if (item != null)
			{
				Context.Entry(item).State = EntityState.Deleted;
			}
		}
	}
}
