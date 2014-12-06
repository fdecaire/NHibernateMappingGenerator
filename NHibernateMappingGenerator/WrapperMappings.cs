using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateMappingGenerator
{
	public class WrapperMappings
	{
		public string EmitHeader(string wrapperType)
		{
			var @out = new StringBuilder();

			@out.AppendLine("using System.Linq;");
			@out.AppendLine("using NHibernate.Linq;");

			@out.AppendLine("");
			@out.AppendLine("// DO NOT MODIFY! This code is auto-generated.");
			@out.AppendLine("namespace NHibernateDataLayer");
			@out.AppendLine("{");
			@out.AppendLine("\tpublic partial class " + wrapperType);
			@out.AppendLine("\t{");

			return @out.ToString();
		}

		public string EmitCode(string databaseName, string tableName)
		{
			var @out = new StringBuilder();

			switch (tableName)
			{
				case "class":
					tableName = "Class";
					break;
				case "public":
					tableName = "Public";
					break;
				case "partial":
					tableName = "Partial";
					break;
			}

			@out.AppendLine("\t\tpublic IQueryable<" + databaseName + ".Tables." + tableName + "> " + tableName + "");
			@out.AppendLine("\t\t{");
			@out.AppendLine("\t\t\tget { return _Session.Query<" + databaseName + ".Tables." + tableName + ">(); }");
			@out.AppendLine("\t\t}");

			return @out.ToString();
		}

		public string EmitFooter()
		{
			var @out = new StringBuilder();

			@out.AppendLine("\t}");
			@out.AppendLine("}");

			return @out.ToString();
		}
	}
}
