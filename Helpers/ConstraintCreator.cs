using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public class ConstraintCreator
	{
		//TODO: need to create an object that contains a list of constraints defined in the database being scraped.
		//TODO: also need a helper object to create constraints from the definitions listed here.

/*
ALTER TABLE Orders
ADD CONSTRAINT fk_PerOrders
FOREIGN KEY (P_Id)
REFERENCES Persons(P_Id)
*/
		public void CreateConstraint(string PrimaryTable, string SecondaryTable)
		{

		}

		public void RemoveAllConstraints()
		{
/*
ALTER TABLE Orders
DROP CONSTRAINT fk_PerOrders
*/
		}
	}
}