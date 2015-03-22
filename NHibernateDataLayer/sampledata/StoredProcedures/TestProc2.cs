using Helpers;

namespace NHibernateDataLayer.sampledata.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class TestProc2 : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new TestProc2()); }
		}
		override public string Name
		{
			get { return "TestProc2"; }
		}
		override public string Database { get { return "sampledata"; } }
		override public string Code
		{
			get
			{
			return @"USE [sampledata]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
CREATE PROC TestProc2
AS
SELECT object_id, name FROM sys.objects ;
SELECT name, schema_id, create_date FROM sys.objects ;
";
			}
		}
	}
}
