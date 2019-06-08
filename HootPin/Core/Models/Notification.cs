using System;
using System.ComponentModel.DataAnnotations;

namespace HootPin.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        [Required]
        public Hoot Hoot { get; private set; }

        public Notification()
        {

        }

        private Notification(NotificationType type, Hoot hoot)
        {
            if (hoot == null)
                throw new ArgumentNullException("hoot");

            Type = type;
            Hoot = hoot;
            DateTime = DateTime.Now;
        }

        public static Notification HootCanceled(Hoot hoot)
        {
            return new Notification(NotificationType.HootCanceled, hoot);
        }

        // public static Notification HootUpdated(Hoot newHoot, Hoot oldHoot)
        public static Notification HootUpdated(Hoot newHoot, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.HootUpdated, newHoot);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification HootCreated(Hoot hoot)
        {
            return new Notification(NotificationType.HootCreated, hoot);
        }
    }
}