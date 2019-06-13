using HootPin.Core.Models;
using System.Data.Entity;

namespace HootPin.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Hoot> Hoots { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}