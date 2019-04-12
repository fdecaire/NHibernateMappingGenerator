using System;

namespace NHibernateMappingGenerator
{
	public static class StringHelpers
	{
		public static int ToInt(this object poField)
		{
			int liReturn;
			try
			{
				liReturn = Convert.ToInt32(poField);
			}
			catch (InvalidCastException)
			{
				liReturn = 0;
			}
			catch (FormatException)
			{
				liReturn = 0;
			}
			return liReturn;
		}
	}
}
