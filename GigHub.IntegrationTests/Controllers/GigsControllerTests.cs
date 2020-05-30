using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using FluentAssertions;
using System.Runtime.Remoting.Contexts;
using System.Linq;
using GigHub.Core.ViewModels;
using System.Data.Entity;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {

        private GigsController _controller;
        private ApplicationDbContext _context;
        private string _artistUserId;
        private string _artistUserName;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
            _artistUserId = "fd7e6cb5-a12e-499f-a050-607567f3c8b8";
            _artistUserName = "artist@gighub.com";
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            _controller.MockCurrentUser(_artistUserId, _artistUserName);
            
            var result = (ViewResult)_controller.Mine();

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(3);

        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGig()
        {
            _controller.MockCurrentUser(_artistUserId, _artistUserName);
            Gig gig = _context.Gigs.Where(g => g.Id == 1).Include(g => g.Genre).Include(a => a.Artist).SingleOrDefault();

            var result = _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("1 Jan 2099"),
                Time = gig.DateTime.ToString("07:00"),
                Venue = "Raggedy Anne",
                GenreId = 1
            });

            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(new DateTime(2099, 1, 1,7,0,0));
            gig.Venue.Should().Be("Raggedy Anne");
            gig.GenreId.Should().Be(1);


        } 
        
        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldCancelTheGig()
        {
            _controller.MockCurrentUser(_artistUserId, _artistUserName);
            Gig gig = _context.Gigs.Where(g => g.Id == 1).Include(g => g.Genre).Include(a => a.Artist).SingleOrDefault();

            gig.Cancel();
            _context.SaveChanges();
            
            _context.Entry(gig).Reload();
            gig.IsCancelled.Should().BeTrue();
        } 
    }
}
