using HootPin.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace HootPin.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ViewResult Index()
        {
            var userId = User.Identity.GetUserId();

            var followees = _unitOfWork.Followings.GetFollowees(userId);

            return View(followees);
        }
    }
}