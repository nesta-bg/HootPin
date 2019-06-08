using HootPin.Core.Models;
using HootPin.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace HootPin.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GeArtistWithFollowers(string artistId)
        {
            return _context.Users
                .Include(u => u.Followers.Select(f => f.Follower))
                .Single(u => u.Id == artistId);
        }
    }
}