using FluentAssertions;
using HootPin.Core.Models;
using HootPin.Persistence;
using HootPin.Persistence.Repositories;
using HootPin.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;

namespace HootPin.Tests.Persistence.Repositories
{
    [TestClass]
    public class UserNotificationRepositoryTests
    {
        private UserNotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);

            _repository = new UserNotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetRecentNotificationsByUser_NotificationIsForDifferentUser_ShouldNotBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetRecentNotificationsByUser(userNotification.UserId + "-", DateTime.Now.AddDays(-1));
            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetRecentNotificationsByUser_NotificationsAreInThePast_ShouldNotBeReturned()
        {
            var user = new ApplicationUser ();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetRecentNotificationsByUser(userNotification.UserId, DateTime.Now.AddDays(1));
            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetRecentNotificationsByUser_ValidRequest_ShouldBeReturned()
        {
            var user = new ApplicationUser ();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetRecentNotificationsByUser(userNotification.UserId, DateTime.Now.AddDays(-1));
            userNotifications.Should().HaveCount(1);
            userNotifications.First().Should().Be(notification);
        }

        [TestMethod]
        public void GetUnreadUserNotifications_UserNotificationIsForDifferentUser_ShoulNotBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetUnreadUserNotifications(userNotification.UserId + "-");
            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadUserNotifications_UserNotificationIsRead_ShoulNotBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            userNotification.Read();
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetUnreadUserNotifications(userNotification.UserId);
            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadUserNotifications_ValidRequest_ShoulBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.HootCreated(new Hoot());
            var userNotification = new UserNotification(user, notification);
            userNotification.UserId = "1";
            _mockNotifications.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetUnreadUserNotifications(userNotification.UserId);
            userNotifications.Should().Contain(userNotification);
        }
    }
}
