using HootPin.Models;

namespace HootPin.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GeArtistWithFollowers(string artistId);
    }
}
