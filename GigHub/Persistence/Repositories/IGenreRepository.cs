using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Persistence.Repositories
{
    public interface IGenreRepository
    {
        Genre GetGenre(int id);
        IEnumerable<Genre> GetGenres();
    }
}