using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Memento.Environment.DataManagement
{
	public static class UserDataManagerExtention
	{
		public static void CreateUserDataDirectory(this HttpContextBase httpContext, string userName)
		{
			string rootPath = httpContext.Server.MapPath("~/UserData/");
			string userDirectoryPath = Path.Combine(rootPath, userName);

			if (!Directory.Exists(userDirectoryPath))
			{
				string avatarDirectoryPath = Path.Combine(userDirectoryPath, GetSpecialDirectoryName(DirectoryType.AvatarDirectory));
				string photoDirectoryPath = Path.Combine(userDirectoryPath,  GetSpecialDirectoryName(DirectoryType.PhotoDirectory));

				Directory.CreateDirectory(userDirectoryPath);
				Directory.CreateDirectory(avatarDirectoryPath);
				Directory.CreateDirectory(photoDirectoryPath);
			}
		}

		public static ServerPath GetFilePath(this HttpContextBase httpContext, DirectoryType type, string userName, string extention)
		{
			string rootPath = httpContext.Server.MapPath("~/UserData/");
			string userDirectoryPath = Path.Combine(rootPath, userName);
			string specialDirectoryName = GetSpecialDirectoryName(type);
			string specialDirectoryPath = Path.Combine(userDirectoryPath, specialDirectoryName);

			if (!Directory.Exists(userDirectoryPath) || !Directory.Exists(specialDirectoryPath))
			{
				throw new DirectoryNotFoundException("User directory don't exist.");
			}

			string name = Path.GetRandomFileName();
			string newFileName = Path.ChangeExtension(name, extention);

			return new ServerPath()
			{
				AbsolutePath = Path.Combine(specialDirectoryPath, newFileName),
				LocalPath = Path.Combine("/UserData", userName, specialDirectoryName, newFileName)
			};
		}

		private static string GetSpecialDirectoryName(DirectoryType type)
		{
			switch (type)
			{
				case DirectoryType.AvatarDirectory:
					return "Avatar";

				case DirectoryType.PhotoDirectory:
				default:
					return "Photo";
			}
		}
	}
}