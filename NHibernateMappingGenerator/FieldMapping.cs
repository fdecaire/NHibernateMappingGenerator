using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateMappingGenerator
{
	public class FieldMapping
	{
		public string FieldName { get; set; }
		public string DataType { get; set; }
		public bool Nullable { get; set; }
		public string Precision { get; set; }
		public string PrecisionRadix { get; set; }
		public string StringLength { get; set; }

		public string CSharpDataType
		{
			get
			{
				switch (DataType)
				{
					case "int":
					case "shortint":
						return "int";
					case "char":
					case "text":
					case "varchar":
					case "nchar":
					case "nvarchar":
					case "xml":
					case "ntext":
						return "string";
					case "datetime":
						return "DateTime";
					case "bit":
						return "bool";
					case "bigint":
						return "long";
					case "money":
					case "decimal":
						return "double";
					case "float":
						return "float";
					case "smallint":
					case "tinyint":
						return "Int16";
					case "numeric":
						return "System.Decimal";
					case "uniqueidentifier":
						return "System.Guid";
					case "real":
						return "System.Single";
					case "varbinary":
						return "System.Data.Linq.Binary";
					default:
						return "unknown(" + DataType + ")";
				}
			}
		}

		public string EmitFieldMapping()
		{
			var @out = new StringBuilder();

			var nullExtension = ".Not.Nullable()";
			if (Nullable)
			{
				nullExtension = ".Nullable()";
			}

			if (StringLength == "-1") StringLength = "MAX";

			var stringLengthForDotNet = (StringLength == "MAX" ? "Int32.MaxValue" : StringLength);

			var lengthExtension = "";
			if (StringLength != "" && StringLength != "-1" && CSharpDataType != "string")
			{
				lengthExtension = ".Length(" + stringLengthForDotNet + ")";
			}

			//Sample: Map(x => x.Column_Name, "Column Name").CustomSqlType("varchar (512)");
			var customType = "";
			switch (DataType)
			{
				case "varchar":
				case "nvarchar":
					if (StringLength == "MAX")
					{
						customType = ".CustomType(\"StringClob\")";
					}
					customType += ".CustomSqlType(\"" + DataType + " (" + StringLength + ")\")";
					lengthExtension = String.Format(".Length({0})", stringLengthForDotNet);
					break;
				case "text":
					customType = ".CustomType(\"StringClob\").CustomSqlType(\"text\")";
					lengthExtension = ".Length(Int32.MaxValue)";
					break;
				case "xml":
					customType = ".CustomType(\"StringClob\").CustomSqlType(\"xml\")";
					lengthExtension = ".Length(Int32.MaxValue)";
					break;
				case "char":
				case "nchar":
					customType = ".CustomSqlType(\"" + DataType + " (" + StringLength + ")\")";
					lengthExtension = String.Format(".Length({0})", stringLengthForDotNet);
					break;
				case "money":
					customType = ".CustomSqlType(\"money\")";
					break;
				case "bigint":
					customType = ".CustomSqlType(\"bigint\")";
					break;
				case "tinyint":
					customType = ".CustomSqlType(\"tinyint\")";
					break;
				case "float":
					customType = ".CustomSqlType(\"float\")";
					break;
				case "ntext":
					customType = ".CustomSqlType(\"ntext\")";
					break;
			}


			//TODO: need to enhance this to handle field names that are not C# compatible
			@out.AppendLine("\t\t\tMap(u => u." + FieldName + ")" + customType + lengthExtension + nullExtension + ";");

			return @out.ToString();
		}
	}
}
