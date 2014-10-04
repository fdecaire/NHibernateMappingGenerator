using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Helpers
{
	public abstract class ViewCreator
	{
		public abstract string Name { get; }
		public abstract string Database { get; }
		public abstract string Code { get; }

		public void CreateView(string instanceName)
		{
			string connectionString = "server=(localdb)\\" + instanceName + ";" +
																					 "Trusted_Connection=yes;" +
																					 "database=" + Database + @"; " +
																					 "Integrated Security=true; ";

			using (var db = new ADODatabaseContext(connectionString))
			{
				// first, drop the view if it already exists
				string sp = @"if exists (select * from sys.views where name = N'" + Name + @"' and type = N'P') 
          begin
            drop view " + Name + @"
          end";
				db.ExecuteNonQuery(sp);

				// need to read the text file and create the view in the test database
				string[] TSQLcommands = Regex.Split(Code, "GO");

				foreach (var tsqlCommand in TSQLcommands)
				{
					db.ExecuteNonQuery(tsqlCommand);
				}
			}
		}
	}
}
