using HootPin.Core.Models;
using System.Collections.Generic;

namespace HootPin.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
        IEnumerable<ApplicationUser> GetFollowees(string followerId);
        void Add(Following following);
        void Remove(Following following);
    }
}
