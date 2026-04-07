using System.Collections.Generic;

namespace SDAssig
{
	public interface IDatabaseService
	{
		void Initialize();
		void ClearDatabase();
		void SaveFile(string path, string content, string lastModified, long size, string extension);
		List<SearchResult> SearchFiles(string query);
		string GetStoredTimestamp(string path);
	}

	public class SearchResult
	{
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public string Preview { get; set; }
		public override string ToString() => $"{FileName} | {Preview}";
	}
}