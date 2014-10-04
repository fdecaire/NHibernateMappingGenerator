using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.studentdata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class Student2Class
	{
		public virtual int StudentId { get; set; }
		public virtual int ClassId { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Student2Class that = (Student2Class)obj;

			return this.StudentId == that.StudentId &&
				this.ClassId == that.ClassId;
		}

		public override int GetHashCode()
		{
			return StudentId.GetHashCode() ^
				ClassId.GetHashCode();
		}
	}

	public class Student2ClassMap : ClassMap<Student2Class>
	{
		public Student2ClassMap()
		{
			Table("studentdata..Student2Class");
			CompositeId()
				.KeyProperty(u => u.StudentId)
				.KeyProperty(u => u.ClassId);
		}
	}
}
