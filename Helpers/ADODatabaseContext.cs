using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Helpers
{
	public class ADODatabaseContext : IDisposable
	{
		private SqlConnection _db;
		private List<ADOParameter> Parameters = new List<ADOParameter>();
		public int CommandTimeOut { get; set;}

		public string Database
		{
			get
			{
				return _db.Database;
			}
		}

		public ADODatabaseContext(string connectionString)
		{
			CommandTimeOut = 300;

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
			CommandTimeOut = 300;

			//TODO: should replace database attribute, or add to the connection string for non-unit test scheme

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
					Parameters = null;
				}
			}
		}

		public DataSet ReadDataSet(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			myCommand.CommandTimeout = CommandTimeOut;
			SqlDataAdapter datasetAdapter = new SqlDataAdapter(myCommand);

			DataSet ds = new DataSet();

			foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			datasetAdapter.Fill(ds);

			return ds;
		}

		public SqlDataReader ReadQuery(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			myCommand.CommandTimeout = CommandTimeOut;

			foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			return myCommand.ExecuteReader();
		}

		public void ExecuteNonQuery(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			myCommand.CommandTimeout = CommandTimeOut;

			foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			myCommand.ExecuteNonQuery();
		}

		public int ExecuteScaler(string queryString)
		{
			SqlCommand myCommand = new SqlCommand(queryString, _db);
			myCommand.CommandTimeout = CommandTimeOut;

			foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			return int.Parse(myCommand.ExecuteScalar().ToString());
		}

		public void AddParameter(string Name, object Value, SqlDbType Type)
		{
			Parameters.Add(new ADOParameter
			{
				Name = Name,
				Value = Value,
				Type = Type
			});
		}

		public void BulkInsert(DataTable DetailTable)
		{
			using (SqlBulkCopy s = new SqlBulkCopy(_db))
			{
				s.DestinationTableName = DetailTable.TableName;
				s.BulkCopyTimeout = CommandTimeOut;

				foreach (var column in DetailTable.Columns)
				{
					s.ColumnMappings.Add(column.ToString(), column.ToString());
				}

				s.WriteToServer(DetailTable);
			}
		}


		public void ExecuteStoredProcedure(string queryString)
		{
			using (SqlCommand myCommand = new SqlCommand(queryString, _db))
			{
				myCommand.CommandType = CommandType.StoredProcedure;

				foreach (var param in Parameters)
				{
					myCommand.Parameters.Add(param.Name, param.Type);
					myCommand.Parameters[param.Name].Value = param.Value;
				}

				Parameters.Clear();

				myCommand.ExecuteNonQuery();
			}
		}

		public string ExecuteStoredProcedureWReturnValue(string queryString)
		{
			using (SqlCommand myCommand = new SqlCommand(queryString, _db))
			{
				myCommand.CommandType = CommandType.StoredProcedure;

				foreach (var param in Parameters)
				{
					myCommand.Parameters.Add(param.Name, param.Type);
					myCommand.Parameters[param.Name].Value = param.Value;
				}

				Parameters.Clear();

				return myCommand.ExecuteScalar().ToString();
			}
		}

		public DataSet StoredProcedureSelect(string queryString)
		{
			using (SqlCommand myCommand = new SqlCommand(queryString, _db))
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter DA = new SqlDataAdapter(myCommand);
				DataSet ds = new DataSet();

				foreach (var param in Parameters)
				{
					myCommand.Parameters.Add(param.Name, param.Type);
					myCommand.Parameters[param.Name].Value = param.Value;
				}

				Parameters.Clear();

				DA.Fill(ds);

				return ds;
			}
		}
	}
}
