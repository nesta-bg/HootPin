using HootPin.Models;
using HootPin.ViewModels;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;

namespace HootPin.Controllers
{
    public class HootsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HootsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new HootFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HootFormViewModel viewModel)
        {
            /*
            var artistId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == artistId);
            var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);

            var hoot = new Hoot
            {
                Artist = artist,
                Genre = genre
            };
            */

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var hoot = new Hoot
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var hoots = _context.Hoots
                .Where(h => h.ArtistId == userId && h.DateTime > DateTime.Now)
                .Include(h => h.Genre)
                .ToList();

            return View(hoots);
        }

        [Authorize]
        public ActionResult Attend()
        {
            var userId = User.Identity.GetUserId();

            var hoots = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Hoot)
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .ToList();

            return View(hoots);
        }
    }
}