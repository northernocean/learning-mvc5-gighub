using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {

        public Notification()
        {
            NotificationDateCreated = System.DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public DateTime? DateTime { get; set; }

        [StringLength(100)]
        public string Venue { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        [StringLength(100)]
        public string OriginalVenue { get; set; }

        [Required]
        public int GigId { get; set; }

        public Gig Gig { get; set; }

        public DateTime NotificationDateCreated { get; set; }

    }
}

