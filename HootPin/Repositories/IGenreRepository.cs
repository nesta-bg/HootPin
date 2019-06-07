using HootPin.Models;
using System.Collections.Generic;

namespace HootPin.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
