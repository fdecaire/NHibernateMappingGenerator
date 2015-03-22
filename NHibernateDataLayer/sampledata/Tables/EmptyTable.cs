using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class EmptyTable
	{
		public virtual int id { get; set; }
		public virtual string name { get; set; }
		public virtual string address { get; set; }
	}

	public class EmptyTableMap : ClassMap<EmptyTable>
	{
		public EmptyTableMap()
		{
			Table("sampledata..EmptyTable");
			Id(u => u.id).GeneratedBy.Assigned().Not.Nullable();
			Map(u => u.name).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.address).CustomSqlType("varchar (50)").Length(50).Nullable();
		}
	}
}
