using GigHub.Core;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Attend(int id)
        {

            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Attendances.IsAttending(id, userId))
                return BadRequest();

            var attendance = new Attendance
            {
                GigId = id,
                AttendeeId = User.Identity.GetUserId()
            };

            _unitOfWork.Attendances.AddAttendance(attendance);
            _unitOfWork.Complete();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult CancelAttendance(int id)
        {

            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance is null)
                return BadRequest();

            _unitOfWork.Attendances.RemoveAttendance(attendance);
            _unitOfWork.Complete();
            return Ok(id);

        }


    }
}
