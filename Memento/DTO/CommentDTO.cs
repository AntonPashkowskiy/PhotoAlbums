using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memento.DTO
{
	public class CommentDTO
	{
		public int Id { get; set; }
		public string AuthorFullName { get; set; }
		public string AuthorEmail { get; set; }
		public string CommentText { get; set; }
		public int ResourseId { get; set; }
		public int ResourseType { get; set; }
	}
}