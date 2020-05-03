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
            Notification notification = new Notification(
                gig, NotificationType.GigCancelled, gig.DateTime, gig.Venue);
            _context.Notifications.Add(notification);

            // Create UserNotifications
            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();
            
            foreach (var attendee in attendees)
                attendee.Notify(notification);
            
            _context.SaveChanges();

            return Ok();
        }
    }
}
