using System.Collections.Generic;
using HelperLibrary;

namespace ApplicationUnderTest.sampledata.Constraints
{
	public class sampledataConstraints
	{
		public static List<ConstraintDefinition> ConstraintList = new List<ConstraintDefinition> {
			new ConstraintDefinition { DatabaseName="sampledata", PkTable = "Store", PkField = "id", FkTable = "Product", FkField = "store", SchemaName = "dbo" },
			new ConstraintDefinition { DatabaseName="sampledata", PkTable = "ProductType", PkField = "Id", FkTable = "Product", FkField = "ProductType", SchemaName = "dbo" }
		};
	}
}
