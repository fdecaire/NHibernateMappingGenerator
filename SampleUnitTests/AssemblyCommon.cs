using HelperLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateDataLayer.sampledata.StoredProcedures;
using NHibernateDataLayer.sampledata.TableGenerator;

namespace SampleUnitTests
{
	[TestClass]
	public sealed class AssemblyUnitTestShared
	{
		[AssemblyInitialize]
		public static void ClassStartInitialize(TestContext testContext)
		{
			UnitTestHelpers.Start("sampledatatestinstance", new string[] { "sampledata" });

			// create tables
			UnitTestHelpers.CreateAllTables(sampledataTables.TableList, sampledataTables.DatabaseName);

			// create one stored procedure for testing purposes
			ExampleStoredProcedure.Instance.CreateStoredProcedure();
		}

		[AssemblyCleanup]
		public static void ClassEndCleanup()
		{
			UnitTestHelpers.End();
		}
	}
}
