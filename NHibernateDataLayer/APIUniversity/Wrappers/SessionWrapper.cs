using System.Linq;
using NHibernate.Linq;

// DO NOT MODIFY! This code is auto-generated.
namespace NHibernateDataLayer
{
	public partial class SessionWrapper
	{
		public IQueryable<APIUniversity.Tables.Room> Room
		{
			get { return _Session.Query<APIUniversity.Tables.Room>(); }
		}
		public IQueryable<APIUniversity.Tables.RoomReservation> RoomReservation
		{
			get { return _Session.Query<APIUniversity.Tables.RoomReservation>(); }
		}
	}
}
