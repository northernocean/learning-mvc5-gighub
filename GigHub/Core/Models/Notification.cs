using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {

        // Constructors

        protected Notification() { }

        public Notification(Gig gig, NotificationType notificationType)
            : this(gig, notificationType, null, null) { }

        public Notification(Gig gig, NotificationType notificationType, DateTime? originalDateTime, string originalVenue)
        {
            Gig = gig ?? throw new ArgumentNullException("gig");
            Type = notificationType;

            DateTime = gig.DateTime;
            Venue = gig.Venue;

            if (notificationType == NotificationType.GigUpdated)
            {
                OriginalDateTime = originalDateTime;
                OriginalVenue = originalVenue;
            }

            NotificationDateCreated = System.DateTime.Now;
        }

        // Properties

        [Key]
        public int Id { get; private set; }

        [Required]
        public int GigId { get; private set; }

        [Required]
        public NotificationType Type { get; private set; }

        public DateTime? DateTime { get; private set; }

        [StringLength(100)]
        public string Venue { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        [StringLength(100)]
        public string OriginalVenue { get; private set; }

        public DateTime NotificationDateCreated { get; private set; }

        // Navigation Properties

        public Gig Gig { get; private set; }

        //Static factory methods

        public static Notification NewGigUpdatedNotification(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            return new Notification(gig, NotificationType.GigUpdated, originalDateTime, originalVenue);
        }

        public static Notification NewGigCancelledNotification(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCancelled);
        }

        public static Notification NewGigCreatedNotification(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

    }
}

