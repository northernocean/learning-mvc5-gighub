using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        [Required]
        public string ArtistName { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public bool Attending { get; set; }

        [Required]
        public bool Following { get; set; }

    }
}