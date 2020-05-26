using GigHub.Core.Repositories;

namespace GigHub.Core
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