using GigHub.Controllers;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowerRepository Followers { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Complete();
    }
}