using GigHub.Controllers;
using GigHub.Core.Persistence;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowerRepository Followers { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IGigRepository Gigs { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Attendances = new AttendanceRepository(context);
            Followers = new FollowerRepository(context);
            Genres = new GenreRepository(context);
            Gigs = new GigRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}