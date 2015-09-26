using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class Person
	{
		public virtual int id { get; set; }
		public virtual string first { get; set; }
		public virtual string last { get; set; }
		public virtual int department { get; set; }
	}

	public class PersonMap : ClassMap<Person>
	{
		public PersonMap()
		{
			Table("sampledata..Person");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.first).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.last).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.department).Not.Nullable();
		}
	}
}
