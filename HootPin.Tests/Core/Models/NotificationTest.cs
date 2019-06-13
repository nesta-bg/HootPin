using FluentAssertions;
using HootPin.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HootPin.Tests.Core.Models
{
    [TestClass]
    public class NotificationTest
    {
        [TestMethod]
        public void HootCanceled_WhenCalled_ShouldReturnNotificationForCanceledHoot()
        {
            var hoot = new Hoot();
            var notification = Notification.HootCanceled(hoot);

            notification.Type.Should().Be(NotificationType.HootCanceled);
            notification.Hoot.Should().Be(hoot);
        }
    }
}
