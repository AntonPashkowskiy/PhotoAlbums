using System.ComponentModel.DataAnnotations;

namespace Memento.Models
{
	public class PersonalRoomViewModel
	{
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Display(Name = "Login")]
		public string Login { get; set; }

		[Display(Name = "Number of albums")]
		public int NumberOfAlbums { get; set; }

		[Display(Name = "Number of photos")]
		public int NumberOfPhotos { get; set; }

		[Display(Name = "Overall albums rating")]
		public int OverallAlbumsRating { get; set; }

		[Display(Name = "Overall photos rating")]
		public int OverallPhotosRating { get; set; }
	}
}