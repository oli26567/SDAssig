using System.Collections.Generic;

namespace SDAssig
{
	public interface IDatabaseService
	{
		void Initialize(); 
		void ClearDatabase();
		void SaveFile(string path, string content, string lastModified);
		List<string> SearchFiles(string query);
	}
}