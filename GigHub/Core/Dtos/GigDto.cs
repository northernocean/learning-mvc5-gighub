using System;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCancelled { get; private set; }
        public ArtistDto ArtistDto { get; private set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto GenreDto { get; set; }
    }
}