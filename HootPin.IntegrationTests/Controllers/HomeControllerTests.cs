using FluentAssertions;
using HootPin.Controllers;
using HootPin.Core.Models;
using HootPin.Core.ViewModels;
using HootPin.Persistence;
using NUnit.Framework;
using System;
using System.Linq;
using TestingExtensions;

namespace HootPin.IntegrationTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private ApplicationDbContext _context;
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new HomeController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnUpcomingHoots()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUserMvc(user.Id, user.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            // Act
            var result = _controller.Index();

            // Assert
            (result.ViewData.Model as HootsViewModel).UpcomingHoots.Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnUsersFutureAttendances()
        {
            // Arrange
            var attendee = _context.Users.Single(u => u.Name == "user1");
            var artist = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserMvc(attendee.Id, attendee.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = artist, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            var attendance = new Attendance { Hoot = hoot, Attendee = attendee };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            // Act
            var result = _controller.Index();

            // Assert
            (result.ViewData.Model as HootsViewModel).Attendances.Should().HaveCount(1);
        }
    }
}
