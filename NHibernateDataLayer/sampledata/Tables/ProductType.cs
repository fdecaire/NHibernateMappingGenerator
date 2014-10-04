using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class ProductType
	{
		public virtual int Id { get; set; }
		public virtual string Description { get; set; }
	}

	public class ProductTypeMap : ClassMap<ProductType>
	{
		public ProductTypeMap()
		{
			Table("sampledata..ProductType");
			Id(u => u.Id).GeneratedBy.Identity().Not.Nullable();
			Map(u => u.Description).CustomSqlType("varchar (50)").Length(50).Nullable();
		}
	}
}
