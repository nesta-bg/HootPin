using HootPin.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace HootPin.Controllers.Api
{
    [Authorize]
    public class HootsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public HootsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var hoot = _context.Hoots.Single(h => h.Id == id && h.ArtistId == userId);

            if (hoot.IsCanceled)
                return NotFound();

            hoot.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Type = NotificationType.HootCanceled,
                Hoot = hoot
            };

            var attendees = _context.Attendances
               .Where(a => a.HootId == hoot.Id)
               .Select(a => a.Attendee)
               .ToList();

            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
