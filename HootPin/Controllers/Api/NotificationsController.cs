using AutoMapper;
using HootPin.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using HootPin.Dtos;
using System;

namespace HootPin.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public NotificationsDto GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var lastMonth = DateTime.Now.AddDays(-30);

            var recentNotifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Where(n => n.DateTime > lastMonth)
                .Include(n => n.Hoot.Artist)
                .ToList();

            var recentNotificationsDto = recentNotifications.Select(Mapper.Map<Notification, NotificationDto>);

            var notificationsDto = new NotificationsDto();
            notificationsDto.RecentNotifications = recentNotificationsDto;

            var newNotifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Where(n => n.DateTime > lastMonth)
                .Count();

            notificationsDto.NumberOfNewNotifications = newNotifications;

            return notificationsDto;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
