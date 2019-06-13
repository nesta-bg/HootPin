using HootPin.Core.Models;
using HootPin.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HootPin.Core.Repositories
{
    public class HootRepository : IHootRepository
    {
        private readonly IApplicationDbContext _context;

        public HootRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Hoot GetHoot(int hootId)
        {
            return _context.Hoots
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .Include(h => h.Attendances)
                .SingleOrDefault(h => h.Id == hootId);
        }

        public Hoot GetHootByUser(int hootId, string userId)
        {
            return _context.Hoots
                .Single(h => h.Id == hootId && h.ArtistId == userId);
        }

        public IEnumerable<Hoot> GetUpcomingHootsByArtist(string artistId)
        {
            return _context.Hoots
                .Where(h =>
                    h.ArtistId == artistId &&
                    h.DateTime > DateTime.Now &&
                    !h.IsCanceled)
                .Include(h => h.Genre)
                .OrderBy(h => h.DateTime)
                .ToList();
        }

        //consider IQueryable<Hoot> as option
        public IEnumerable<Hoot> GetUpcomingHootsWithArtistsAndGenres()
        {
            return _context.Hoots
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .Where(h => h.DateTime > DateTime.Now && !h.IsCanceled)
                .OrderBy(h => h.DateTime)
                .ToList();
        }

        public IEnumerable<Hoot> GetHootsByQuery(IEnumerable<Hoot> hoots, string query)
        {
            return hoots
                .Where(h =>
                       h.Artist.Name.Contains(query) ||
                       h.Genre.Name.Contains(query) ||
                       h.Venue.Contains(query))
                .OrderBy(h => h.DateTime);
        }

        public Hoot GetHootWithAttendees(int hootId)
        {
            return _context.Hoots
                .Include(h => h.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(h => h.Id == hootId);
        }

        public IEnumerable<Hoot> GetHootsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Hoot)
                .Include(h => h.Artist)
                .Include(h => h.Genre)
                .OrderBy(h => h.DateTime)
                .ToList();
        }

        public void Add(Hoot hoot)
        {
            _context.Hoots.Add(hoot);
        }
    }
}