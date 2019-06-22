using FluentAssertions;
using HootPin.Controllers.Api;
using HootPin.Core.Dtos;
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
    public class AttendancesControllerTests
    {
        private ApplicationDbContext _context;
        private AttendancesController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new AttendancesController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Attend_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var attendee = _context.Users.Single(u => u.Name == "user1");
            var artist = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserApi(attendee.Id, attendee.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = artist, DateTime = DateTime.Now, Genre = genre, Venue = "-" };
            
            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            var attendanceDto = new AttendanceDto { HootId = hoot.Id };

            // Act
            var result = _controller.Attend(attendanceDto);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test, Isolated]
        public void DeleteAttendance_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var attendee = _context.Users.Single(u => u.Name == "user1");
            var artist = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserApi(attendee.Id, attendee.UserName);
            var genre = _context.Genres.First();

            var hoot = new Hoot { Artist = artist, DateTime = DateTime.Now, Genre = genre, Venue = "-" };

            _context.Hoots.Add(hoot);
            _context.SaveChanges();

            var attendance = new Attendance { Hoot = hoot, Attendee = attendee };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteAttendance(hoot.Id);
            var negConResult = result as OkNegotiatedContentResult<int>;

            // Assert
            negConResult.Content.Should().Be(hoot.Id);
        }
    }
}
