using System;
using HelperLibrary;
using System.Collections.Generic;

namespace NHibernateDataLayer.facultydata.TableGenerator
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class facultydataTables
	{
		public static string DatabaseName {
			get 
			{
				return "facultydata";
			}
		}

		public static List<TableDefinition> TableList = new List<TableDefinition> {
			new TableDefinition {Name="teacher", CreateScript="CREATE TABLE [dbo].[teacher]([id][int] IDENTITY(1,1) NOT NULL,[name][char](50), CONSTRAINT [PK_teacher] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="class", CreateScript="CREATE TABLE [dbo].[class]([id][int] IDENTITY(1,1) NOT NULL,[name][char](50),[teacherid][int] NOT NULL, CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
		};
	}
}
