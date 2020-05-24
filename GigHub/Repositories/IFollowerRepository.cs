using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Controllers
{
    public interface IFollowerRepository
    {
        IEnumerable<Follower> GetFollowersForArtist(string artistId);
        IEnumerable<Gig> GetGigsForArtistsIAmFollowing(string userId);
        bool IsFollowing(string artistId, string userId);
    }
}