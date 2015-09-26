using HelperLibrary;

namespace NHibernateDataLayer.sampledata.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class ReadPersonByLastName : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new ReadPersonByLastName()); }
		}
		override public string Name
		{
			get { return "ReadPersonByLastName"; }
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

CREATE PROCEDURE [dbo].[ReadPersonByLastName]
	@LastName char(50)
AS
BEGIN
	SET NOCOUNT ON;     

	SELECT * FROM person WHERE last = @LastName	
	
END";
			}
		}
	}
}
