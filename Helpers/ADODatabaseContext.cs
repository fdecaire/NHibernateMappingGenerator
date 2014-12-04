using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Helpers
{
	public class ADODatabaseContext : IDisposable
	{
		private SqlConnection _db;
		public string Database
		{
			get
			{
				return _db.Database;
			}
		}

		public ADODatabaseContext(string connectionString)
		{
			if (UnitTestHelpers.IsInUnitTest)
			{
				string database = "master";

				if (connectionString.Contains("database"))
				{
					int startPos = connectionString.IndexOf("database");
					startPos = connectionString.IndexOf("=", startPos) + 1; // move past the "=" sign, could be spaces in between
					int endPos = connectionString.IndexOf(";", startPos);

					if (startPos > -1 && endPos > -1)
					{
						database = connectionString.Substring(startPos, endPos - startPos);
					}
				}

				connectionString = "server=(localdb)\\" + UnitTestHelpers.InstanceName + ";" +
								"Trusted_Connection=yes;" +
								"database=" + database + ";"+
								"Integrated Security=true; " +
								"connection timeout=30";
			}

			_db = new SqlConnection(connectionString);
			_db.Open();
		}

		public ADODatabaseContext(string connectionString, string databaseName)
		{
			if (UnitTestHelpers.IsInUnitTest)
			{
				connectionString = "server=(localdb)\\" + UnitTestHelpers.InstanceName + ";" +
								"Trusted_Connection=yes;" +
								"database=" + databaseName + "; " +
								"Integrated Security=true; " +
								"connection timeout=30";
			}

			_db = new SqlConnection(connectionString);
			_db.Open();
		}

		public void Dispose()
		{
			Dispose(true);
		}

		~ADODatabaseContext()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_db != null)
				{
					_db.Close();
					_db.Dispose();
					_db = null;
				}
			}
		}

		public SqlDataReader ReadQuery(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			return myCommand.ExecuteReader();
		}

		public void ExecuteNonQuery(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			myCommand.ExecuteNonQuery();
		}

		public int ExecuteScaler(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			return int.Parse(myCommand.ExecuteScalar().ToString());
		}
	}
}
