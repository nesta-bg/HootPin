﻿using HootPin.Models;
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
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("HootForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var artist = _context.Users
                .Include(u => u.Followers.Select(f => f.Follower))
                .Single(u => u.Id == userId);
                
            var hoot = new Hoot();
            hoot = hoot.Create(artist, userId, viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);
            
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
            var hoot = _context.Hoots
                .Include(h => h.Attendances.Select(a => a.Attendee))
                .Single(h => h.Id == viewModel.Id && h.ArtistId == userId);

            hoot.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

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
                .OrderBy(h => h.DateTime)
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
                .OrderBy( h => h.DateTime)
                .ToList();

            var viewModel = new HootsViewModel
            {
                UpcomingHoots = hoots,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Hoots I'm Attending."
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
            var hoot = _context.Hoots
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .Single(h => h.Id == id);

            if (hoot == null)
                return HttpNotFound();

            var viewModel = new HootDetailsViewModel { Hoot = hoot };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                   .Any(a => a.HootId == hoot.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == hoot.ArtistId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
    }
}