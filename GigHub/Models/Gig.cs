using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public ApplicationUser Artist { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }

    }
}