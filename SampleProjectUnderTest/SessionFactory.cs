using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HelperLibrary;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateDataLayer.sampledata.Tables;
using NHibernateDataLayer;

namespace SampleProjectUnderTest
{
	public class MSSQLSessionFactory
	{
		private static ISessionFactory _sessionFactory;
		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
				{
					string serverInstance = "FRANK-PC\\FRANK";
					if (UnitTestHelpers.IsInUnitTest)
					{
						serverInstance = "(localdb)\\" + UnitTestHelpers.InstanceName;
					}

					_sessionFactory = Fluently.Configure()
					.Database(MsSqlConfiguration.MsSql2005
					.ConnectionString("Server=" + serverInstance + ";Initial Catalog=sampledata;Integrated Security=True"))
					.Mappings(m => m.FluentMappings.Add<DepartmentMap>())
					.Mappings(m => m.FluentMappings.Add<StoreMap>())
					.Mappings(m => m.FluentMappings.Add<ProductMap>())
					.Mappings(m => m.FluentMappings.Add<ProductTypeMap>())
					.ExposeConfiguration(config =>
					{
						SchemaExport schemaExport = new SchemaExport(config);
					})
					.BuildSessionFactory();
				}
				return _sessionFactory;
			}
		}
		public static SessionWrapper OpenSession()
		{
			return new SessionWrapper(SessionFactory.OpenSession());
		}
	}
}
