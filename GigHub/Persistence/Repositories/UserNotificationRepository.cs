using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetNotifications(string userId)
        {
            return _context.UserNotifications.Where(u => u.UserId == userId);
        }

        // TODO - unit tests
        public IEnumerable<Notification> GetNewNotifications(string userId)
        {
            var x = _context.UserNotifications;
            return _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification);
            //.Include(n => n.Gig.Artist)
            //.Include(n => n.Gig.Genre);
        }


    }
}