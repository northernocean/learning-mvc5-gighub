using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public int NotificationId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        public bool IsRead { get; set; }

        // Navigation Properties
        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public Notification Notification { get; set; }

    }
}

