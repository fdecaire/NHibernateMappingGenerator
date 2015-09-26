using System.Data;
using System.Linq;
using ApplicationUnderTest.sampledata.Constraints;
using HelperLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProjectUnderTest;

namespace SampleUnitTests
{
	// these unit tests are just samples of what the SQLLocalDB objects can do
	[TestClass]
	public class SampleUnitTests
	{
		[TestCleanup]
		public void Cleanup()
		{
			UnitTestHelpers.TruncateData();
		}
		
		[TestMethod]
		public void sample_test_stored_procedure()
		{
			using (var db = new ADODatabaseContext("", "sampledata"))
			{
				db.ExecuteNonQuery("INSERT INTO department (Name) VALUES ('sales')");
				db.ExecuteNonQuery("INSERT INTO person (First, Last, Department) VALUES ('Joe', 'Cool', 1)");

				DataSet data = db.StoredProcedureSelect("ExampleStoredProcedure");
				Assert.AreEqual(1, data.Tables[0].Rows.Count);
			}
		}

		[TestMethod]
		public void sample_test_identity_override()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.xml");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var productTypeList = (from s in db.ProductType orderby s.Id select s.Id).ToList();

				Assert.AreEqual(1, productTypeList[0]);
				Assert.AreEqual(5, productTypeList[1]);
				Assert.AreEqual(6, productTypeList[2]);
			}
		}

		[TestMethod]
		public void sample_test_json_data_file()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.json");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var storeList = (from s in db.Store select s).ToList();
				Assert.AreEqual(2, storeList.Count);

				var productTypeList = (from s in db.ProductType select s).ToList();
				Assert.AreEqual(3, productTypeList.Count);

				var productList = (from s in db.Product select s).ToList();
				Assert.AreEqual(2, productList.Count);
			}
		}

		[TestMethod]
		public void sample_test_constraint_create()
		{
			UnitTestHelpers.CreateConstraint(sampledataConstraints.ConstraintList, "product", "producttype");

			UnitTestHelpers.ClearConstraints(sampledataConstraints.ConstraintList);
		}

		[TestMethod]
		public void sample_test_xml_data_file_with_attributes()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListDataWithAttributes.xml");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var query = (from s in db.Store select s).ToList();
				Assert.AreEqual(5, query.Count);

				var productQuery = (from s in db.Product select s).ToList();
				Assert.AreEqual(4, productQuery.Count);
			}
		}

		[TestMethod]
		public void sample_test_xml_data_file_with_elements()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.xml");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var query = (from s in db.Store select s).ToList();

				Assert.AreEqual(2, query.Count);
			}
		}
	}
}
