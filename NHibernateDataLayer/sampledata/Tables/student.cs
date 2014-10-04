using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class student
	{
		public virtual int id { get; set; }
		public virtual string Name { get; set; }
	}

	public class studentMap : ClassMap<student>
	{
		public studentMap()
		{
			Table("sampledata..student");
			Id(u => u.id).GeneratedBy.Assigned().Not.Nullable();
			Map(u => u.Name).CustomSqlType("char (50)").Length(50).Nullable();
		}
	}
}
