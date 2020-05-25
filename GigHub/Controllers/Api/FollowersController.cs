using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowersController : ApiController
    {

        IUnitOfWork _unitOfWork;

        public FollowersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Follow(string id)
        {

            var userId = User.Identity.GetUserId();
            if (_unitOfWork.Followers.IsFollowing(id, userId))
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

                _unitOfWork.Followers.Add(follower);
                _unitOfWork.Complete();
            }

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {

            var userId = User.Identity.GetUserId();

            var follower = _unitOfWork.Followers.GetFollower(id, userId);

            if (follower is null)
                return BadRequest();

            _unitOfWork.Followers.Remove(follower);
            _unitOfWork.Complete();

            return Ok(id);

        }
    }
}
