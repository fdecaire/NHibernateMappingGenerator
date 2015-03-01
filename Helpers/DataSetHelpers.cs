using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Helpers
{
	public static class DataSetHelpers
	{
		public static void AddDataColumn(this DataTable dataTable, string fieldName, string dataType)
		{
			DataColumn dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType(dataType);
			dataColumn.ColumnName = fieldName;
			dataTable.Columns.Add(dataColumn);
		}
	}
}
