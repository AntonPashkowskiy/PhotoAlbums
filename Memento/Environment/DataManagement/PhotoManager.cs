using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Memento.Environment.DataManagement
{
	public static class PhotoManager
	{
		public static Size GetThumbailSize(PhotoSize size)
		{
			switch (size)
			{
				case PhotoSize.Small:
					return new Size(100, 100);

				case PhotoSize.AvatarStandart:
					return new Size(500, 500);
				
				case PhotoSize.Standart:
				default:
					return new Size(255, 255);
			}
		}
	}

	public enum PhotoSize
	{
		Small,
		Standart,
		AvatarStandart
	}
}