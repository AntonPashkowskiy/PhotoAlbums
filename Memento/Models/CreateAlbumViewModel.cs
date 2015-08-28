using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memento.Models
{
	public class CreateAlbumViewModel
	{
		[Required]
		[Display(Name = "Album name")]
		public string AlbumName { get; set; }

		[Display(Name = "Tags")]
		public string Tags { get; set; }

		[Display(Name = "Album description")]
		public string AlbumDescription { get; set; }

		[Display(Name = "Is private album")]
		public bool IsPrivate { get; set; }
	}
}