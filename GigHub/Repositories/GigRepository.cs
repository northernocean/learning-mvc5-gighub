using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
               .Include(g => g.Attendances.Select(a => a.Attendee))
               .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId, DateTime dateCutoff)
        {
            return _context.Attendances
                .Where(c => c.AttendeeId == userId && c.Gig.DateTime > dateCutoff)
                .Select(c => c.Gig)
                .Include(c => c.Artist)
                .Include(c => c.Genre)
                .OrderBy(c => c.DateTime)
                .ToList();
        }

        public Gig GetGigWithArtistAndGenre(int id)
        {
            return _context.Gigs
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigsWithArtistAndGenre()
        {
            return _context.Gigs
               .Include(g => g.Artist)
               .Include(g => g.Genre)
               .Where(g => g.DateTime > DateTime.Now);
        }

    }
}