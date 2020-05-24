using GigHub.Models;
using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var gig = _unitOfWork.Gigs.GetGig(id);

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
                viewModel.Following = _unitOfWork.Followers.IsFollowing(gig.ArtistId, userId);
                viewModel.Attending = _unitOfWork.Attendances.IsAttending(gig.Id, userId);
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
                Genres = _unitOfWork.Genres.GetGenres().ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(id);

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
                Genres = _unitOfWork.Genres.GetGenres().ToList(),
                Heading = "Edit"
            };
            return View("GigForm", viewModel);

        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigs(userId).ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserIsAttending(userId),
                Attendances = _unitOfWork.Attendances
                                .GetUpcomingAttendances(userId)
                                .Select(g => g.GigId).ToList(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {

            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.Followers.GetGigsForArtistsIAmFollowing(userId);

            GigsViewModel viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs().ToList(),
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);

            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Genre = _unitOfWork.Genres.GetGenre(viewModel.GenreId),
                Venue = viewModel.Venue
            };
            gig.Create(_unitOfWork);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres().ToList();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig is null)
                return HttpNotFound();
            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

    }

}
