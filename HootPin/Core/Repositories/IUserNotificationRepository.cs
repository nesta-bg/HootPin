using HootPin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HootPin.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<Notification> GetRecentNotificationsByUser(string id, DateTime period);
        int GetNumberOfNewNotificationsByUser(string id, DateTime period);
        List<UserNotification> GetUnreadUserNotifications(string id);
    }
}
