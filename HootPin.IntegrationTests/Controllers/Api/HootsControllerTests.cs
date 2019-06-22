using FluentAssertions;
using HootPin.Controllers.Api;
using HootPin.Core.Models;
using HootPin.Persistence;
using NUnit.Framework;
using System;
using System.Linq;
using System.Web.Http.Results;
using TestingExtensions;

namespace HootPin.IntegrationTests.Controllers.Api
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
        public void Cancel_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            //var artist = _context.Users.First();
            var artist = _context.Users.Single(u => u.Name == "user1");
            var attendee = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserApi(artist.Id, artist.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = artist, DateTime = DateTime.Now, Genre = genre, Venue = "-" };
            var attendance = new Attendance { Attendee = attendee, Hoot = hoot };

            hoot.Attendances.Add(attendance);

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            // Act
            var result = _controller.Cancel(hoot.Id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
