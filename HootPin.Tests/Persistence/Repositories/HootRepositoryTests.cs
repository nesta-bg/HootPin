using FluentAssertions;
using HootPin.Core.Models;
using HootPin.Core.Repositories;
using HootPin.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using TestingExtensions;

namespace HootPin.Tests.Persistence.Repositories
{
    [TestClass]
    public class HootRepositoryTests
    {
        private HootRepository _repository;
        private Mock<DbSet<Hoot>> _mockHoots;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHoots = new Mock<DbSet<Hoot>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Hoots).Returns(_mockHoots.Object);

            _repository = new HootRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingHootsByArtist_HootIsInThePast_ShouldNotBeReturned()
        {
            var hoot = new Hoot() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            //_mockHoots.SetSource(new List<Hoot> { hoot });
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetUpcomingHootsByArtist("1");
            hoots.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingHootsByArtist_HootIsCanceled_ShouldNotBeReturned()
        {
            //var hoot = new Hoot();
            var hoot = new Hoot() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            hoot.Cancel();
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetUpcomingHootsByArtist("1");
            hoots.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingHootsByArtist_HootIsForADifferentArtist_ShouldNotBeReturned()
        {
            var hoot = new Hoot() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetUpcomingHootsByArtist(hoot.ArtistId + "-");
            hoots.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingHootsByArtist_HootIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var hoot = new Hoot() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetUpcomingHootsByArtist(hoot.ArtistId);
            hoots.Should().Contain(hoot);
        }

        [TestMethod]
        public void GetHootsUserAttending_HootIsNotForTheGivenArtist_ShouldNotBeReturned()
        {
            var attendance = new Attendance() { AttendeeId = "1" };
            var hoot = new Hoot();
            hoot.Attendances.Add(attendance);
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetHootsUserAttending(attendance.AttendeeId + "-");
            hoots.Should().BeEmpty();
        }

        [TestMethod]
        public void GetHootsUserAttending_HootIsForTheGivenArtist_ShouldBeReturned()
        {
            var attendance = new Attendance() { AttendeeId = "1" };
            var hoot = new Hoot();
            hoot.Attendances.Add(attendance);
            _mockHoots.SetSource(new[] { hoot });

            var hoots = _repository.GetHootsUserAttending(attendance.AttendeeId);
            hoots.Should().Contain(hoot);
        }
    }
}
