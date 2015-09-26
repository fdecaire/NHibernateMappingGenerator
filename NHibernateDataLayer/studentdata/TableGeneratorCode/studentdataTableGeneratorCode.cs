using System;
using HelperLibrary;
using System.Collections.Generic;

namespace NHibernateDataLayer.studentdata.TableGenerator
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class studentdataTables
	{
		public static string DatabaseName {
			get 
			{
				return "studentdata";
			}
		}

		public static List<TableDefinition> TableList = new List<TableDefinition> {
			new TableDefinition {Name="student", CreateScript="CREATE TABLE [dbo].[student]([id][int] IDENTITY(1,1) NOT NULL,[name][char](50), CONSTRAINT [PK_student] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="StudentClass", CreateScript="CREATE TABLE [dbo].[StudentClass]([StudentClassId][int] IDENTITY(1,1) NOT NULL,[StudentId][int] NOT NULL,[ClassId][int] NOT NULL, CONSTRAINT [PK_StudentClass] PRIMARY KEY CLUSTERED ([StudentClassId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="Student2Class", CreateScript="CREATE TABLE [dbo].[Student2Class]([StudentId][int] NOT NULL,[ClassId][int] NOT NULL, CONSTRAINT [PK_Student2Class] PRIMARY KEY CLUSTERED ([StudentId] ASC,[ClassId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
		};
	}
}
