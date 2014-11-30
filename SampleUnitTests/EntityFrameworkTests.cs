using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleEFProjectUnderTest;
using Helpers;

namespace SampleUnitTests
{
	[TestClass]
	public class EntityFrameworkTests
	{
		[TestCleanup]
		public void Cleanup()
		{
			UnitTestHelpers.TruncateData();
		}

		[TestMethod]
		public void TestEntityFrameworkContext()
		{
			using (var db = new sampledataEntities())
			{
				Store store = new Store 
				{
 					Name = "Test Store",
				};

				db.Stores.Add(store);
				db.SaveChanges();

				var resultQuery = (from s in db.Stores select s).ToList();

				Assert.AreEqual(1, resultQuery.Count());
				Assert.AreEqual("Test Store", resultQuery[0].Name);
			}
		}
	}
}
