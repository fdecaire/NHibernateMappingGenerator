using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Helpers;

namespace HelpersUnitTests
{
	[TestClass]
	public class ADOContextUnitTests
	{
		[TestCleanup]
		public void Cleanup()
		{
			UnitTestHelpers.TruncateData();
		}

		[TestMethod]
		public void TestADOContextDatabaseReference()
		{
			// test to see if we can pass the database name inside the connection string in unit test mode
			string connectionString = "server=(localdb)\\" + UnitTestHelpers.InstanceName + ";" +
								"Trusted_Connection=yes;" +
								"database=model;" +
								"Integrated Security=true; " +
								"connection timeout=30";

			string database = "";
			using (var db = new ADODatabaseContext(connectionString))
			{
				database = db.Database;
			}

			Assert.AreEqual("model", database);
		}
	}
}
