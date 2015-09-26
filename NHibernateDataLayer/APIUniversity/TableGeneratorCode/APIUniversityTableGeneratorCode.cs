using System;
using HelperLibrary;
using System.Collections.Generic;

namespace NHibernateDataLayer.APIUniversity.TableGenerator
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class APIUniversityTables
	{
		public static string DatabaseName {
			get 
			{
				return "APIUniversity";
			}
		}

		public static List<TableDefinition> TableList = new List<TableDefinition> {
			new TableDefinition {Name="Room", CreateScript="CREATE TABLE [dbo].[Room]([id][int] IDENTITY(1,1) NOT NULL,[Name][varchar](50),[RoomNumber][int], CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
			new TableDefinition {Name="RoomReservation", CreateScript="CREATE TABLE [dbo].[RoomReservation]([id][int] IDENTITY(1,1) NOT NULL,[Room][int] NOT NULL,[TimeSlot][int] NOT NULL,[Reserved][bit] NOT NULL, CONSTRAINT [PK_RoomReservation] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"},
		};
	}
}
