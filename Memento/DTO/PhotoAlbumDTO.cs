﻿namespace Memento.DTO
{
	public class PhotoAlbumDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public string PresentationPhotoUrl { get; set; }
		public string[] Tags { get; set; }
	}
}