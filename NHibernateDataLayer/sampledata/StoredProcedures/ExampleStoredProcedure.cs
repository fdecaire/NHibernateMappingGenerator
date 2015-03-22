using Helpers;

namespace NHibernateDataLayer.sampledata.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class ExampleStoredProcedure : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new ExampleStoredProcedure()); }
		}
		override public string Name
		{
			get { return "ExampleStoredProcedure"; }
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
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ExampleStoredProcedure]
	@Param1 char(50),
	@Param2 int,
	@Param3 int = NULL,
	@Param4 varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM person
	select * from emptytable
END
";
			}
		}
	}
}
