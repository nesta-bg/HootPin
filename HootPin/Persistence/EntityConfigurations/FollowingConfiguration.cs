using HootPin.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace HootPin.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(t => new { t.FollowerId, t.FolloweeId });
        }
    }
}