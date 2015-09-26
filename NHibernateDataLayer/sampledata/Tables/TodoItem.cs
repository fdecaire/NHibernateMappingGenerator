using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class TodoItem
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual bool Active { get; set; }
	}

	public class TodoItemMap : ClassMap<TodoItem>
	{
		public TodoItemMap()
		{
			Table("sampledata..TodoItem");
			Id(u => u.Id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.Title).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.Description).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.Active).Not.Nullable();
		}
	}
}
