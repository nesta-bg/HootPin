﻿using System.Collections.Generic;

namespace HootPin.Dtos
{
    public class NotificationsDto
    {
        public IEnumerable<NotificationDto> RecentNotifications { get; set; }
        public int NumberOfNewNotifications { get; set; }
    }
}