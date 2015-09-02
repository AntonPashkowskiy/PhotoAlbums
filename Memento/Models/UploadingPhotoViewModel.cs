using System.ComponentModel.DataAnnotations;

namespace Memento.Models
{
	public class UploadingPhotoViewModel
	{
		public int AlbumId { get; set; }

		[Display(Name = "Photo description:")]
		public string PhotoDescription { get; set; }
	}
}