using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userId);
        IEnumerable<UserNotification> GetNotifications(string userId);
    }
}