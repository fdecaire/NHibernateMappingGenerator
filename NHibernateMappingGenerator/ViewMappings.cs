using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

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
			string result = "";

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

			@out.Append("using Helpers;\r\n");
			@out.Append("\r\n");
			@out.Append("namespace NHibernateDataLayer." + _databaseName + ".Views\r\n");
			@out.Append("{\r\n");
			@out.Append("\t// DO NOT MODIFY! This code is auto-generated.\r\n");
			@out.Append("\tpublic partial class view" + _viewName + " : ViewCreator");
			@out.Append("\t{\r\n");
			@out.Append("\t\tprivate static ViewCreator _instance;\r\n");
			@out.Append("\t\tpublic static ViewCreator Instance\r\n");
			@out.Append("\t\t{\r\n");
			@out.Append("\t\t\tget { return _instance ?? (_instance = new view" + _viewName + "()); }\r\n");
			@out.Append("\t\t}\r\n");
			@out.Append("\t\toverride public string Name\r\n");
			@out.Append("\t\t{\r\n");
			@out.Append("\t\t\tget { return \"" + _viewName + "\"; }\r\n");
			@out.Append("\t\t}\r\n");
			@out.Append("\t\toverride public string Database { get { return \"" + _databaseName + "\"; } }\r\n");
			@out.Append("\t\toverride public string Code\r\n");
			@out.Append("\t\t{\r\n");
			@out.Append("\t\t\tget\r\n");
			@out.Append("\t\t\t{\r\n");
			@out.Append("\t\t\treturn @\"" + _code.Replace("\"", "\"\"") + "\";\r\n");
			@out.Append("\t\t\t}\r\n");
			@out.Append("\t\t}\r\n");
			@out.Append("\t}\r\n");
			@out.Append("}\r\n");

			return @out.ToString();
		}
	}
}
