﻿using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig is null)
                return NotFound();

            if (gig.ArtistId != userId)
                return BadRequest();

            if (gig.IsCancelled)
                return NotFound();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
