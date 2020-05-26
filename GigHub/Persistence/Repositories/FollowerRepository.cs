using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Follower follower)
        {
            _context.Followers.Add(follower);
        }

        public void Remove(Follower follower)
        {
            _context.Followers.Remove(follower);
        }

        public Follower GetFollower(string artistId, string userId)
        {
            return _context.Followers
                .SingleOrDefault(f => f.ArtistId == artistId && f.UserId == userId);
        }

        public bool IsFollowing(string artistId, string userId)
        {
            return _context.Followers
                .Any(f => f.UserId == userId && f.ArtistId == artistId);
        }

        public IEnumerable<Follower> GetFollowersForArtist(string artistId)
        {
            return _context.Followers.Include(c => c.User).Where(a => a.ArtistId == artistId);
        }

        public IEnumerable<string> GetArtistsUserIsFollowing(string userId)
        {
            return _context.Followers
                .Where(f => f.UserId == userId)
                .Select(a => a.ArtistId);
        }

        public IEnumerable<Gig> GetGigsForArtistsIAmFollowing(string userId)
        {
            return (
                from g in _context.Gigs
                join f in _context.Followers
                on g.ArtistId equals f.ArtistId
                where f.UserId == userId && g.DateTime > System.DateTime.Now.AddDays(-1) && !g.IsCancelled
                select g
                ).AsEnumerable();

            //var gigs2 =
            //        _context.Gigs.Include(c => c.Genre).Include(c => c.Artist).Join(
            //            _context.Followers,
            //            g => g.ArtistId,
            //            a => a.ArtistId,
            //            (g, a) => g);

        }

    }
}