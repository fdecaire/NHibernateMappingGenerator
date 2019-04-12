using System.Collections.Generic;
using System.Text;
using HelperLibrary;

namespace NHibernateMappingGenerator
{
	public class TableGeneratorMappings
	{
		private string _databaseName;
		private string _connectionString;

		public TableGeneratorMappings(string connectionString, string databaseName)
		{
			_databaseName = databaseName;
			_connectionString = connectionString; 
		}

		private List<string> ReadPrimaryKeyList(string tableName)
		{
			var keyList = new List<string>();

			var query = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name='" + tableName + "' AND CONSTRAINT_NAME LIKE 'PK_%' ORDER BY ORDINAL_POSITION";

			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					keyList.Add(reader["COLUMN_NAME"].ToString());
				}
			}

			return keyList;
		}

		public string EmitCode()
		{
			var @out = new StringBuilder();

			@out.AppendLine("using System;");
			@out.AppendLine("using HelperLibrary;");
			@out.AppendLine("using System.Collections.Generic;");
			@out.AppendLine("");
			@out.AppendLine("namespace NHibernateDataLayer." + _databaseName + ".TableGenerator");
			@out.AppendLine("{");
			@out.AppendLine("\t// DO NOT MODIFY! This code is auto-generated.");
			@out.AppendLine("\tpublic partial class " + _databaseName + "Tables");
			@out.AppendLine("\t{");
			@out.AppendLine("\t\tpublic static string DatabaseName {");
			@out.AppendLine("\t\t\tget ");
			@out.AppendLine("\t\t\t{");
			@out.AppendLine("\t\t\t\treturn \"" + _databaseName + "\";");
			@out.AppendLine("\t\t\t}");
			@out.AppendLine("\t\t}");
			@out.AppendLine("");

			@out.AppendLine("\t\tpublic static List<TableDefinition> TableList = new List<TableDefinition> {");

			string query = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.tables";
			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					@out.Append("\t\t\tnew TableDefinition {");
					@out.Append(EmitTableGenerateCode(reader["TABLE_NAME"].ToString()));
					@out.AppendLine("},");
				}
			}

			@out.AppendLine("\t\t};");
			@out.AppendLine("\t}");
			@out.AppendLine("}");

			return @out.ToString();
		}

		private string EmitTableGenerateCode(string tableName)
		{
			var @out = new StringBuilder();
			var firstTime = true;

			@out.Append("Name=\"" + tableName + "\", ");

			@out.Append("CreateScript=\"CREATE TABLE [dbo].[" + tableName + "](");
			string query = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.COLUMNS WHERE table_name='" + tableName + "' ORDER BY ORDINAL_POSITION";
			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					if (firstTime)
					{
						firstTime = false;
					}
					else
					{
						@out.Append(",");
					}

					@out.Append("[" + reader["COLUMN_NAME"].ToString() + "]");
					@out.Append("[" + reader["DATA_TYPE"].ToString() + "]");

					switch (reader["DATA_TYPE"].ToString().ToLower())
					{
						case "char":
						case "nchar":
						case "varchar":
						case "nvarchar":
						case "varbinary":
							if (reader["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
							{
								@out.Append("(MAX)");
							}
							else
							{
								@out.Append("(" + reader["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")");
							}
							break;
						case "numeric":
						case "money":
							@out.Append("(" + reader["NUMERIC_PRECISION"].ToString());

							if (reader["NUMERIC_SCALE"].ToString() != "")
							{
								@out.Append("," + reader["NUMERIC_SCALE"].ToString());
							}

							@out.Append(")");
							break;
					}

					// output identity field information
					@out.Append(GetIdentitySeedAndIncrementValues(tableName, reader["COLUMN_NAME"].ToString()));

					if (reader["IS_NULLABLE"].ToString() != "YES")
					{
						@out.Append(" NOT NULL");
					}
				}
			}

			// get the primary key
			firstTime = true;
			query = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name='" + tableName + "' AND CONSTRAINT_NAME LIKE 'PK_%' ORDER BY ORDINAL_POSITION";
			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					if (firstTime)
					{
						@out.Append(", CONSTRAINT [" + reader["CONSTRAINT_NAME"].ToString() + "] PRIMARY KEY CLUSTERED (");
						firstTime = false;
					}
					else
					{
						@out.Append(",");
					}

					@out.Append("[" + reader["COLUMN_NAME"].ToString() + "]");

					@out.Append(" ASC");
					
				}
			}

			if (!firstTime)
			{
				@out.Append(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				@out.Append(") ON [PRIMARY]\"");
			}
			else
			{
				@out.Append(")\"");
			}

			return @out.ToString();
		}


		private string GetIdentitySeedAndIncrementValues(string tableName, string fieldName)
		{
			using (var db = new ADODatabaseContext(_connectionString))
			{
				var queryString = "SELECT * " +
								 "FROM " + _databaseName + ".sys.identity_columns AS a INNER JOIN " + _databaseName + ".sys.objects AS b ON a.object_id=b.object_id " +
								 "WHERE LOWER(b.name)='" + tableName.ToLower().Trim() + "' AND LOWER(a.name)='" +
								 fieldName.ToLower().Trim() + "' AND type='U'";

				using (var columnReader = db.ReadQuery(queryString))
				{
					while (columnReader.Read())
					{
						return " IDENTITY(" + columnReader["SEED_VALUE"].ToString() + "," + columnReader["INCREMENT_VALUE"].ToString() + ")";
					}
				}
			}

			return "";
		}
	}
}
