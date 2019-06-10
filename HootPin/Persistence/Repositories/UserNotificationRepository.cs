using HootPin.Core.Models;
using HootPin.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HootPin.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetRecentNotificationsByUser(string id, DateTime period)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == id)
                .Select(un => un.Notification)
                .Where(n => n.DateTime > period)
                .Include(n => n.Hoot.Artist)
                .ToList();
        }

        public int GetNumberOfNewNotificationsByUser(string id, DateTime period)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == id
                    && !un.IsRead
                    && un.Notification.DateTime > period)
                .Count();
        }

        public List<UserNotification> GetUnreadUserNotifications(string id)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == id && !un.IsRead)
                .ToList();
        }
    }
}