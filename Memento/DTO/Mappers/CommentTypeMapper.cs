using Entities.Entities;

namespace Memento.DTO.Mappers
{
	public static class CommentTypeMapper
	{
		public static CommentType GetCommentType(int resourseType)
		{
			switch (resourseType)
			{
				case 1:
					return CommentType.AlbumComment;
				case 2:
				default:
					return CommentType.PhotoComment;
			}
		}

		public static int GetResourseType(CommentType type)
		{
			switch (type)
			{
				case CommentType.AlbumComment:
					return 1;
				case CommentType.PhotoComment:
				default:
					return 2;
			}
		}
	}
}