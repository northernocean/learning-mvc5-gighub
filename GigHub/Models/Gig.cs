using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {

        public int Id { get; set; }

        public bool IsCancelled { get; set; }

        [Required]
        [StringLength(128)]
        public string ArtistId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Venue { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ApplicationUser Artist { get; set; }

    }
}