using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateDataLayer.sampledata.Tables;
using ApplicationUnderTest.sampledata.Constraints;
using Helpers;
using SampleProjectUnderTest;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

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
		public void TestXMLDataFile()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.xml");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var query = (from s in db.Query<Store>() select s).ToList();

				Assert.AreEqual(2, query.Count);
			}
		}

		[TestMethod]
		public void TestIentityOverride()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.xml");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var productTypeList = (from s in db.Query<ProductType>() orderby s.Id select s.Id).ToList();

				Assert.AreEqual(1, productTypeList[0]);
				Assert.AreEqual(5, productTypeList[1]);
				Assert.AreEqual(6, productTypeList[2]);
			}
		}

		[TestMethod]
		public void TestJsonDataFile()
		{
			UnitTestHelpers.ReadData("SampleUnitTests.TestData.StoreListData.json");

			using (var db = MSSQLSessionFactory.OpenSession())
			{
				var storeList = (from s in db.Query<Store>() select s).ToList();
				Assert.AreEqual(2, storeList.Count);

				var productTypeList = (from s in db.Query<ProductType>() select s).ToList();
				Assert.AreEqual(3, productTypeList.Count);

				var productList = (from s in db.Query<Product>() select s).ToList();
				Assert.AreEqual(2, productList.Count);
			}
		}

		[TestMethod]
		public void TestConstraintCreate()
		{
			UnitTestHelpers.CreateConstraint(sampledataConstraints.ConstraintList, "product", "producttype");

			UnitTestHelpers.ClearConstraints(sampledataConstraints.ConstraintList);
		}
	}
}
