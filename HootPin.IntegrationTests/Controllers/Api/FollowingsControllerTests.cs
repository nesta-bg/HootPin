using FluentAssertions;
using HootPin.Controllers.Api;
using HootPin.Core.Dtos;
using HootPin.Core.Models;
using HootPin.Persistence;
using NUnit.Framework;
using System.Linq;
using System.Web.Http.Results;
using TestingExtensions;

namespace HootPin.IntegrationTests.Controllers.Api
{
    [TestFixture]
    public class FollowingsControllerTests
    {
        private ApplicationDbContext _context;
        private FollowingsController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new FollowingsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Follow_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var follower = _context.Users.Single(u => u.Name == "user1");
            var followee = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserApi(follower.Id, follower.UserName);
            var followingDto = new FollowingDto { FolloweeId = followee.Id };

            // Act
            var result = _controller.Follow(followingDto);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test, Isolated]
        public void Unfollow_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var follower = _context.Users.Single(u => u.Name == "user1");
            var followee = _context.Users.Single(u => u.Name == "user2");
            _controller.MockCurrentUserApi(follower.Id, follower.UserName);
            var following = new Following { Follower = follower, Followee = followee };

            _context.Followings.Add(following);
            _context.SaveChanges();

            // Act
            var result = _controller.Unfollow(followee.Id);
            var negConResult = result as OkNegotiatedContentResult<string>;

            // Assert
            negConResult.Content.Should().BeEquivalentTo(followee.Id);
        }
    }
}
