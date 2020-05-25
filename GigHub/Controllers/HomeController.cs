using GigHub.Core.Persistence;
using GigHub.Core.ViewModels;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }

            IEnumerable<int> userAttendances = new List<int>();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                userAttendances = _attendanceRepository
                    .GetUpcomingAttendances(userId)
                    .Select(g => g.GigId);
            }

            IEnumerable<string> userFollowings = new List<string>();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                userFollowings = _context.Followers
                    .Where(f => f.UserId == userId)
                    .Select(a => a.ArtistId);
            }

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs.ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = userAttendances.ToList(),
                Followings = userFollowings.ToList()
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}