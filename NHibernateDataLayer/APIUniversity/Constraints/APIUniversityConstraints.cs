using System.Collections.Generic;
using HelperLibrary;

namespace ApplicationUnderTest.APIUniversity.Constraints
{
	public class APIUniversityConstraints
	{
		public static List<ConstraintDefinition> ConstraintList = new List<ConstraintDefinition> {
			new ConstraintDefinition { DatabaseName="APIUniversity", PkTable = "Room", PkField = "id", FkTable = "RoomReservation", FkField = "Room", SchemaName = "dbo" }
		};
	}
}
