using System.Linq;
using NHibernate.Linq;

// DO NOT MODIFY! This code is auto-generated.
namespace NHibernateDataLayer
{
	public partial class SessionWrapper
	{
		public IQueryable<sampledata.Tables.Store> Store
		{
			get { return _Session.Query<sampledata.Tables.Store>(); }
		}
		public IQueryable<sampledata.Tables.student> student
		{
			get { return _Session.Query<sampledata.Tables.student>(); }
		}
		public IQueryable<sampledata.Tables.ProductType> ProductType
		{
			get { return _Session.Query<sampledata.Tables.ProductType>(); }
		}
		public IQueryable<sampledata.Tables.Product> Product
		{
			get { return _Session.Query<sampledata.Tables.Product>(); }
		}
		public IQueryable<sampledata.Tables.vwStoreProduct> vwStoreProduct
		{
			get { return _Session.Query<sampledata.Tables.vwStoreProduct>(); }
		}
		public IQueryable<sampledata.Tables.department> department
		{
			get { return _Session.Query<sampledata.Tables.department>(); }
		}
		public IQueryable<sampledata.Tables.person> person
		{
			get { return _Session.Query<sampledata.Tables.person>(); }
		}
	}
}
