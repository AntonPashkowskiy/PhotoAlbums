using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memento.DTO
{
	public class AlbumCommentDTO : CommentDTO
	{
		public int TargetAlbumId { get; set; }
	}
}