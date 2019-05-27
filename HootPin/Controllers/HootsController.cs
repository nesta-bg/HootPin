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
                Genres = _context.Genres.ToList(),
                Heading = "Add a Hoot"
            };

            return View("HootForm", viewModel);
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
                return View("HootForm", viewModel);
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
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var hoot = _context.Hoots.Single(h => h.Id == id && h.ArtistId == userId);

            var viewModel = new HootFormViewModel
            {
                Id = hoot.Id,
                Genres = _context.Genres.ToList(),
                Date = hoot.DateTime.ToString("d MMM yyyy"),
                Time = hoot.DateTime.ToString("HH:mm"),
                Genre = hoot.GenreId,
                Venue = hoot.Venue,
                Heading = "Edit a Hoot"
            };

            return View("HootForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HootFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("HootForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var hoot = _context.Hoots.Single(h => h.Id == viewModel.Id && h.ArtistId == userId);

            hoot.Venue = viewModel.Venue;
            hoot.DateTime = viewModel.GetDateTime();
            hoot.GenreId = viewModel.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Hoots");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var hoots = _context.Hoots
                .Where(h => h.ArtistId == userId && 
                    h.DateTime > DateTime.Now && 
                    !h.IsCanceled)
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