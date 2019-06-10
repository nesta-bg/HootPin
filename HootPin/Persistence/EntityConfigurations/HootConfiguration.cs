using HootPin.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace HootPin.Persistence.EntityConfigurations
{
    public class HootConfiguration : EntityTypeConfiguration<Hoot>
    {
        public HootConfiguration()
        {
            //1.KEYS
            //2.PROPERTIES (Alphabetically)
            //3.RELATIONSHIPS

            Property(h => h.ArtistId)
                .IsRequired();

            Property(h => h.GenreId)
                .IsRequired();

            Property(h => h.Venue)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(a => a.Attendances)
                .WithRequired(a => a.Hoot)
                .WillCascadeOnDelete(false);
        }
    }
}