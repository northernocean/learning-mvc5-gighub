using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowersController : ApiController
    {

        ApplicationDbContext _context;

        public FollowersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult Follow(string id)
        {

            var userId = User.Identity.GetUserId();

            if (_context.Followers
                .Any(f => f.UserId == userId && f.ArtistId == id))
            {
                return BadRequest();
            }
            else
            {
                Follower follower = new Follower
                {
                    ArtistId = id,
                    UserId = userId
                };

                _context.Followers.Add(follower);
                _context.SaveChanges();
            }

            return Ok();

        }
        
        [HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {

            var userId = User.Identity.GetUserId();

            var follower = _context.Followers
                .SingleOrDefault(f => f.ArtistId == id && f.UserId == userId);

            if (follower is null)
                return BadRequest();

            _context.Followers.Remove(follower);
            _context.SaveChanges();

            return Ok();

        }
    }
}
