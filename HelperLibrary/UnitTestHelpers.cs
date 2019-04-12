using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml;

namespace HelperLibrary
{
	public class UnitTestHelpers
	{
		private static string[] _databaseList;
        public static string InstanceName { get; private set; }

        public static bool IsInUnitTest
		{
			get
			{
				const string testAssemblyName = "Microsoft.VisualStudio.QualityTools.UnitTestFramework";
				return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.StartsWith(testAssemblyName));
			}
		}

		public static void Start(string instanceName, string[] databaseList)
		{
			_databaseList = databaseList;
			InstanceName = instanceName;

			// make sure any previous instances are shut down
			var startInfo = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = "cmd.exe",
				Arguments = "/c sqllocaldb stop \"" + instanceName + "\""
			};

			var process = new Process { StartInfo = startInfo };
			process.Start();
			process.WaitForExit();

			// delete any previous instance
			startInfo.Arguments = "/c sqllocaldb delete \"" + instanceName + "\"";
			process.Start();
			process.WaitForExit();

			// check to see if the database files exist, if so, then delete them
			foreach (var databaseName in databaseList)
			{
				DeleteDatabaseFiles(databaseName);
			}

			// create a new localdb sql server instance
			startInfo = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = "cmd.exe",
				Arguments = "/c sqllocaldb create \"" + instanceName + "\" -s"
			};

			process = new Process { StartInfo = startInfo };
			process.Start();
			process.WaitForExit();

			foreach (var databaseName in databaseList)
			{
				CreateDatabase(databaseName);
			}
		}

		private static void DeleteDatabaseFiles(string databaseName)
		{
			if (File.Exists(databaseName + ".mdf"))
			{
				File.Delete(databaseName + ".mdf");
			}

			if (File.Exists(databaseName + "_log.ldf"))
			{
				File.Delete(databaseName + "_log.ldf");
			}
		}

		public static void End()
		{
			// shut down the instance
			var startInfo = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = "cmd.exe",
				Arguments = "/c sqllocaldb stop \"" + InstanceName + "\""
			};

			var process = new Process { StartInfo = startInfo };
			process.Start();
			process.WaitForExit();

			// delete the instance
			startInfo.Arguments = "/c sqllocaldb delete \"" + InstanceName + "\"";
			process.Start();
			process.WaitForExit();

			foreach (var databaseName in _databaseList)
			{
				DeleteDatabaseFiles(databaseName);
			}
		}

		// truncate all tables in the databases setup
		public static void TruncateData()
		{
			var tableList = new List<string>();

			using (var db = new ADODatabaseContext("TEST"))
			{
				//_databaseList
				foreach (var database in _databaseList)
				{
					ClearAllConstraints(database);

					// generate a table list
					using (var reader = db.ReadQuery(@"
						SELECT * 
						FROM " + database + @".INFORMATION_SCHEMA.tables 
						WHERE TABLE_TYPE = 'BASE TABLE'
						ORDER BY TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME"))
					{
						while (reader.Read())
						{
							var tableName = reader["table_name"].ToString();
							var schemaName = reader["TABLE_SCHEMA"].ToString();

							tableList.Add(database + "." + schemaName + "." + tableName);
						}
					}
				}
			}

			using (var db = new ADODatabaseContext("TEST"))
			{
				foreach (var item in tableList)
				{
					db.ExecuteNonQuery(@"TRUNCATE TABLE " + item);
				}
			}
		}
		/// <summary>
		/// clear all constraints in database
		/// </summary>
		private static void ClearAllConstraints(string database)
		{
			using (var db = new ADODatabaseContext("TEST", database))
			{
				using (var reader = db.ReadQuery(@"
						SELECT 
							OBJECT_NAME(OBJECT_ID) AS ConstraintName,
							SCHEMA_NAME(schema_id) AS SchemaName,
							OBJECT_NAME(parent_object_id) AS TableName,
							type_desc AS ConstraintType
						FROM 
							sys.objects
						WHERE 
							type_desc LIKE '%CONSTRAINT' AND 
							OBJECT_NAME(OBJECT_ID) LIKE'fk_%'"))
				{
					while (reader.Read())
					{
						var foreignKeyTableName = reader["TableName"].ToString();
						var constraintName = reader["ConstraintName"].ToString();
						var schemaName = reader["SchemaName"].ToString();

						using (var dbExec = new ADODatabaseContext("TEST", database))
						{
							string query = "ALTER TABLE " + database + "." + schemaName + "." + foreignKeyTableName + " DROP CONSTRAINT " + constraintName;
							dbExec.ExecuteNonQuery(query);
						}
					}
				}
			}
		}

		private static void CreateDatabase(string databaseName)
		{
			var databaseDirectory = Directory.GetCurrentDirectory();

			using (var db = new ADODatabaseContext("TEST"))
			{
				db.ExecuteNonQuery(@"CREATE DATABASE [" + databaseName + @"]
				  CONTAINMENT = NONE
				  ON  PRIMARY 
				  ( NAME = N'" + databaseName + @"', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
																									@".mdf' , SIZE = 8096KB , FILEGROWTH = 1024KB )
				  LOG ON 
				  ( NAME = N'" + databaseName + @"_log', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
																									@"_log.ldf' , SIZE = 8096KB , FILEGROWTH = 10%)
				  ");
			}
		}

		public static void CreateStoredProcedure(Stream stream, string databaseName, string spName)
		{
			using (var db = new ADODatabaseContext("TEST", databaseName))
			{
				// first, drop the stored procedure if it already exists
				var sp = @"if exists (select * from sys.objects where name = N'" + spName + @"' and type = N'P') 
						  begin
							drop procedure " + spName + @"
						  end";
				db.ExecuteNonQuery(sp);

				// need to read the text file and create the stored procedure in the test database
				using (var reader = new StreamReader(stream))
				{
					var storedProcText = reader.ReadToEnd();

					var tsqLcommandList = Regex.Split(storedProcText, "GO");

					foreach (var tsqlCommand in tsqLcommandList)
					{
						db.ExecuteNonQuery(tsqlCommand);
					}
				}
			}
		}

		private static string LowerCaseTags(string xml)
		{
			return Regex.Replace(
				xml,
				@"<[^<>]+>",
				m => { return m.Value.ToLower(); },
				RegexOptions.Multiline | RegexOptions.Singleline);
		}

		public static void ReadData(string xmlJsonDataFile)
		{
            var schemaName = "dbo";

			var assembly = Assembly.GetCallingAssembly();
			var resourceName = xmlJsonDataFile;
			using (var stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new Exception("Cannot find XML data file, make sure it is set to Embedded Resource!");
				}

                var databaseName = "";
                if (xmlJsonDataFile.Substring(xmlJsonDataFile.Length - 3, 3).ToLower() == "xml")
				{
					using (var reader = new StreamReader(stream))
					{
						var document = new XmlDocument();
						document.LoadXml(reader.ReadToEnd());

						foreach (XmlNode element in document.ChildNodes)
						{
							foreach (XmlNode subelement in element.ChildNodes)
							{
								if (subelement.Name.ToLower() == "database")
								{
									databaseName = subelement.Attributes["name"].Value;
									schemaName = "dbo";

									if (subelement.Attributes["schema"] != null)
									{
										schemaName = subelement.Attributes["schema"].Value;
									}

									var insertQueryGenerator = new InsertQueryGenerator("TEST", databaseName, schemaName);

									var children = subelement.ChildNodes;
									foreach (XmlNode e in children)
									{
										insertQueryGenerator.InsertData(e);
									}
								}
							}
						}
					}
				}
				else if (xmlJsonDataFile.Substring(xmlJsonDataFile.Length - 4, 4).ToLower() == "json")
				{
					using (var reader = new StreamReader(stream))
					{
						var jsonFile = reader.ReadToEnd();

						dynamic temp = JsonConvert.DeserializeObject(jsonFile);
						InsertQueryGenerator insertQueryGenerator = null;

						foreach (var attr in temp)
						{
							if (attr.Name == "database")
							{
								databaseName = attr.Value;
								insertQueryGenerator = new InsertQueryGenerator("TEST", databaseName, schemaName);
							}
							else if (insertQueryGenerator != null)
							{
								insertQueryGenerator.InsertJsonData(attr);
							}
						}
					}
				}
			}
		}

		public static void CreateAllTables(List<TableDefinition> TableList, string databaseName)
		{
			// create all non "dbo" schemas in database
			var schemaList = (from t in TableList where t.SchemaName != "dbo" select t.SchemaName).Distinct();
			foreach (var schemaName in schemaList)
			{
				if (!string.IsNullOrEmpty(schemaName))
				{
					using (var db = new ADODatabaseContext("TEST", databaseName))
					{
						db.ExecuteNonQuery("CREATE SCHEMA [" + schemaName + "] AUTHORIZATION [dbo]");
					}
				}
			}

			// generate all tables listed in the table name list
			foreach (var tableDefinition in TableList)
			{
				var query = tableDefinition.CreateScript;

				using (var db = new ADODatabaseContext("TEST", databaseName))
				{
					db.ExecuteNonQuery(query);
				}
			}
		}

		public static void CreateConstraint(List<ConstraintDefinition> pConstraintList, string table1, string table2)
		{
			var constraintList = pConstraintList.Where(x => x.PkTable.ToLower() == table1 && x.FkTable.ToLower() == table2).ToList();
			foreach (var constraint in constraintList)
			{
				var query = "ALTER TABLE " + constraint.FkTable + " ADD CONSTRAINT fk_" + constraint.FkTable + "_" + constraint.PkTable + " FOREIGN KEY (" + constraint.FkField + ") REFERENCES " + constraint.PkTable + "(" + constraint.PkField + ")";

				using (var db = new ADODatabaseContext("TEST"))
				{
					db.ExecuteNonQuery(query);
				}
			}

			constraintList = pConstraintList.Where(x => x.PkTable.ToLower() == table2 && x.FkTable.ToLower() == table1).ToList();
			foreach (var constraint in constraintList)
			{
				var query = "ALTER TABLE " + constraint.FkTable + " ADD CONSTRAINT fk_" + constraint.FkTable + "_" + constraint.PkTable + " FOREIGN KEY (" + constraint.FkField + ") REFERENCES " + constraint.PkTable + "(" + constraint.PkField + ")";

				using (var db = new ADODatabaseContext("TEST", constraint.DatabaseName))
				{
					db.ExecuteNonQuery(query);
				}
			}
		}

		public static void ClearConstraints(List<ConstraintDefinition> pConstraintList)
		{
			var schemaName = "dbo";

			// delete all foreign constraints in all databases
			using (var db = new ADODatabaseContext("TEST"))
			{
				//_databaseList
				foreach (var database in _databaseList)
				{
					var constraints = pConstraintList.Where(x => x.DatabaseName == database).ToList();

					foreach (var constraint in constraints)
					{
						var constraintName = "fk_" + constraint.FkTable + "_" + constraint.PkTable;

						if (!string.IsNullOrEmpty(constraint.SchemaName))
						{
							schemaName = constraint.SchemaName;
						}

						var query = "SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='" + constraintName + "' AND CONSTRAINT_SCHEMA='" + schemaName + "'";
						using (var reader = db.ReadQuery(query))
						{
							while (reader.Read())
							{
								query = "ALTER TABLE " + constraint.DatabaseName + ".." + constraint.FkTable + " DROP CONSTRAINT " + constraintName;
								db.ExecuteNonQuery(query);
								break;
							}
						}
					}
				}
			}
        }

        public static int TotalRecords(string tableName, string database)
        {
            var results = 0;

            using (var db = new ADODatabaseContext("", database))
            {
                string query = "SELECT COUNT(*) AS total FROM " + tableName;
				using (var reader = db.ReadQuery(query))
				{
					while (reader.Read())
					{
						results = reader["total"].ToInt();
						break;
					}
				}
            }

            return results;
		}

		public static void ExecuteSQLCode(string filePath)
		{
			using (var db = new ADODatabaseContext("TEST"))
			{
				var assembly = Assembly.GetCallingAssembly();
				using (var stream = assembly.GetManifestResourceStream(filePath))
				{
					using (var reader = new StreamReader(stream))
					{
						var code = reader.ReadToEnd();
						var TSQLcommands = Regex.Split(code, "GO");

						foreach (var tsqlCommand in TSQLcommands)
						{
							if (tsqlCommand.Trim() != "")
							{
								db.ExecuteNonQuery(tsqlCommand);
							}
						}
					}
				}
			}
		}
	}
}
