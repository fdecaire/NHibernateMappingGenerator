using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Helpers
{
	public class InsertQueryGenerator
	{
		public List<FieldDefinition> Fields = new List<FieldDefinition>();
		private string _tableName;
		private string _connectionString;
		private string _databaseName;
		private string _IdentityFieldName;
		private bool _DataContainsPrimaryKey;

		public InsertQueryGenerator(string connectionString, string databaseName)
		{
			_connectionString = connectionString;
			_databaseName = databaseName;
		}

		public void InsertData(XmlNode e)
		{
			_tableName = e.Name.ToString();

			_IdentityFieldName = ReadTableIdentityFieldName();
			_DataContainsPrimaryKey = false;

			BuildFieldsForTable();

			// iterate through the fields and collect the data
            var children = e.ChildNodes;

			if (children.Count > 0)
			{
				// parse the xml child elements
                foreach (XmlNode fields in children)
				{
					string fieldName = fields.Name.ToString().ToLower();
                    string fieldData = fields.InnerText;

					var record = Fields.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
					if (record != null)
					{
						record.Value = fieldData;

						if (fieldName.ToLower() == _IdentityFieldName.ToLower())
						{
							_DataContainsPrimaryKey = true;
						}
					}
				}
			}
			else
			{
				// try to parse the xml attributes
				var childElements = e.Attributes;
                foreach (XmlAttribute fields in childElements)
				{
					string fieldName = fields.Name.ToString().ToLower();
					string fieldData = fields.Value;

					var record = Fields.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
					if (record != null)
					{
						record.Value = fieldData;

						if (fieldName.ToLower() == _IdentityFieldName.ToLower())
						{
							_DataContainsPrimaryKey = true;
						}
					}
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
				if (_DataContainsPrimaryKey)
				{
					db.ExecuteNonQuery("SET IDENTITY_INSERT " + _databaseName + ".." + _tableName + " ON");
				}
				db.ExecuteNonQuery(query);
				if (_DataContainsPrimaryKey)
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
				_IdentityFieldName = ReadTableIdentityFieldName();
				_DataContainsPrimaryKey = false;

				BuildFieldsForTable();

				foreach (var fieldItem in tableItem)
				{
					string fieldName = fieldItem.Name.ToLower();
					string fieldData = fieldItem.Value;

					var record = Fields.Where(x => x.Name.ToLower() == fieldName).FirstOrDefault();
					if (record != null)
					{
						record.Value = fieldData;

						if (fieldName.ToLower() == _IdentityFieldName.ToLower())
						{
							_DataContainsPrimaryKey = true;
						}
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

		private string ReadTableIdentityFieldName()
		{
			string query = @"
					SELECT * FROM " + _databaseName + @".sys.identity_columns AS a 
					INNER JOIN " + _databaseName + @".sys.objects AS b ON a.object_id=b.object_id 
					WHERE 
						LOWER(b.name)='" + _tableName + @"' AND 
						type='U'";

			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					return reader["name"].ToString();
				}
			}
			return "";
		}
	}
}
