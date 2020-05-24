using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IGenreRepository
    {
        Genre GetGenre(int id);
        IEnumerable<Genre> GetGenres();
    }
}