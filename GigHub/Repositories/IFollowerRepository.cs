using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Controllers
{
    public interface IFollowerRepository
    {
        IEnumerable<Gig> GetGigsForArtistsIAmFollowing(string userId);
        bool IsFollowing(string artistId, string userId);
    }
}