using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Follower
    {

        public ApplicationUser User { get; set; }

        public ApplicationUser Artist { get; set; }

        [Key]
        [StringLength(128)]
        [Column(Order = 1)]
        public string ArtistId { get; set; }

        [Key]
        [StringLength(128)]
        [Column(Order = 2)]
        public string UserId { get; set; }
    }
}