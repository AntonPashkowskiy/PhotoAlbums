﻿using System;
using System.Collections.Generic;

namespace Entities.Entities
{
	public class Photo
	{
		public int Id { get; set; }
		public string SmallPhotoUrl { get; set; }
		public string MediumPhotoUrl { get; set; }
		public string FullPhotoUrl { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public DateTime CreationDate { get; set; }
		public string AuthorId { get; set; }
		public int AlbumId { get; set; }

		// navigation properties for Entity Framework
		public virtual PhotoAlbum PhotoAlbum { get; set; }
		public virtual User Author { get; set; }
		public virtual ICollection<PhotoComment> Comments { get; set; }
	}
}
