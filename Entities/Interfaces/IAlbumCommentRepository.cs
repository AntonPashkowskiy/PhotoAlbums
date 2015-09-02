using System.Collections.Generic;
using Entities.Entities;

namespace Entities.Interfaces
{
	public interface IAlbumCommentRepository : IRepository<AlbumComment>
	{
		IEnumerable<AlbumComment> GetComments(int albumId);
	}
}
