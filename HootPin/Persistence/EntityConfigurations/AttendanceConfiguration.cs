using HootPin.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace HootPin.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(t => new { t.HootId, t.AttendeeId });
        }
    }
}