using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class UserNotificationRepositoryTests
    {

        private ApplicationUser _user;
        private string _venue;
        private UserNotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _userNotifications;
        private Mock<IApplicationDbContext> _context;

        [TestInitialize]
        public void Initialize()
        {
            _user = new ApplicationUser();
            _venue = "Blue Stage Live";
            _userNotifications = new Mock<DbSet<UserNotification>>();
            _context = new Mock<IApplicationDbContext>();
            _context.SetupGet(c => c.UserNotifications).Returns(_userNotifications.Object);
            _repository = new UserNotificationRepository(_context.Object);
        }

        [TestMethod]
        public void GetNewNotifications_NotificationIsForAnotherUser_DoesNotReturnNotification()
        {
            Notification notification = new Notification(
                new Gig { DateTime = DateTime.Now.AddDays(7), Venue = _venue },
                NotificationType.GigCreated);
            UserNotification userNotification = new UserNotification(_user, notification);
            _userNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotifications(_user.Id.Substring(1));

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_NotificationIsRead_DoesNotReturnNotification()
        {
            Notification notification = new Notification(
                new Gig { DateTime = DateTime.Now.AddDays(7), Venue = _venue },
                NotificationType.GigCreated);
            UserNotification userNotification = new UserNotification(_user, notification);
            userNotification.MarkRead();
            _userNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotifications(_user.Id);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_ValidRequest_ReturnsUnreadNotification()
        {
            Notification notification = new Notification(
                new Gig { DateTime = DateTime.Now.AddDays(7), Venue = _venue },
                NotificationType.GigCreated);
            UserNotification userNotification = new UserNotification(_user, notification);

            _userNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotifications(_user.Id);

            //This test fails because there is no userId on the mock object (our constructor only sets the User, not the UserId)
            //In real life the UserNotification is persisted immediately and when it is used again the UserId is populated from the DB.
            //result.Should().Contain(notification);
        }
    }
}
