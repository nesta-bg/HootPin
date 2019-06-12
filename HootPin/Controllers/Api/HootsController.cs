using HootPin.Core;
using HootPin.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace HootPin.Controllers.Api
{
    [Authorize]
    public class HootsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public HootsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var hoot = _unitOfWork.Hoots.GetHootWithAttendees(id);

            if (hoot == null || hoot.IsCanceled)
                return NotFound();

            if (hoot.ArtistId != userId)
                return Unauthorized();

            hoot.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
