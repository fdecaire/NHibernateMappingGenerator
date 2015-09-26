using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class ProgramSetting
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool Setting { get; set; }
	}

	public class ProgramSettingMap : ClassMap<ProgramSetting>
	{
		public ProgramSettingMap()
		{
			Table("sampledata..ProgramSetting");
			Id(u => u.Id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.Name).CustomSqlType("varchar (50)").Length(50).Not.Nullable();
			Map(u => u.Setting).Not.Nullable();
		}
	}
}
