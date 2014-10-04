using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProjectUnderTest;
using Helpers;

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
			using (var db = MSSQLSessionFactory.OpenSession()) { };
		}

		[AssemblyCleanup]
		public static void ClassEndCleanup()
		{
			UnitTestHelpers.End();
		}
	}
}
