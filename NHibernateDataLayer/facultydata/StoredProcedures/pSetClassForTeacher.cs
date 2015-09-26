using HelperLibrary;

namespace NHibernateDataLayer.facultydata.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class pSetClassForTeacher : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new pSetClassForTeacher()); }
		}
		override public string Name
		{
			get { return "pSetClassForTeacher"; }
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

create PROCEDURE [dbo].[pSetClassForTeacher]
@TeacherId int,
@ClassId int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE class SET teacherid=@TeacherId WHERE id=@ClassId

END

";
			}
		}
	}
}
