using System;
using System.Linq;

namespace SampleProjectUnderTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var query = (from d in db.department select d).ToList();

				foreach (var item in query)
				{
					Console.WriteLine(item.name);
				}

				Console.ReadKey();
			}
		}
	}
}
