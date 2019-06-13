using FluentAssertions;
using HootPin.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HootPin.Tests.Core.Models
{
    [TestClass]
    public class HootTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var hoot = new Hoot();

            hoot.Cancel();

            hoot.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var hoot = new Hoot();
            hoot.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            hoot.Cancel();

            var attendees = hoot.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
