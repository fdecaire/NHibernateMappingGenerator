using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.studentdata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class student
	{
		public virtual int id { get; set; }
		public virtual string name { get; set; }
	}

	public class studentMap : ClassMap<student>
	{
		public studentMap()
		{
			Table("studentdata..student");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.name).CustomSqlType("char (50)").Length(50).Nullable();
		}
	}
}
