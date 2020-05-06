using System;

namespace GigHub.Models.Dtos
{
    public class NotificationDto
    {


        public NotificationType Type { get; set; }

        public DateTime? DateTime { get; set; }

        public string Venue { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        public DateTime NotificationDateCreated { get; set; }

        public int GigId { get; set; }

        public ArtistDto Artist { get; set; }

        public GenreDto Genre { get; set; }


    }
}
