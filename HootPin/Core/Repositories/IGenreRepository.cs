using HootPin.Core.Models;
using System.Collections.Generic;

namespace HootPin.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
