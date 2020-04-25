using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {

        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult Attend(int id)
        {

            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(
                a => a.GigId == id && a.AttendeeId == userId))
            {
                return BadRequest();
            }
            else
            {
                var attendance = new Attendance
                {
                    GigId = id,
                    AttendeeId = User.Identity.GetUserId()
                };

                _context.Attendances.Add(attendance);
                _context.SaveChanges();
            }

            return Ok();

        }


    }
}
