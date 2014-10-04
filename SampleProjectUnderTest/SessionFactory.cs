using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Helpers;
using NHibernateDataLayer.sampledata.Tables;

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
					if (UnitTestHelpers.IsInUnitTest)
					{
						// unit test context
						//TODO: refactor the exposeConfiguration() part to reduce duplicate code
						_sessionFactory = Fluently.Configure()
						.Database(MsSqlConfiguration.MsSql2005
						.ConnectionString("Server=(localdb)\\sampledatatestinstance;Initial Catalog=sampledata;Integrated Security=True"))
						.Mappings(m => m.FluentMappings.Add<ProductMap>())
						.Mappings(m => m.FluentMappings.Add<StoreMap>())
						.Mappings(m => m.FluentMappings.Add<ProductTypeMap>())
						.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
						.BuildSessionFactory();
					}
					else
					{
						// production context
						_sessionFactory = Fluently.Configure()
						.Database(MsSqlConfiguration.MsSql2005
						.ConnectionString("Server=FRANK-PC\\FRANK;Initial Catalog=sampledata;Integrated Security=True"))
						.Mappings(m => m.FluentMappings.Add<ProductMap>())
						.Mappings(m => m.FluentMappings.Add<StoreMap>())
						.Mappings(m => m.FluentMappings.Add<ProductTypeMap>())
						.ExposeConfiguration(config =>
						{
							SchemaExport schemaExport = new SchemaExport(config);
						})
						.BuildSessionFactory();
					}
				}
				return _sessionFactory;
			}
		}
		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}
	}
}
