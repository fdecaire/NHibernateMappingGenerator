using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.APIUniversity.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class Room
	{
		public virtual int id { get; set; }
		public virtual string Name { get; set; }
		public virtual int ? RoomNumber { get; set; }
	}

	public class RoomMap : ClassMap<Room>
	{
		public RoomMap()
		{
			Table("APIUniversity..Room");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.Name).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.RoomNumber).Nullable();
		}
	}
}
