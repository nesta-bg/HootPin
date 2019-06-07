using HootPin.Models;
using System.Linq;

namespace HootPin.Repositories
{
    public interface IAttendanceRepository
    {
        ILookup<int, Attendance> GetFutureAttendancesByAttendee(string attendeeId);
        Attendance GetAttendance(int hootId, string userId);
    }
}
