using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class NotificationsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public IHttpActionResult MarkRead()
        {
            var userId = User.Identity.GetUserId();
            foreach (var notification in _unitOfWork.UserNotifications.GetNotifications(userId).ToList())
            {
                notification.MarkRead();
            }
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            //userId = "a7d12f08-ec6a-435a-b20d-9364e5b26b63";

            var notificationsInDb = _unitOfWork.UserNotifications
                .GetNewNotifications(userId)
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
