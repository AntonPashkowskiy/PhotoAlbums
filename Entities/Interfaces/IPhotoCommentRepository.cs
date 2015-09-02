using System.Collections.Generic;
using Entities.Entities;

namespace Entities.Interfaces
{
	public interface IPhotoCommentRepository : IRepository<PhotoComment> 
	{
		IEnumerable<PhotoComment> GetComments(int photoId);
	}
}
