using HootPin.Models;
using HootPin.ViewModels;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using HootPin.Persistence;

namespace HootPin.Controllers
{
    public class HootsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public HootsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new HootFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Hoot"
            };

            return View("HootForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HootFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("HootForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var artist = _unitOfWork.Users.GeArtistWithFollowers(userId);
                
            var hoot = new Hoot();
            hoot = hoot.Create(artist, userId, viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

            _unitOfWork.Hoots.Add(hoot);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var hoot = _unitOfWork.Hoots.GetHootByUser(id, userId);

            var viewModel = new HootFormViewModel
            {
                Id = hoot.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("HootForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var hoot = _unitOfWork.Hoots.GetHootWithAttendees(viewModel.Id, userId);

            hoot.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Hoots");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var hoots = _unitOfWork.Hoots.GetUpcomingHootsByArtist(userId);

            return View(hoots);
        }

        [Authorize]
        public ActionResult Attend()
        {
            var userId = User.Identity.GetUserId();

            var hoots = _unitOfWork.Hoots.GetHootsUserAttending(userId);

            var attendances = _unitOfWork.Attendances.GetFutureAttendancesByAttendee(userId);

            var viewModel = new HootsViewModel
            {
                UpcomingHoots = hoots,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Hoots I'm Attending.",
                Attendances = attendances
            };

            return View("Hoots", viewModel);
        }

        [HttpPost]
        public ActionResult Search(HootsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var hoot = _unitOfWork.Hoots.GetHoot(id);

            if (hoot == null)
                return HttpNotFound();

            var viewModel = new HootDetailsViewModel { Hoot = hoot };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(hoot.Id, userId) != null;

                viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, hoot.ArtistId) != null;
            }

            return View("Details", viewModel);
        }
    }
}