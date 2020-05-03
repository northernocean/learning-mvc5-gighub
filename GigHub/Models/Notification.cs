using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {

        protected Notification()
        {
        }

        public Notification(Gig gig, NotificationType notificationType, DateTime originalDateTime, string originalVenue)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = notificationType;
            OriginalDateTime = originalDateTime;
            OriginalVenue = originalVenue;
            NotificationDateCreated = System.DateTime.Now;
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int GigId { get; }

        [Required]
        public NotificationType Type { get; }

        [Required]
        public DateTime DateTime { get; }

        [StringLength(100)]
        public string Venue { get; }

        [Required]
        public DateTime OriginalDateTime { get; }

        [StringLength(100)]
        public string OriginalVenue { get; }

        public DateTime NotificationDateCreated { get; }

        // Navigation Property
        public Gig Gig { get; }

    }
}

