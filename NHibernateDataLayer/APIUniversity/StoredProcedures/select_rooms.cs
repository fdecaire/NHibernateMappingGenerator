using HelperLibrary;

namespace NHibernateDataLayer.APIUniversity.StoredProcedures
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class select_rooms : StoredProc
	{
		private static StoredProc _instance;
		public static StoredProc Instance
		{
			get { return _instance ?? (_instance = new select_rooms()); }
		}
		override public string Name
		{
			get { return "select_rooms"; }
		}
		override public string Database { get { return "APIUniversity"; } }
		override public string Code
		{
			get
			{
			return @"USE [APIUniversity]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 

CREATE PROCEDURE [dbo].[select_rooms]
AS
BEGIN
	SELECT id,name FROM Room
END";
			}
		}
	}
}
