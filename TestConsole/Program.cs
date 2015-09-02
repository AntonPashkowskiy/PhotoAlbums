using System;
using System.Text.RegularExpressions;

namespace TestConsole
{
	class Program
	{
		static void Main()
		{
			const string pattern = @"#(\w)+";
			const string target = "#nature. #family. #poison";
			var tagRegexp = new Regex(pattern);

			foreach (var match in tagRegexp.Matches(target))
			{
				Console.WriteLine(match.ToString());
			}
			Console.ReadKey();
		}
	}
}
