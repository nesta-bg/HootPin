using HootPin.Core.Models;
using System.Linq;

namespace HootPin.Core.Repositories
{
    public interface IAttendanceRepository
    {
        ILookup<int, Attendance> GetFutureAttendancesByAttendee(string attendeeId);
        Attendance GetAttendance(int hootId, string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}
