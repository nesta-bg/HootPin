using HootPin.Persistence;
using HootPin.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace HootPin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingHoots = _unitOfWork.Hoots.GetUpcomingHootsWithArtistsAndGenres();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingHoots = _unitOfWork.Hoots.GetHootsByQuery(upcomingHoots, query);
            }

            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances.GetFutureAttendancesByAttendee(userId);

            var viewModel = new HootsViewModel
            {
                UpcomingHoots = upcomingHoots,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query,
                Heading = "Upcoming Hoots",
                Attendances = attendances
            };

            return View("Hoots", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}