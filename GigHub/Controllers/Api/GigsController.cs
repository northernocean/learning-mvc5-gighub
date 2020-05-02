using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {

        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
                return NotFound();

            // Cancel the Gig
            gig.IsCancelled = true;

            // Create a Notification
            Notification notification = new Notification
            {
                Type = NotificationType.GigCancelled,
                GigId = gig.Id,
                OriginalDateTime = gig.DateTime,
                OriginalVenue = gig.Venue
            };
            _context.Notifications.Add(notification);

            // Create UserNotifications
            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.AttendeeId)
                .ToList();
            foreach (var attendee in attendees)
            {
                UserNotification userNotification = new UserNotification
                {
                    UserId = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
