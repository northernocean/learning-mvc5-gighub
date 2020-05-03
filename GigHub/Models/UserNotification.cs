using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {

        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException("user");
            Notification = notification ?? throw new ArgumentNullException("notification");
        }

        [Key]
        [Column(Order = 1)]
        public int NotificationId { get; private set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(128)]
        public string UserId { get; private set; }

        [Required]
        public bool IsRead { get; set; }

        // Navigation Properties
        [Required]
        public Notification Notification { get; private set; }

        [Required]
        public ApplicationUser User { get; private set; }

    }
}

