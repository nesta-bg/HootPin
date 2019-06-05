using HootPin.Models;
using HootPin.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace HootPin.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var followees = _unitOfWork.Followings.GetFollowees(userId);

            return View(followees);
        }
    }
}