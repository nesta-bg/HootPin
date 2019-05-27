using HootPin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HootPin.ViewModels;

namespace HootPin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingHoots = _context.Hoots
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .Where(h => h.DateTime > DateTime.Now && !h.IsCanceled);

            var viewModel = new HomeViewModel
            {
                UpcomingHoots = upcomingHoots,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
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