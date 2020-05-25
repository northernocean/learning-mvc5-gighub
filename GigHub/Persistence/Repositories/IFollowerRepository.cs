using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Controllers
{
    public interface IFollowerRepository
    {
        IEnumerable<Follower> GetFollowersForArtist(string artistId);
        IEnumerable<Gig> GetGigsForArtistsIAmFollowing(string userId);
        bool IsFollowing(string artistId, string userId);
        IEnumerable<string> GetArtistsUserIsFollowing(string userId);
        void Add(Follower follower);
        void Remove(Follower follower);
        Follower GetFollower(string artistId, string userId);
    }
}