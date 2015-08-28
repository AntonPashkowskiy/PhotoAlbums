using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities.Interfaces
{
	public interface IPhotoCommentRepository : IRepository<PhotoComment> 
	{
		IEnumerable<PhotoComment> GetComments(int photoId);
	}
}
