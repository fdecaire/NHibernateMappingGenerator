using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace HelperLibrary
{
	public static class StringHelpers
	{
		public static string ReverseString(string input)
		{
			return new string(input.ToCharArray().Reverse().ToArray());
		}

		public static List<string> Explode(this string s, char separator)
		{
			List<string> result = new List<string>();

			string[] temporary = s.Split(separator);

			foreach (string temp in temporary)
			{
				result.Add(temp);
			}

			return result;
		}

		public static string ReadFromList(this string[] list, int index)
		{
			if (list.Length > index)
			{
				if (list[index] == "")
				{
					return null;
				}
				else
				{
					return list[index];
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Input could be nullable.  Return null if input is null, otherwise convert to string.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string ToNullableString(this object o)
		{
			if (o == null)
			{
				return null;
			}

			return o.ToString();
		}

		public static int ToNullableInt(this object num)
		{
			if (num == null)
			{
				return 0;
			}

			if (num == DBNull.Value)
			{
				return 0;
			}

			return int.Parse(num.ToString());
		}

		public static int ToInt(this object num)
		{
			return int.Parse(num.ToString());
		}

		public static double ToDouble(this string num)
		{
			return double.Parse(num);
		}
		
		public static bool IsNumeric(this string text)
		{
			double output;

			if (double.TryParse(text, out output))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// returns true if the string "text" represents an integer value
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsInt(this string text)
		{
			int output;

			if (int.TryParse(text, out output))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Convert two char day of week into DayOfWeek type
		/// </summary>
		/// <param name="dayOfWeekName">MO, TU, WE, TH, FR, SA, SU</param>
		/// <returns></returns>
		public static DayOfWeek TwoCharWeekName(this string dayOfWeekName)
		{
			switch (dayOfWeekName.ToUpper())
			{
				case "MO":
					return DayOfWeek.Monday;
				case "TU":
					return DayOfWeek.Tuesday;
				case "WE":
					return DayOfWeek.Wednesday;
				case "TH":
					return DayOfWeek.Thursday;
				case "FR":
					return DayOfWeek.Friday;
				case "SA":
					return DayOfWeek.Saturday;
				case "SU":
					return DayOfWeek.Sunday;
			}
			return DayOfWeek.Monday;
		}
	}
}
