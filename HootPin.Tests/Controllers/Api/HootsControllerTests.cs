using FluentAssertions;
using HootPin.Controllers.Api;
using HootPin.Core;
using HootPin.Core.Models;
using HootPin.Core.Repositories;
using HootPin.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace HootPin.Tests.Controllers.Api
{
    [TestClass]
    public class HootsControllerTests
    {
        private HootsController _controller;
        private Mock<IHootRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IHootRepository>();

            var mockUoW = new Mock<IUnitOfWork>();

            mockUoW.SetupGet(u => u.Hoots).Returns(_mockRepository.Object);

            _controller = new HootsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain");
        }

        [TestMethod]
        public void Cancel_NoHootWithGivenIdExists_ShouldReturnNotFound()
        {
            //because we haven't used the Setup() method in _mockRepository like in the test below
            //when we call GetHootWithAttendees() it will return null by default
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_HootIsCanceled_ShouldReturnNotFound()
        {
            var hoot = new Hoot();
            hoot.Cancel();
            _mockRepository.Setup(r => r.GetHootWithAttendees(1)).Returns(hoot);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersHoot_ShouldReturnUnauthorized()
        {
            var hoot = new Hoot { ArtistId = _userId + "-" };
            _mockRepository.Setup(r => r.GetHootWithAttendees(1)).Returns(hoot);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var hoot = new Hoot { ArtistId = _userId };
            _mockRepository.Setup(r => r.GetHootWithAttendees(1)).Returns(hoot);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
