using Helpers;

namespace NHibernateDataLayer.sampledata.Views
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class viewvwStoreProduct : ViewCreator	{
		private static ViewCreator _instance;
		public static ViewCreator Instance
		{
			get { return _instance ?? (_instance = new viewvwStoreProduct()); }
		}
		override public string Name
		{
			get { return "vwStoreProduct"; }
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
CREATE VIEW dbo.vwStoreProduct
AS
SELECT        dbo.Store.Name AS storename, dbo.Product.Name AS productname, dbo.ProductType.Description
FROM            dbo.Store INNER JOIN
                         dbo.Product ON dbo.Store.id = dbo.Product.store INNER JOIN
                         dbo.ProductType ON dbo.Product.ProductType = dbo.ProductType.Id
";
			}
		}
	}
}
