using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.APIUniversity.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class RoomReservation
	{
		public virtual int id { get; set; }
		public virtual int Room { get; set; }
		public virtual int TimeSlot { get; set; }
		public virtual bool Reserved { get; set; }
	}

	public class RoomReservationMap : ClassMap<RoomReservation>
	{
		public RoomReservationMap()
		{
			Table("APIUniversity..RoomReservation");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.Room).Not.Nullable();
			Map(u => u.TimeSlot).Not.Nullable();
			Map(u => u.Reserved).Not.Nullable();
		}
	}
}
