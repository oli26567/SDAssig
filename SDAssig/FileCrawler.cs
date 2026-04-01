using System;
using System.IO;
using System.Linq;
using SDAssig;

namespace SDAssig
{
	public class FileCrawler
	{
		private readonly IDatabaseService _db;

		public FileCrawler(IDatabaseService database)
		{
			_db = database;
		}

		public void IndexDirectory(string targetDirectory)
		{
			string[] allFiles = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);

			List<string> filteredFiles = new List<string>();

			foreach (string filePath in allFiles)
			{
				if (filePath.EndsWith(".txt") || filePath.EndsWith(".cs") || filePath.EndsWith(".md"))
				{
					filteredFiles.Add(filePath);
				}
			}

			foreach (string filePath in filteredFiles)
			{
				try
				{
					string content = string.Join(" ", File.ReadLines(filePath).Take(3));
					string lastMod = File.GetLastWriteTime(filePath).ToString();
					_db.SaveFile(filePath, content, lastMod);
				}
				catch (Exception ex)
				{

				}
			}
		}

	}
}