using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memento.Models
{
	public class AlbumViewModel
	{
		public string AuthorId { get; set; }
		public string CurrentUserId { get; set; }
		public string AlbumName { get; set; }
		public int AlbumRating { get; set; }
	}
}