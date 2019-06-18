using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HootPin.Core.Models
{
    public class Hoot
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public bool IsCanceled { get; private set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Hoot()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.HootCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.HootUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public Hoot Create(ApplicationUser artist, string userId, DateTime dateTime, byte genre, string venue)
        {
            var notification = Notification.HootCreated(this);

            Artist = artist;
            ArtistId = userId;
            DateTime = dateTime;
            GenreId = genre;
            Venue = venue;

            foreach (var follower in Artist.Followers.Select(f => f.Follower))
            {
                follower.Notify(notification);
            }

            return this;
        }
    }
}