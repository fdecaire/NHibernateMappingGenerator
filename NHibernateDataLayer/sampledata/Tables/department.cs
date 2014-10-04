using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class department
	{
		public virtual int id { get; set; }
		public virtual string name { get; set; }
	}

	public class departmentMap : ClassMap<department>
	{
		public departmentMap()
		{
			Table("sampledata..department");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.name).CustomSqlType("varchar (50)").Length(50).Nullable();
		}
	}
}
