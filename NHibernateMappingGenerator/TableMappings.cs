using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HelperLibrary;

namespace NHibernateMappingGenerator
{
	public class TableMappings
	{
		private string _databaseName;
		private string _tableName;
		private string _connectionString;

		public List<FieldMapping> Fields = new List<FieldMapping>();
		public List<string> KeyList = new List<string>();

		public TableMappings(string connectionString, string databaseName, string tableName)
		{
			_databaseName = databaseName;
			_tableName = tableName;
			_connectionString = connectionString;

			// read all fields in the primary key
			ReadPrimaryKeyList();

			// read all fields
			string query = "SELECT * FROM " + databaseName + ".INFORMATION_SCHEMA.COLUMNS WHERE table_name='" + tableName + "' ORDER BY ORDINAL_POSITION";
			using (var db = new ADODatabaseContext(connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					FieldMapping fieldMapping = new FieldMapping
						{
							FieldName = reader["COLUMN_NAME"].ToString(),
							DataType = reader["DATA_TYPE"].ToString(),
							Nullable = (reader["IS_NULLABLE"].ToString() == "YES"),
							Precision = reader["NUMERIC_PRECISION"].ToString(),
							PrecisionRadix = reader["NUMERIC_PRECISION_RADIX"].ToString(),
							StringLength = reader["CHARACTER_MAXIMUM_LENGTH"].ToString()
						};

						Fields.Add(fieldMapping);
				}
			}
		}

		private void ReadPrimaryKeyList()
		{
			string query = "SELECT * FROM " + _databaseName + ".INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE table_name='" + _tableName + "' AND CONSTRAINT_NAME LIKE 'PK_%' ORDER BY ORDINAL_POSITION";

			using (var db = new ADODatabaseContext(_connectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					KeyList.Add(reader["COLUMN_NAME"].ToString());
				}
			}
		}

		public string EmitCode()
		{
			var @out = new StringBuilder();

			// make sure tablename is not a reserved word
			NormalizeTableNameText();

			@out.AppendLine("using FluentNHibernate.Mapping;");
			@out.AppendLine("using System;");
			@out.AppendLine("");
			@out.AppendLine("namespace NHibernateDataLayer." + _databaseName + ".Tables");
			@out.AppendLine("{");
			@out.AppendLine("\t// DO NOT MODIFY! This code is auto-generated.");
			@out.AppendLine("\tpublic partial class " + _tableName + "");
			@out.AppendLine("\t{");

			//list all fields first
			foreach (var field in Fields)
			{
				string nullSeperator = "";
				if (field.Nullable && field.CSharpDataType != "string")
				{
					nullSeperator = "? ";
				}

				@out.AppendLine("\t\tpublic virtual " + field.CSharpDataType + " " + nullSeperator + field.FieldName + " { get; set; }");
			}

			// emit special code for compound keys
			if (KeyList.Count > 1)
			{
				@out.AppendLine("");
				@out.AppendLine("\t\tpublic override bool Equals(object obj)");
				@out.AppendLine("\t\t{");
				@out.AppendLine("\t\t\tif (obj == null || GetType() != obj.GetType())");
				@out.AppendLine("\t\t\t{");
				@out.AppendLine("\t\t\t\treturn false;");
				@out.AppendLine("\t\t\t}");
				@out.AppendLine("");
				@out.AppendLine("\t\t\t" + _tableName + " that = (" + _tableName + ")obj;");
				@out.AppendLine("");

				@out.Append("\t\t\treturn ");
				for (int i = 0; i < KeyList.Count; i++)
				{
					if (i > 0)
					{
						@out.Append("\t\t\t\t");
					}

					@out.Append("this." + KeyList[i] + " == that." + KeyList[i] + "");

					if (i < KeyList.Count - 1)
					{
						@out.AppendLine(" &&");
					}
					else
					{
						@out.AppendLine(";");
					}
				}

				@out.AppendLine("\t\t}");
				@out.AppendLine("");


				@out.AppendLine("\t\tpublic override int GetHashCode()");
				@out.AppendLine("\t\t{");
				@out.Append("\t\t\treturn ");

				for (int i = 0; i < KeyList.Count; i++)
				{
					if (i > 0)
					{
						@out.Append("\t\t\t\t");
					}

					@out.Append(KeyList[i] + ".GetHashCode()");

					if (i < KeyList.Count - 1)
					{
						@out.AppendLine(" ^");
					}
					else
					{
						@out.AppendLine(";");
					}
				}

				@out.AppendLine("\t\t}");
			}

			// create the mapping section here
			@out.AppendLine("\t}");

			@out.AppendLine("");
			@out.AppendLine("\tpublic class " + _tableName + "Map : ClassMap<" + _tableName + ">");
			@out.AppendLine("\t{");
			@out.AppendLine("\t\tpublic " + _tableName + "Map()");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tTable(\"" + _databaseName + ".." + _tableName + "\");");

			// index first, need to find out which field(s) are primary key and do a composite key if necessary
			string primaryKeyList = "";
			if (KeyList.Count > 0)
			{
				primaryKeyList = "AND COLUMN_NAME NOT IN (";
				for (int i = 0; i < KeyList.Count; i++)
				{
					if (i > 0)
					{
						primaryKeyList += ",";
					}

					primaryKeyList += "'" + KeyList[i] + "'";
				}
				primaryKeyList += ")";
			}

			if (KeyList.Count == 1)
			{
				// one primary key
				@out.Append("\t\t\tId(u => u." + KeyList[0] + ")");

				// test to see if this is an identity
				if (IsThisAnIdentityColumn(KeyList[0]))
				{
					@out.Append(".GeneratedBy.Identity()");
				}
				else
				{
					@out.Append(".GeneratedBy.Assigned()");
				}

				@out.AppendLine(".Not.Nullable();");
			}
			else if (KeyList.Count > 1)
			{
				// composite key
				@out.AppendLine("\t\t\tCompositeId()");
				for (int i = 0; i < KeyList.Count; i++)
				{
					string endString = "";
					if (i == KeyList.Count - 1)
					{
						endString = ";";
					}
					@out.AppendLine("\t\t\t\t.KeyProperty(u => u." + KeyList[i] + ")" + endString + "");
				}
			}

			// output remaining fields
			foreach (var field in Fields)
			{
				if (!KeyList.Contains(field.FieldName))
				{
					@out.Append(field.EmitFieldMapping());
				}
			}

			@out.AppendLine("\t\t}");

			@out.AppendLine("\t}");
			@out.AppendLine("}");

			return @out.ToString();
		}

		private void NormalizeTableNameText()
		{
			//TODO: convert this into a dictionary and include var types like "string", "int" etc.
			switch (_tableName)
			{
				case "class":
					_tableName = "Class";
					break;
				case "public":
					_tableName = "Public";
					break;
				case "partial":
					_tableName = "Partial";
					break;
			}
		}

		private bool IsThisAnIdentityColumn(string fieldName)
		{
			using (var db = new ADODatabaseContext(_connectionString))
			{
				string queryString = "SELECT * " +
														 "FROM " + _databaseName + ".sys.identity_columns AS a INNER JOIN " + _databaseName + ".sys.objects AS b ON a.object_id=b.object_id " +
														 "WHERE LOWER(b.name)='" + _tableName.ToLower().Trim() + "' AND LOWER(a.name)='" +
														 fieldName.ToLower().Trim() + "' AND type='U'";

				using (var columnReader = db.ReadQuery(queryString))
				{
					if (columnReader.HasRows)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
