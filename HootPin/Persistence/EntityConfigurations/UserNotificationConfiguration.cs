using HootPin.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace HootPin.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(t => new { t.UserId, t.NotificationId });

            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}