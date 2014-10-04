using FluentNHibernate.Mapping;
using System;

namespace NHibernateDataLayer.sampledata.Tables
{
	// DO NOT MODIFY! This code is auto-generated.
	public partial class vwStoreProduct
	{
		public virtual string storename { get; set; }
		public virtual string productname { get; set; }
		public virtual string Description { get; set; }
	}

	public class vwStoreProductMap : ClassMap<vwStoreProduct>
	{
		public vwStoreProductMap()
		{
			Table("sampledata..vwStoreProduct");
			Map(u => u.storename).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.productname).CustomSqlType("varchar (50)").Length(50).Nullable();
			Map(u => u.Description).CustomSqlType("varchar (50)").Length(50).Nullable();
		}
	}
}
