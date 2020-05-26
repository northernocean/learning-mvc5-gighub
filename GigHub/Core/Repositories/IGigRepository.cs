using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int id);
        IEnumerable<Gig> GetGigs(string artistId);
        IEnumerable<int> GetGigsIdsForGigsUserIsAttending(string userId);
        IEnumerable<Gig> GetGigsUserIsAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigs();
    }
}