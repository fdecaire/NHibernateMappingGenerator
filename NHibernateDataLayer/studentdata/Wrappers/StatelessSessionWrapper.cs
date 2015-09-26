using System.Linq;
using NHibernate.Linq;

// DO NOT MODIFY! This code is auto-generated.
namespace NHibernateDataLayer
{
	public partial class StatelessSessionWrapper
	{
		public IQueryable<studentdata.Tables.student> student
		{
			get { return _Session.Query<studentdata.Tables.student>(); }
		}
		public IQueryable<studentdata.Tables.StudentClass> StudentClass
		{
			get { return _Session.Query<studentdata.Tables.StudentClass>(); }
		}
		public IQueryable<studentdata.Tables.Student2Class> Student2Class
		{
			get { return _Session.Query<studentdata.Tables.Student2Class>(); }
		}
	}
}
