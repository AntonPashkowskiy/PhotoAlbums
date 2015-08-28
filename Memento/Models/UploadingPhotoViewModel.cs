using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memento.Models
{
	public class UploadingPhotoViewModel
	{
		public int AlbumId { get; set; }

		[Display(Name = "Photo description:")]
		public string PhotoDescription { get; set; }
	}
}