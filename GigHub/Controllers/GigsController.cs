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
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var dateCutoff = DateTime.Now.Date.AddDays(-1);
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

            return RedirectToAction("Index", "Home");
        }

    }

}
