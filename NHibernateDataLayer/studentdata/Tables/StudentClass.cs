using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.studentdata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class StudentClass
	{
		public virtual int StudentClassId { get; set; }
		public virtual int StudentId { get; set; }
		public virtual int ClassId { get; set; }
	}

	public class StudentClassMap : ClassMap<StudentClass>
	{
		public StudentClassMap()
		{
			Table("studentdata..StudentClass");
			Id(u => u.StudentClassId).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.StudentId).Not.Nullable();
			Map(u => u.ClassId).Not.Nullable();
		}
	}
}
