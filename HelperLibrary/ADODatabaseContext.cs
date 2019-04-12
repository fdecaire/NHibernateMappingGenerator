using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HelperLibrary
{
	public class ADODatabaseContext : IDisposable
	{
		private SqlConnection _db;
		private readonly List<ADOParameter> Parameters = new List<ADOParameter>();
		public int CommandTimeout { get; set;}

        private readonly string _Database = "master";
		public string Database => _db.Database;

        public ADODatabaseContext(string connectionString)
		{
			CommandTimeout = 300;

			if (UnitTestHelpers.IsInUnitTest)
			{
                if (connectionString.IndexOf("database", StringComparison.InvariantCultureIgnoreCase) > -1 || connectionString.IndexOf("initial catalog", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    var startPos = connectionString.IndexOf("database", StringComparison.InvariantCultureIgnoreCase);

                    if (startPos == -1)
                    {
                        startPos = connectionString.IndexOf("initial catalog", StringComparison.InvariantCultureIgnoreCase);
                    }
					startPos = connectionString.IndexOf("=", startPos) + 1; // move past the "=" sign, could be spaces in between
					var endPos = connectionString.IndexOf(";", startPos);

                    if (endPos == -1)
                    {
                        endPos = connectionString.Length;
                    }
					if (startPos > -1 && endPos > -1)
					{
                        _Database = connectionString.Substring(startPos, endPos - startPos);
					}
				}

				connectionString = "server=(localdb)\\" + UnitTestHelpers.InstanceName + ";" +
								"database=" + _Database + ";" +
								"Integrated Security=true; " +
								"connection timeout=30";
			}

			_db = new SqlConnection(connectionString);
			_db.Open();
		}

		public ADODatabaseContext(string connectionString, string databaseName)
		{
			CommandTimeout = 300;

            _Database = databaseName;

			if (UnitTestHelpers.IsInUnitTest)
			{
				connectionString = "server=(localdb)\\" + UnitTestHelpers.InstanceName + ";" +
								"database=" + _Database + "; " +
								"Integrated Security=true; " +
								"connection timeout=30";
			}
            else
            {
                if (connectionString.Contains("database"))
                {
                    var startPos = connectionString.IndexOf("database");
                    startPos = connectionString.IndexOf("=", startPos) + 1; // move past the "=" sign, could be spaces in between
                    var endPos = connectionString.IndexOf(";", startPos);
                    if (startPos > -1 && endPos > -1)
                    {
						connectionString = connectionString.Substring(0, startPos) + _Database + connectionString.Substring(endPos, connectionString.Length - endPos);
                    }
                }
                else
                {
					connectionString += ";database=" + _Database;
                }
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

		public DataSet ReadDataSet(string queryString)
		{
			var myCommand = new SqlCommand(queryString, _db);
			myCommand.CommandTimeout = CommandTimeout;
			var datasetAdapter = new SqlDataAdapter(myCommand);

			var ds = new DataSet();

			foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			datasetAdapter.Fill(ds);

			return ds;
		}

		public ADOReader ReadQuery(string queryString)
		{
            var myCommand = new SqlCommand(queryString, _db) {CommandTimeout = CommandTimeout};

            foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			return new ADOReader(myCommand);
		}

		public void ExecuteNonQuery(string queryString)
		{
            var myCommand = new SqlCommand(queryString, _db) {CommandTimeout = CommandTimeout};

            foreach (var param in Parameters)
			{
				myCommand.Parameters.Add(param.Name, param.Type);
				myCommand.Parameters[param.Name].Value = param.Value;
			}

			Parameters.Clear();

			myCommand.ExecuteNonQuery();
		}

        public int ExecuteScalar(string queryString)
        {
            var myCommand = new SqlCommand(queryString, _db) {CommandTimeout = CommandTimeout};

            foreach (var param in Parameters)
            {
                myCommand.Parameters.Add(param.Name, param.Type);
                myCommand.Parameters[param.Name].Value = param.Value;
            }

            Parameters.Clear();

            int result = int.Parse(myCommand.ExecuteScalar().ToString());
            return result;
        }

		public void AddParameter(string name, object value, SqlDbType type)
		{
			Parameters.Add(new ADOParameter
			{
				Name = name,
				Value = value,
				Type = type
			});
		}

		public void BulkInsert(DataTable detailTable)
		{
			using (var s = new SqlBulkCopy(_db))
			{
				s.DestinationTableName = detailTable.TableName;
				s.BulkCopyTimeout = CommandTimeout;

				foreach (var column in detailTable.Columns)
				{
					s.ColumnMappings.Add(column.ToString(), column.ToString());
				}

				s.WriteToServer(detailTable);
			}
		}

		public void ExecuteStoredProcedure(string queryString)
		{
			using (var myCommand = new SqlCommand(queryString, _db))
			{
				myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = CommandTimeout;

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
			using (var myCommand = new SqlCommand(queryString, _db))
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
			using (var myCommand = new SqlCommand(queryString, _db))
			{
				myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandTimeout = CommandTimeout;
				var sqlDataAdapter = new SqlDataAdapter(myCommand);
				var ds = new DataSet();

				foreach (var param in Parameters)
				{
					myCommand.Parameters.Add(param.Name, param.Type);
					myCommand.Parameters[param.Name].Value = param.Value;
				}

				Parameters.Clear();

				sqlDataAdapter.Fill(ds);

				return ds;
			}
		}
	
        public void BulkInsertKeepIdentityField(DataTable detailTable)
        {
            using (var s = new SqlBulkCopy(_db.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
            {
                s.DestinationTableName = detailTable.TableName;
                s.BulkCopyTimeout = CommandTimeout; // set this for overkill;

				foreach (var column in detailTable.Columns)
				{
					s.ColumnMappings.Add(column.ToString(), column.ToString());
				}

                s.WriteToServer(detailTable);
            }
        }

        public void InsertDataColumn(DataTable detailTable, string columnName, string dataType)
        {
            var dataColumn = new DataColumn
            {
                DataType = Type.GetType(dataType),
                ColumnName = columnName
            };
            detailTable.Columns.Add(dataColumn);
        }
    }
}
