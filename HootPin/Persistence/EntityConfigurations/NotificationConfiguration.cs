using HootPin.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace HootPin.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Hoot);
        }
    }
}