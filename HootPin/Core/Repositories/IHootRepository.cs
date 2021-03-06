﻿using HootPin.Core.Models;
using System.Collections.Generic;

namespace HootPin.Core.Repositories
{
    public interface IHootRepository
    {
        Hoot GetHoot(int hootId);
        Hoot GetHootByUser(int hootId, string userId);
        IEnumerable<Hoot> GetUpcomingHootsByArtist(string artistId);
        IEnumerable<Hoot> GetUpcomingHootsWithArtistsAndGenres();
        IEnumerable<Hoot> GetHootsByQuery(IEnumerable<Hoot> hoots, string query);
        Hoot GetHootWithAttendees(int hootId);
        IEnumerable<Hoot> GetHootsUserAttending(string userId);
        void Add(Hoot hoot);
    }
}
