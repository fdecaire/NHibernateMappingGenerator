using System.Text.RegularExpressions;

namespace HelperLibrary
{
	public abstract class StoredProc
	{
		public abstract string Name { get; }
		public abstract string Database { get; }
		public abstract string Code { get; }

		public void CreateStoredProcedure()
		{
			using (var db = new ADODatabaseContext("TEST", Database))
			{
				// first, drop the stored procedure if it already exists
				var sp = @"if exists (select * from sys.objects where name = N'" + Name.Replace("_tilde_", "~") + @"' and type = N'P') 
					  begin
						drop procedure " + Name.Replace("_tilde_", "~") + @"
					  end";
				db.ExecuteNonQuery(sp);

				// need to read the text file and create the stored procedure in the test database
				var tsqlCommandList = Regex.Split(Code, "GO");

				foreach (var tsqlCommand in tsqlCommandList)
				{
					db.ExecuteNonQuery(tsqlCommand);
				}
			}
		}
	}
}
