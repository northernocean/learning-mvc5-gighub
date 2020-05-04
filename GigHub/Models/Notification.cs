using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {

        protected Notification()
        {
        }

        public Notification(Gig gig, NotificationType notificationType)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = notificationType;
            OriginalDateTime = gig.DateTime;
            OriginalVenue = gig.Venue;
            NotificationDateCreated = System.DateTime.Now;
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int GigId { get; private set; }

        [Required]
        public NotificationType Type { get; private set; }

        public DateTime? DateTime { get; private set; }

        [StringLength(100)]
        public string Venue { get; private set; }

        [Required]
        public DateTime OriginalDateTime { get; private set; }

        [StringLength(100)]
        public string OriginalVenue { get; private set; }

        public DateTime NotificationDateCreated { get; private set; }

        // Navigation Property
        public Gig Gig { get; private set; }

    }
}

