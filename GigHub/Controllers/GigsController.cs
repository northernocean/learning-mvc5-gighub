using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;
        private readonly GenreRepository _genreRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var gig = _gigRepository.GetGigWithArtistAndGenre(id);

            if (gig is null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel
            {
                ArtistName = gig.Artist.Name,
                ArtistId = gig.ArtistId,
                Venue = gig.Venue,
                DateTime = gig.DateTime,
                Genre = gig.Genre.Name
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.Following = _context.Followers
                    .Any(f => f.UserId == userId && f.ArtistId == gig.ArtistId);
                viewModel.Attending = _context.Attendances
                    .Any(a => a.AttendeeId == userId && a.GigId == gig.Id);
            }

            return View("GigDetails", viewModel);


        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _genreRepository.GetGenres().ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                GenreId = gig.GenreId,
                Genres = _context.Genres.ToList(),
                Heading = "Edit"
            };
            return View("GigForm", viewModel);

        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancelled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId, DateTime.Now.Date.AddDays(-3)),
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
                    where f.UserId == userId && g.DateTime > dateCutoff && !g.IsCancelled
                    select g;

            //var gigs2 =
            //        _context.Gigs.Include(c => c.Genre).Include(c => c.Artist).Join(
            //            _context.Followers,
            //            g => g.ArtistId,
            //            a => a.ArtistId,
            //            (g, a) => g);




            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetUpcomingGigsWithArtistAndGenre().ToList(),
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
                return View("GigForm", viewModel);

            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Genre = _context.Genres.Single(g => g.Id == viewModel.GenreId),
                Venue = viewModel.Venue
            };
            gig.Create(_context);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig is null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

    }

}
