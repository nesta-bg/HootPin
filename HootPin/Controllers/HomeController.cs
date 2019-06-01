using HootPin.Models;
using HootPin.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HootPin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upcomingHoots = _context.Hoots
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .Where(h => h.DateTime > DateTime.Now && !h.IsCanceled)
                .OrderBy(h => h.DateTime)
                .ToList();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingHoots = upcomingHoots
                    .Where(h =>
                            h.Artist.Name.Contains(query) ||
                            h.Genre.Name.Contains(query) ||
                            h.Venue.Contains(query)
                            )
                    .OrderBy(h => h.DateTime)
                    .ToList();
            }

            var userId = User.Identity.GetUserId();
            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Hoot.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.HootId);

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