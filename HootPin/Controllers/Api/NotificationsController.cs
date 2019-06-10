using AutoMapper;
using HootPin.Core;
using HootPin.Core.Dtos;
using HootPin.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace HootPin.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public NotificationsDto GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var lastMonth = DateTime.Now.AddDays(-30);

            var recentNotifications = _unitOfWork.UserNotifications.GetRecentNotificationsByUser(userId, lastMonth);

            var recentNotificationsDto = recentNotifications.Select(Mapper.Map<Notification, NotificationDto>);

            var notificationsDto = new NotificationsDto();
            notificationsDto.RecentNotifications = recentNotificationsDto;

            var newNotifications = _unitOfWork.UserNotifications.GetNumberOfNewNotificationsByUser(userId, lastMonth);

            notificationsDto.NumberOfNewNotifications = newNotifications;

            return notificationsDto;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.UserNotifications.GetUnreadUserNotifications(userId);

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
