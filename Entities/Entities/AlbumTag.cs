using System.Collections.Generic;

namespace Entities.Entities
{
	public class AlbumTag
	{
		public int Id { get; set; }
		public string TagName { get; set; }

		// navigation property for Entity Framework
		public virtual ICollection<PhotoAlbum> Albums { get; set; }
	}
}
