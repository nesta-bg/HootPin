using HootPin.Core.Models;

namespace HootPin.Core.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GeArtistWithFollowers(string artistId);
    }
}
