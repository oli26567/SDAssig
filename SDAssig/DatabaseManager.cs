using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;

namespace SDAssig
{
	public class DatabaseManager : IDatabaseService
	{
		private string dbPath = "C:\\An3\\SD\\search_engine.db";

		public void Initialize()
		{
			if (!Directory.Exists("C:\\An3\\SD"))
			{
				Directory.CreateDirectory("C:\\An3\\SD");
			}

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
                        LastModified TEXT
                    );";
				command.ExecuteNonQuery();
			}
		}

		public void ClearDatabase()
		{
			using (var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=C:\\An3\\SD\\search_engine.db"))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "DELETE FROM Files";
				command.ExecuteNonQuery();
			}
		}

		public void SaveFile(string path, string content, string lastModified)
		{
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "INSERT OR REPLACE INTO Files (FilePath, FileName, Content, LastModified) VALUES (@p, @n, @c, @m)";
				command.Parameters.AddWithValue("@p", path);
				command.Parameters.AddWithValue("@n", Path.GetFileName(path));
				command.Parameters.AddWithValue("@c", content);
				command.Parameters.AddWithValue("@m", lastModified);
				command.ExecuteNonQuery();
			}
		}

		public List<string> SearchFiles(string query)
		{
			var results = new List<string>();
			using (var connection = new SqliteConnection("Data Source=" + dbPath))
			{
				connection.Open();
				var command = connection.CreateCommand();

				if (query.ToLower() == "all")
				{
					command.CommandText = "SELECT FileName, Content FROM Files";
				}
				else
				{
					command.CommandText = "SELECT FileName, Content FROM Files WHERE Content LIKE @q OR FileName LIKE @q";
					command.Parameters.AddWithValue("@q", "%" + query + "%");
				}

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						results.Add(reader.GetString(0) + " | Preview: " + reader.GetString(1));
					}
				}
			}
			return results;
		}
	}
}