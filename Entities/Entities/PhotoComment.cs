namespace Entities.Entities
{
	public class PhotoComment : Comment
	{
		public int PhotoId { get; set; }
		public string AuthorId { get; set; }
		
		// navigation properties for Entity Framework
		public virtual Photo TargetPhoto { get; set; }
		public virtual User Author { get; set; }
	}
}
