using System;
using System.IO;
using System.Linq;

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
			string[] files = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);

			foreach (string path in files)
			{
				string currentLastMod = File.GetLastWriteTime(path).ToString();

				string storedLastMod = _db.GetStoredTimestamp(path);

				if (currentLastMod == storedLastMod)
				{
					continue;
				}

				try
				{
					string content = string.Join(" ", File.ReadLines(path).Take(3));
					long size = new FileInfo(path).Length;
					string ext = Path.GetExtension(path);

					_db.SaveFile(path, content, currentLastMod, size, ext);
				}
				catch {

				}
			}
		}
	}
}