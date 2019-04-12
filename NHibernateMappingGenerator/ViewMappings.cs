using System.Text;
using HelperLibrary;

namespace NHibernateMappingGenerator
{
	public class ViewMappings
	{
		private string _databaseName;
		private string _viewName;
		private string _connectionString;
		private string _code;

		public ViewMappings(string connectionString, string databaseName, string viewName)
		{
			_databaseName = databaseName;
			_viewName = viewName;
			_connectionString = connectionString;

			// read view code here
			_code = LookupViewCode();
		}

		private string LookupViewCode()
		{
			var result = "";

			using (var db = new ADODatabaseContext(_connectionString.Replace("master", _databaseName)))
			{
				var myReader = db.ReadQuery("SELECT OBJECT_DEFINITION(OBJECT_ID('" + _viewName + "')) AS code");
				while (myReader.Read())
				{
					result = myReader["code"].ToString();
				}
			}

			return result;
		}

		public string EmitCode()
		{
			var @out = new StringBuilder();

			_code = @"USE [" + _databaseName + @"]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
" + _code;

			@out.AppendLine("using HelperLibrary;");
			@out.AppendLine("");
			@out.AppendLine("namespace NHibernateDataLayer." + _databaseName + ".Views");
			@out.AppendLine("{");
			@out.AppendLine("\t// DO NOT MODIFY! This code is auto-generated.");
			@out.AppendLine("\tpublic partial class view" + _viewName + " : ViewCreator");
			@out.AppendLine("\t{");
			@out.AppendLine("\t\tprivate static ViewCreator _instance;");
			@out.AppendLine("\t\tpublic static ViewCreator Instance");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget { return _instance ?? (_instance = new view" + _viewName + "()); }");
			@out.AppendLine("\t\t}");
			@out.AppendLine("\t\toverride public string Name");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget { return \"" + _viewName + "\"; }");
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
