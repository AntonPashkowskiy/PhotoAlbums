using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public struct UserStatistic
	{
		public int NumberOfAlbums { get; set; }
		public int NumberOfPhotos { get; set; }
		public int OverallRatingOfAlbums { get; set; }
		public int OverallRatingOfPhotos { get; set; }
	}
}
