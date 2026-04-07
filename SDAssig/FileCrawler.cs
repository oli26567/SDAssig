using System;
using System.IO;
using System.Linq;

namespace SDAssig
{
	public class FileCrawler
	{
		private readonly IDatabaseService _db;

		public FileCrawler(IDatabaseService database) => _db = database;

		public void IndexDirectory(string targetDirectory)
		{
			string[] allFiles = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);

			foreach (string filePath in allFiles)
			{
				try
				{
					string ext = Path.GetExtension(filePath).ToLower();
					if (ext == ".txt" || ext == ".cs" || ext == ".md")
					{
						string currentMod = File.GetLastWriteTime(filePath).ToString();
						string storedMod = _db.GetStoredTimestamp(filePath);

						if (currentMod != storedMod)
						{
							string preview = string.Join(" ", File.ReadLines(filePath).Take(3));
							long size = new FileInfo(filePath).Length;

							_db.SaveFile(filePath, preview, currentMod, size, ext);
						}
					}
				}
				catch (Exception)
				{

				}
			}
		}
	}
}