using HootPin.Core.Models;
using HootPin.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HootPin.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
        }

        public IEnumerable<ApplicationUser> GetFollowees(string followerId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == followerId)
                .Select(f => f.Followee)
                .ToList();
        }
    }
}