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

        public bool? Attending { get; set; }

        public bool? Following { get; set; }

        public string EventDate()
        {
            return this.DateTime.ToString("d MMM");
        }

        public string EventTime()
        {
            return this.DateTime.ToString("HH:mm");
        }


    }
}