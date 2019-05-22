using HootPin.Dtos;
using HootPin.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace HootPin.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.HootId == dto.HootId))
                return BadRequest("The Attendance already exists.");

            var attendance = new Attendance
            {
                HootId = dto.HootId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
