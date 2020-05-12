using GigHub.Models;
using GigHub.Models.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    public class NotificationsController : ApiController
    {

        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            //userId = "a7d12f08-ec6a-435a-b20d-9364e5b26b63";

            var notificationsInDb = _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Gig.Artist)
                .Include(n => n.Gig.Genre)
                .ToList();

            //var notifications = notificationsInDb.Select(
            //    Mapper.Map<Notification, NotificationDto>); // Not resolving nested objects when using automapper. TODO: Fix this.

            var notifications = notificationsInDb.Select(
                n => new NotificationDto
                {
                    DateTime = n.DateTime,
                    GigId = n.GigId,
                    Venue = n.Venue,
                    OriginalVenue = n.OriginalVenue,
                    OriginalDateTime = n.OriginalDateTime,
                    Type = n.Type,
                    Artist = new ArtistDto { Id = n.Gig.Artist.Id, Name = n.Gig.Artist.Name },
                    Genre = new GenreDto { Id = n.Gig.GenreId, Name = n.Gig.Genre.Name }
                });

            return notifications;

            //IEnumerable<NotificationDto> result = new List<NotificationDto>();

            //foreach (var n in notificationsInDb)
            //{
            //    result.Append(
            //        new NotificationDto
            //        {
            //            DateTime = n.DateTime,
            //            GigId = n.GigId,
            //            Venue = n.Venue,
            //            OriginalVenue = n.OriginalVenue,
            //            OriginalDateTime = n.OriginalDateTime,
            //            Type = n.Type,
            //            Artist = new ArtistDto { Id = n.Gig.Artist.Id, Name = n.Gig.Artist.Name },
            //            Genre = new GenreDto { Id = n.Gig.GenreId, Name = n.Gig.Genre.Name }
            //        });
            //}

            //return JsonConvert.SerializeObject(
            //    notifications,
            //    Formatting.Indented,
            //    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

    }
}
