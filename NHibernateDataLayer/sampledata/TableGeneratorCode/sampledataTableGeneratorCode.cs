using System;
using HelperLibrary;
using System.Collections.Generic;

namespace NHibernateDataLayer.sampledata.TableGenerator
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class sampledataTables
	{
		public static string DatabaseName {
			get 
			{
				return "sampledata";
			}
		}

		public static List<TableDefinition> TableList = new List<TableDefinition> {
			new TableDefinition {Name="Store", CreateScript="CREATE TABLE [dbo].[Store]([id][int] IDENTITY(1,1) NOT NULL,[Name][varchar](50),[Address][varchar](50),[City][varchar](50),[State][varchar](50),[Zip][varchar](50), CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="ProductType", CreateScript="CREATE TABLE [dbo].[ProductType]([Id][int] IDENTITY(1,1) NOT NULL,[Description][varchar](50), CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="Product", CreateScript="CREATE TABLE [dbo].[Product]([ProductId][int] NOT NULL,[ProductType][int] NOT NULL,[Name][varchar](50),[store][int] NOT NULL,[Price][numeric](18,4), CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId] ASC,[ProductType] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="vwStoreProduct", CreateScript="CREATE TABLE [dbo].[vwStoreProduct]([storename][varchar](50),[productname][varchar](50),[Description][varchar](50))"},
			new TableDefinition {Name="EmptyTable", CreateScript="CREATE TABLE [dbo].[EmptyTable]([id][int] NOT NULL,[name][varchar](50),[address][varchar](50), CONSTRAINT [PK_EmptyTable] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="Department", CreateScript="CREATE TABLE [dbo].[Department]([id][int] IDENTITY(1,1) NOT NULL,[name][varchar](50), CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="Person", CreateScript="CREATE TABLE [dbo].[Person]([id][int] IDENTITY(1,1) NOT NULL,[first][varchar](50),[last][varchar](50),[department][int] NOT NULL, CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="TodoItem", CreateScript="CREATE TABLE [dbo].[TodoItem]([Id][int] IDENTITY(1,1) NOT NULL,[Title][varchar](50),[Description][varchar](50),[Active][bit] NOT NULL, CONSTRAINT [PK_TodoItem] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="ProgramSetting", CreateScript="CREATE TABLE [dbo].[ProgramSetting]([Id][int] IDENTITY(1,1) NOT NULL,[Name][varchar](50) NOT NULL,[Setting][bit] NOT NULL, CONSTRAINT [PK_ProgramSetting] PRIMARY KEY CLUSTERED ([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
		};
	}
}
