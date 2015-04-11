using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public class FieldDefinition : IEquatable<string>
	{
		public string Name;
		public string Type;
		private string _value;
		public string Value
		{
			get { return _value; }
			set
			{
				_value = value;

				switch (Type)
				{
					case "char":
					case "text":
					case "varchar":
					case "nchar":
					case "nvarchar":
					case "xml":
					case "ntext":
					case "datetime":
					case "datetime2":
					case "date":
					case "time":
						_value = "'" + _value.Replace("'", "''") + "'";
						break;
				}
			}
		}

		public Boolean Equals(string Name)
		{
			return (this.Name.ToLower() == Name);
		}
	}
}
