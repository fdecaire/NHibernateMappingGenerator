using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEFProjectUnderTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new sampledataEntities())
			{
				var query = (from s in db.Stores select s);

				foreach (var item in query)
				{
					Console.WriteLine(item.Name);
				}

				Console.ReadKey();
			}
		}
	}
}
