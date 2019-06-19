using FluentAssertions;
using HootPin.Controllers;
using HootPin.Core.Models;
using HootPin.Core.ViewModels;
using HootPin.IntegrationTests.Extensions;
using HootPin.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HootPin.IntegrationTests.Controllers
{
    [TestFixture]
    public class HootsControllerTests
    {
        private ApplicationDbContext _context;
        private HootsController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new HootsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingHoots()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            // Act
            var result = _controller.Mine();

            // Assert
            (result.ViewData.Model as IEnumerable<Hoot>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenHoot()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.Single(g => g.Id == 1);

            var hoot = new Hoot { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            // Act
            var result = _controller.Update(new HootFormViewModel
            {
                Id = hoot.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });

            // Assert
            _context.Entry(hoot).Reload();
            hoot.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            hoot.Venue.Should().Be("Venue");
            hoot.GenreId.Should().Be(2);
        }
    }
}
