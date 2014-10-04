using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.facultydata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class Class
	{
		public virtual int id { get; set; }
		public virtual string name { get; set; }
		public virtual int teacherid { get; set; }
	}

	public class ClassMap : ClassMap<Class>
	{
		public ClassMap()
		{
			Table("facultydata..Class");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.name).CustomSqlType("char (50)").Length(50).Nullable();
			Map(u => u.teacherid).Not.Nullable();
		}
	}
}
