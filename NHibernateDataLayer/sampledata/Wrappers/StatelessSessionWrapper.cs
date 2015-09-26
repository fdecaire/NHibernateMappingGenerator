using System.Linq;
using NHibernate.Linq;

// DO NOT MODIFY! This code is auto-generated.
namespace NHibernateDataLayer
{
	public partial class StatelessSessionWrapper
	{
		public IQueryable<sampledata.Tables.Store> Store
		{
			get { return _Session.Query<sampledata.Tables.Store>(); }
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
		public IQueryable<sampledata.Tables.EmptyTable> EmptyTable
		{
			get { return _Session.Query<sampledata.Tables.EmptyTable>(); }
		}
		public IQueryable<sampledata.Tables.Department> Department
		{
			get { return _Session.Query<sampledata.Tables.Department>(); }
		}
		public IQueryable<sampledata.Tables.Person> Person
		{
			get { return _Session.Query<sampledata.Tables.Person>(); }
		}
		public IQueryable<sampledata.Tables.TodoItem> TodoItem
		{
			get { return _Session.Query<sampledata.Tables.TodoItem>(); }
		}
		public IQueryable<sampledata.Tables.ProgramSetting> ProgramSetting
		{
			get { return _Session.Query<sampledata.Tables.ProgramSetting>(); }
		}
	}
}
