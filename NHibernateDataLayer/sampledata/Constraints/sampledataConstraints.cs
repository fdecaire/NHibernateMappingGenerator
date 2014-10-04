using System.Collections.Generic;
using Helpers;

namespace ApplicationUnderTest.sampledata.Constraints
{
	public class sampledataConstraints
	{
		public static List<ConstraintDefinition> ConstraintList = new List<ConstraintDefinition> {
			new ConstraintDefinition { DatabaseName="sampledata", PkTable = "ProductType", PkField = "Id", FkTable = "Product", FkField = "ProductType" }
		};
	}
}
