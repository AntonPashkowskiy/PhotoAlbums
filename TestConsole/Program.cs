using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using EFDataProvider;
using Entities;
using Entities.Interfaces;
using EFDataProvider.Realization;
using ServiceLayer;
using System.Text.RegularExpressions;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			string pattern = @"#(\w)+";
			string target = "#nature. #family. #poison";
			Regex tagRegexp = new Regex(pattern);

			foreach (var match in tagRegexp.Matches(target))
			{
				Console.WriteLine(match.ToString());
			}
			Console.ReadKey();
		}
	}
}
