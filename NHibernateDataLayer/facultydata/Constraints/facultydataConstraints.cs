using System.Collections.Generic;
using HelperLibrary;

namespace ApplicationUnderTest.facultydata.Constraints
{
	public class facultydataConstraints
	{
		public static List<ConstraintDefinition> ConstraintList = new List<ConstraintDefinition> {
			new ConstraintDefinition { DatabaseName="facultydata", PkTable = "teacher", PkField = "id", FkTable = "class", FkField = "teacherid" }
		};
	}
}
