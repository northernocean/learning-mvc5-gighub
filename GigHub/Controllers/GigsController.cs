using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var dateCutoff = DateTime.Now.Date.AddDays(-3);
            IEnumerable<Gig> gigs = _context.Attendances
                .Where(c => c.AttendeeId == userId && c.Gig.DateTime > dateCutoff)
                .Select(c => c.Gig)
                .Include(c => c.Artist)
                .Include(c => c.Genre)
                .OrderBy(c => c.DateTime)
                .ToList();

            GigsViewModel viewModel = new GigsViewModel
            {
                upcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {

            var userId = User.Identity.GetUserId();
            var dateCutoff = DateTime.Now.Date.AddDays(-3);

            var gigs =
                    from g in _context.Gigs
                    join f in _context.Followers
                    on g.ArtistId equals f.ArtistId
                    where f.UserId == userId && g.DateTime > dateCutoff
                    select g;

            //var gigs2 =
            //        _context.Gigs.Include(c => c.Genre).Include(c => c.Artist).Join(
            //            _context.Followers,
            //            g => g.ArtistId,
            //            a => a.ArtistId,
            //            (g, a) => g);




            GigsViewModel viewModel = new GigsViewModel
            {
                upcomingGigs = gigs.Include(c => c.Artist).Include(c => c.Genre).ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);

            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Genre = _context.Genres.Single(g => g.Id == viewModel.GenreId),
                Venue = viewModel.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

    }

}
