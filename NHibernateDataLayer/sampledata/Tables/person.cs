using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class person
	{
		public virtual int id { get; set; }
		public virtual string first { get; set; }
		public virtual string last { get; set; }
		public virtual int department { get; set; }
		public virtual string ExtraData { get; set; }
	}

	public class personMap : ClassMap<person>
	{
		public personMap()
		{
			Table("sampledata..person");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.first).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.last).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.department).Not.Nullable();
			Map(u => u.ExtraData).CustomType("StringClob").CustomSqlType("varchar (MAX)").Length(Int32.MaxValue).Nullable();
		}
	}
}
