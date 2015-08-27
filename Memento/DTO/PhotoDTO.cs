using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memento.DTO
{
	public class PhotoDTO
	{
		public int Id { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string MediumPhotoUrl { get; set; }
		public string FullPhotoUrl { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public bool IsEditable { get; set; }
	}
}