using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

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

        public virtual ICollection<Attendance> Attendances { get; }

        public void Cancel()
        {
            IsCancelled = true;

            Notification notification = Notification.NewGigCancelledNotification(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Modify(DateTime date, string venue, int genre)
        {

            DateTime originalDateTime = DateTime;
            string originalVenue = venue;

            DateTime = date;
            Venue = venue;
            GenreId = genre;

            Notification notification = Notification.NewGigUpdatedNotification(
                this, originalDateTime, originalVenue);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

    }
}