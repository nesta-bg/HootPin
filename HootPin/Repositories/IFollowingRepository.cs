using HootPin.Models;
using System.Collections.Generic;

namespace HootPin.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
        IEnumerable<ApplicationUser> GetFollowees(string followerId);
    }
}
