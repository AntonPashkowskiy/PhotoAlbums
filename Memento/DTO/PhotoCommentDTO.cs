using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memento.DTO
{
	public class PhotoCommentDTO : CommentDTO
	{
		public int TargetPhotoId { get; set; }
	}
}