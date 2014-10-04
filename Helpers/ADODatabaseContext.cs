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

		public ADODatabaseContext(string connectionString)
		{
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

		public int ExecuteNonQuery(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			return myCommand.ExecuteNonQuery();
		}
	}
}
