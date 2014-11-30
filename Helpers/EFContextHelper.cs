using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace Helpers
{
	public static class EFContextHelper
	{
		public static string ConnectionString(string connectionName, string databaseName, string modelName, string userName, string password)
		{
			bool integratedSecurity = (userName == "");

			if (UnitTestHelpers.IsInUnitTest)
			{
				connectionName = "(localdb)\\" + UnitTestHelpers.InstanceName;
				integratedSecurity = true;
			}

			return new EntityConnectionStringBuilder
			{
				Metadata = "res://*/" + modelName + ".csdl|res://*/" + modelName + ".ssdl|res://*/" + modelName + ".msl",
				Provider = "System.Data.SqlClient",
				ProviderConnectionString = new SqlConnectionStringBuilder
				{
					MultipleActiveResultSets = true,
					InitialCatalog = databaseName,
					DataSource = connectionName,
					IntegratedSecurity = integratedSecurity,
					UserID = userName,
					Password = password
				}.ConnectionString
			}.ConnectionString;
		}
	}
}
