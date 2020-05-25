using GigHub.Core.Persistence;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
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

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs();

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
                userAttendances = _unitOfWork.Attendances 
                    .GetUpcomingAttendances(userId)
                    .Select(g => g.GigId);
            }

            IEnumerable<string> userFollowings = new List<string>();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                userFollowings = _unitOfWork.Followers.GetArtistsUserIsFollowing(userId);
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