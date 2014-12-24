using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public class ADOParameter
	{
		public string Name { get; set; }
		public System.Data.SqlDbType Type { get; set; }
		public string Value { get; set; }
	}
}
