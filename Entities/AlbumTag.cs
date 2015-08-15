using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class AlbumTag
	{
		public AlbumTag()
		{
			this.Albums = new List<PhotoAlbum>();
		}

		public int Id { get; set; }
		public string TagName { get; set; }

		public virtual ICollection<PhotoAlbum> Albums { get; set; }
	}
}
