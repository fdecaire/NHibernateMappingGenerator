using System;
using System.Data;

namespace HelperLibrary
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
		public static object ConvertToDBNullDate(this string DateTimeString)
		{
			if (!String.IsNullOrEmpty(DateTimeString))
			{
				DateTime tempDate;
				if (DateTime.TryParse(DateTimeString, out tempDate))
				{
					return tempDate;
				}
			}

			return DBNull.Value;
		}

		public static object ConvertToDBNullDouble(this string DoubleString)
		{
			if (!String.IsNullOrEmpty(DoubleString))
			{
				Double tempDouble;
				if (Double.TryParse(DoubleString, out tempDouble))
				{
					return tempDouble;
				}
			}

			return DBNull.Value;
		}

		/// <summary>
		/// convert from dataset to a nullable int data type
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static int? DBNullToInt(this object data)
		{
			if (data != DBNull.Value)
			{
				return data.ToString().ToInt();
			}

			return null;
		}

		/// <summary>
		/// convert from dataset to a nullable double data type
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double? DBNullToDouble(this object data)
		{
			if (data != DBNull.Value)
			{
				return data.ToString().ToDouble();
			}

			return null;
		}
	}
}
