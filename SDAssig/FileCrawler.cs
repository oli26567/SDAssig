using System;
using System.IO;
using System.Linq;
using SDAssig;

namespace SDAssig
{
	public class FileCrawler
	{
		private DatabaseManager db;

		public FileCrawler(DatabaseManager database)
		{
			db = database;
		}

		public void IndexDirectory(string targetDirectory)
		{
			var files = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories)
				.Where(f => f.EndsWith(".txt") || f.EndsWith(".cs") || f.EndsWith(".md")).ToList();

			foreach (string filePath in files)
			{
				try
				{
					string content = string.Join(" ", File.ReadLines(filePath).Take(3));

					string lastMod = File.GetLastWriteTime(filePath).ToString();
					db.SaveFile(filePath, content, lastMod);

				}
				catch (Exception ex)
				{

				}
			}
		}

	}
}