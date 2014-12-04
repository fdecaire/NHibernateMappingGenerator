using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helpers
{
	public class InsertQueryGenerator
	{
		public List<FieldDefinition> Fields = new List<FieldDefinition>();
		private string _tableName;
		private string _connectionString;
		private string _databaseName;
		private bool _HasIdentity;

		public InsertQueryGenerator(string connectionString, string databaseName)
		{
			_connectionString = connectionString;
			_databaseName = databaseName;
		}

		public void InsertData(XElement e)
		{
			_tableName = e.Name.ToString();

			_HasIdentity = DoesTableHaveIdentityField();

			BuildFieldsForTable();

			// iterate through the fields and collect the data
			var children = e.Elements();
			foreach (var fields in children)
			{
				string fieldName = fields.Name.ToString().ToLower();
				string fieldData = fields.Value;

				var record = Fields.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
				if (record != null)
				{
					record.Value = fieldData;
				}
			}

			BuildQueryInsertData();
		}

		private void BuildQueryInsertData()
		{
			string query = "";

			foreach (var field in Fields)
			{
				if (field.Value != null)
				{
					query += field.Name + ",";
				}
			}

			if (query[query.Length - 1] == ',')
			{
				query = query.Substring(0, query.Length - 1);
			}

			if (query == "")
			{
				return;
			}

			query = "INSERT INTO " + _databaseName + ".." + _tableName + " (" + query + ") VALUES (";

			foreach (var field in Fields)
			{
				if (field.Value != null)
				{
					query += field.Value + ",";
				}
			}

			if (query[query.Length - 1] == ',')
			{
				query = query.Substring(0, query.Length - 1);
			}

			query += ")";

			using (var db = new ADODatabaseContext(_connectionString, _databaseName))
			{
				if (_HasIdentity)
				{
					db.ExecuteNonQuery("SET IDENTITY_INSERT " + _databaseName + ".." + _tableName + " ON");
				}
				db.ExecuteNonQuery(query);
				if (_HasIdentity)
				{
					db.ExecuteNonQuery("SET IDENTITY_INSERT " + _databaseName + ".." + _tableName + " OFF");
				}
			}
		}

		public void InsertJsonData(dynamic jsonTableData)
		{
			// must be a list of tables
			_tableName = jsonTableData.Name;

			foreach (var tableItem in jsonTableData.Value)
			{
				_HasIdentity = DoesTableHaveIdentityField();

				BuildFieldsForTable();

				foreach (var fieldItem in tableItem)
				{
					string fieldName = fieldItem.Name.ToLower();
					string fieldData = fieldItem.Value;

					var record = Fields.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
					if (record != null)
					{
						record.Value = fieldData;
					}
				}

				BuildQueryInsertData();
			}
		}

		private void BuildFieldsForTable()
		{
			Fields.Clear();
			using (var db = new ADODatabaseContext(_connectionString, _databaseName))
			{
				string columnQuery = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.columns WHERE TABLE_NAME='" + _tableName + "'";
				var reader = db.ReadQuery(columnQuery);
				while (reader.Read())
				{
					Fields.Add(new FieldDefinition
					{
						Name = reader["COLUMN_NAME"].ToString(),
						Type = reader["DATA_TYPE"].ToString()
					});
				}
			}
		}

		private bool DoesTableHaveIdentityField()
		{
			string query = @"
					SELECT * FROM " + _databaseName + @".sys.identity_columns AS a 
					INNER JOIN " + _databaseName + @".sys.objects AS b ON a.object_id=b.object_id 
					WHERE 
						LOWER(b.name)='" + _tableName + @"' AND 
						type='U'";

			using (var db = new ADODatabaseContext(_connectionString, _databaseName))
			{
				var reader = db.ReadQuery(query);
				if (reader.HasRows)
				{
					return true;
				}
			}
			return false;
		}
	}
}
