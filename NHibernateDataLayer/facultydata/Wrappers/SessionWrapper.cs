using System.Linq;
using NHibernate.Linq;

// DO NOT MODIFY! This code is auto-generated.
namespace NHibernateDataLayer
{
	public partial class SessionWrapper
	{
		public IQueryable<facultydata.Tables.teacher> teacher
		{
			get { return _Session.Query<facultydata.Tables.teacher>(); }
		}
		public IQueryable<facultydata.Tables.Class> Class
		{
			get { return _Session.Query<facultydata.Tables.Class>(); }
		}
	}
}
