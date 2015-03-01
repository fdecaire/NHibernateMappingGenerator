using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Helpers
{
	public class ADOParameter
	{
		public string Name { get; set; }
		public SqlDbType Type { get; set; }
		public object Value { get; set; }
	}
}
