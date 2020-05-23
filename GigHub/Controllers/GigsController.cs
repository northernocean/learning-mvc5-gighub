using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
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
        private readonly FollowerRepository _followerRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
            _followerRepository = new FollowerRepository(_context);
            _genreRepository = new GenreRepository(_context);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var gig = _gigRepository.GetGig(id);

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
                viewModel.Following = _followerRepository.IsFollowing(gig.ArtistId, userId);
                viewModel.Attending = _attendanceRepository.IsAttending(gig.Id, userId);
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
            var gig = _gigRepository.GetGig(id);

            //A user should only be able to edit their own gigs
            if (gig.ArtistId != userId)
                return View("Home", "Index");

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
            var gigs = _gigRepository.GetMyGigsWithGenre(userId).ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserIsAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {

            var userId = User.Identity.GetUserId();

            var gigs = _followerRepository.GetGigsForArtistsIAmFollowing(userId);

            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetUpcomingGigs().ToList(),
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
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);

            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Genre = _genreRepository.GetGenre(viewModel.GenreId),
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
                viewModel.Genres = _genreRepository.GetGenres().ToList();
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
