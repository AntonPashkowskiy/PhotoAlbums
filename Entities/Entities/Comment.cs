using System;

namespace Entities.Entities
{
	public abstract class Comment
	{
		public int Id { get; set; }
		public string CommentText { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
