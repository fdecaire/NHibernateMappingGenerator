using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.facultydata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class teacher
	{
		public virtual int id { get; set; }
		public virtual string name { get; set; }
	}

	public class teacherMap : ClassMap<teacher>
	{
		public teacherMap()
		{
			Table("facultydata..teacher");
			Id(u => u.id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.name).CustomSqlType("char (50)").Length(50).Nullable();
		}
	}
}
