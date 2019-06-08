using HootPin.Core.Models;
using HootPin.Core.Repositories;
using System;
using System.Linq;

namespace HootPin.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILookup<int, Attendance> GetFutureAttendancesByAttendee(string attendeeId)
        {
            return _context.Attendances
              .Where(a => a.AttendeeId == attendeeId && a.Hoot.DateTime > DateTime.Now)
              .ToList()
              .ToLookup(a => a.HootId);
        }

        public Attendance GetAttendance(int hootId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.HootId == hootId && a.AttendeeId == userId);
        }
    }
}