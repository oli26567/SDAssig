using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace SDAssig
{
	public class DatabaseManager : IDatabaseService
	{
		private string dbPath;

		public DatabaseManager()
		{
			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SDAssig");
			if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
			dbPath = Path.Combine(folder, "search_engine.db");
		}

		public void Initialize()
		{
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Files (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FilePath TEXT UNIQUE,
                        FileName TEXT,
                        Content TEXT,
                        LastModified TEXT,
                        FileSize INTEGER,
                        Extension TEXT
                    );";
				command.ExecuteNonQuery();
			}
		}

		public string GetStoredTimestamp(string path)
		{
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "SELECT LastModified FROM Files WHERE FilePath = @p";
				command.Parameters.AddWithValue("@p", path);
				object result = command.ExecuteScalar();
				if (result != null)
				{
					return result.ToString();
				}
				else
				{
					return null;
				}
			}
		}

		public void SaveFile(string path, string content, string lastMod, long size, string ext)
		{
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = @"INSERT OR REPLACE INTO Files 
                    (FilePath, FileName, Content, LastModified, FileSize, Extension) 
                    VALUES (@p, @n, @c, @m, @s, @e)";

				command.Parameters.AddWithValue("@p", path);
				command.Parameters.AddWithValue("@n", Path.GetFileName(path));
				command.Parameters.AddWithValue("@c", content);
				command.Parameters.AddWithValue("@m", lastMod);
				command.Parameters.AddWithValue("@s", size);
				command.Parameters.AddWithValue("@e", ext);
				command.ExecuteNonQuery();
			}
		}

		public List<SearchResult> SearchFiles(string query)
		{
			var results = new List<SearchResult>();
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "SELECT FileName, FilePath, Content FROM Files WHERE Content LIKE @q OR FileName LIKE @q";
				command.Parameters.AddWithValue("@q", "%" + query + "%");

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						results.Add(new SearchResult
						{
							FileName = reader.GetString(0),
							FilePath = reader.GetString(1),
							Preview = reader.GetString(2)
						});
					}
				}
			}
			return results;
		}

		public void ClearDatabase()
		{
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "DELETE FROM Files";
				command.ExecuteNonQuery();
			}
		}
	}
}