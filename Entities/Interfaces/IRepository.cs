using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> GetAll();
		IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
		TEntity Get(int id);
		void Add(TEntity item);
		void Update(TEntity item);
		void Delete(int id);
	}
}
