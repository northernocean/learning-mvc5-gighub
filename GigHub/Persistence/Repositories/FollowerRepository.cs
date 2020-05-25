using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Persistence;

namespace GigHub.Controllers
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowerRepository(ApplicationDbContext context)
        {
            _context = context;
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