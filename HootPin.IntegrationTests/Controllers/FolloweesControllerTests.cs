using FluentAssertions;
using HootPin.Controllers;
using HootPin.Core.Models;
using HootPin.Persistence;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestingExtensions;

namespace HootPin.IntegrationTests.Controllers
{
    [TestFixture]
    public class FolloweesControllerTests
    {
        private ApplicationDbContext _context;
        private FolloweesController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new FolloweesController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnUsersFollowees()
        {
            // Arrange
            var follower = _context.Users.Single(u => u.Name == "user1");
            var followee = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserMvc(follower.Id, follower.UserName);
            var following = new Following { Follower = follower, Followee = followee };

            _context.Followings.Add(following);
            _context.SaveChanges();

            // Act
            var result = _controller.Index();

            // Assert
            (result.ViewData.Model as IEnumerable<ApplicationUser>).Should().HaveCount(1);
        }
    }
}
