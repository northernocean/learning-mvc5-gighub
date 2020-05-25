using GigHub.Core.Models;
using GigHub.Core.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres;
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.SingleOrDefault(g => g.Id == id);
        }
    }
}