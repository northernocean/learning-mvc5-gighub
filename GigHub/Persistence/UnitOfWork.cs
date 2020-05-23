using GigHub.Controllers;
using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public AttendanceRepository Attendances { get; private set; }
        public FollowerRepository Followers { get; private set; }
        public GenreRepository Genres { get; private set; }
        public GigRepository Gigs { get; private set; }

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