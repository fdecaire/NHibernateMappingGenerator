using HelperLibrary;

namespace NHibernateDataLayer.facultydata.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class pSelectClassPerTeacher : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new pSelectClassPerTeacher()); }
		}
		override public string Name
		{
			get { return "pSelectClassPerTeacher"; }
		}
		override public string Database { get { return "facultydata"; } }
		override public string Code
		{
			get
			{
			return @"USE [facultydata]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 

CREATE PROCEDURE [dbo].[pSelectClassPerTeacher]
@TeacherId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM class WHERE teacherid = @TeacherId 
END

";
			}
		}
	}
}
