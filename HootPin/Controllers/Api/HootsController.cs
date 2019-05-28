using HootPin.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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
            var hoot = _context.Hoots
                .Include(h => h.Attendances.Select(a => a.Attendee))
                .Single(h => h.Id == id && h.ArtistId == userId);

            if (hoot.IsCanceled)
                return NotFound();

            hoot.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
