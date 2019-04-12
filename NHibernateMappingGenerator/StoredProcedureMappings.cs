using System.Text;
using HelperLibrary;

namespace NHibernateMappingGenerator
{
	public class StoredProcedureMappings
	{
		private string _databaseName;
		private string _storedProcedureName;
		private string _connectionString;
		private string _code;

		public StoredProcedureMappings(string connectionString, string databaseName, string storedProcedureName)
		{
			_databaseName = databaseName;
			_storedProcedureName = storedProcedureName;
			_connectionString = connectionString;

			NormalizeStoredProcedureName();

			_code = LookupStoredProcedureCode();
		}

		private void NormalizeStoredProcedureName()
		{
			_storedProcedureName = _storedProcedureName.Replace("~", "_tilde_");
			_storedProcedureName = _storedProcedureName.Replace("-", "_dash_");
			_storedProcedureName = _storedProcedureName.Replace(".", "_dot_");
		}

		// this method will create an object that can be used by LINQ to access the stored procedure
		private string CreateStoredProcedureNHibernateMapping()
		{
			var @out = new StringBuilder();

			//TODO: parse the stored procedure code and find all the variable names and types
			//TODO: generate the C# code that will be the method used inside LINQ code
			/*
CREATE PROCEDURE [dbo].[ReadPersonByLastName]
	@LastName char(50)
AS
			 
			  
			 db.CreateSQLQuery("exec pSetClassForTeacher @LastName=:LastName")
					.SetParameter("LastName", 3)
					.SetTimeout(100)
					.ExecuteUpdate();
			 
			 
			 public class class_with_properties
			 {
					prop1 {get;set;}
					prop2 {get;set;}
			 }
			 
			 public static class partial StoreProcedureClasses
			 {
					public static class_with_properties spname()
					{
							var resultSet =
								db.CreateSQLQuery("exec database..spname @LastName=:LastName")
									.SetResultTransformer(Transformers.AliasToBean<class_with_properties>())
									.SetParameter("LastName", 3)
									.List<class_with_properties>();
			  
							return resultSet;
					}
			 }
			*/

			//TODO: need to find out if there is a return result set and create a class with properties, then use the transformer
			//TODO: need to figure out how to determine what the result set will be

			return @out.ToString();
		}

		private string LookupStoredProcedureCode()
		{
			string result = "";

			using (var db = new ADODatabaseContext(_connectionString.Replace("master", _databaseName)))
			{
				var reader = db.ReadQuery("SELECT OBJECT_DEFINITION(OBJECT_ID('" + _storedProcedureName + "')) AS code");
				while (reader.Read())
				{
					result = reader["code"].ToString();
				}
			}

			return result;
		}

		public string EmitCode()
		{
			var @out = new StringBuilder();

			_code = _code.Replace("ALTER PROCEDURE", "CREATE PROCEDURE");

			_code = @"USE [" + _databaseName + @"]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
" + _code;

			@out.AppendLine("using HelperLibrary;");
			@out.AppendLine("");
			@out.AppendLine("namespace NHibernateDataLayer." + _databaseName + ".StoredProcedures");
			@out.AppendLine("{");
			@out.AppendLine("\t// DO NOT MODIFY! This code is auto-generated.");
			@out.AppendLine("\tpublic partial class " + _storedProcedureName + " : StoredProc");
			@out.AppendLine("\t{");
			@out.AppendLine("\t\tprivate static StoredProc _instance;");
			@out.AppendLine("\t\tpublic static StoredProc Instance");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget { return _instance ?? (_instance = new " + _storedProcedureName + "()); }");
			@out.AppendLine("\t\t}");
			@out.AppendLine("\t\toverride public string Name");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget { return \"" + _storedProcedureName + "\"; }");
			@out.AppendLine("\t\t}");
			@out.AppendLine("\t\toverride public string Database { get { return \"" + _databaseName + "\"; } }");
			@out.AppendLine("\t\toverride public string Code");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget");
			@out.AppendLine("\t\t\t{");
			@out.AppendLine("\t\t\treturn @\"" + _code.Replace("\"", "\"\"") + "\";");
			@out.AppendLine("\t\t\t}");
			@out.AppendLine("\t\t}");
			@out.AppendLine("\t}");
			@out.AppendLine("}");


			return @out.ToString();
		}
	}
}
