using HootPin.Core.Models;
using System;

namespace HootPin.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public HootDto Hoot { get; private set; }
    }
}