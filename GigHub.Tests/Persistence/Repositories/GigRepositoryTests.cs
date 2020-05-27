using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {

        private GigRepository _repository;
        private Mock<DbSet<Gig>> _gigs;
        private Mock<IApplicationDbContext> _context;
        private string _artistId = "1";
        //private string _userId = "2";

        [TestInitialize]
        public void Initialize()
        {
            _gigs = new Mock<DbSet<Gig>>();
            _context = new Mock<IApplicationDbContext>();
            _context.SetupGet(c => c.Gigs).Returns(_gigs.Object);
            _repository = new GigRepository(_context.Object);
            _artistId = "1";
        }

        [TestMethod]
        public void GetGigs_GigsIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig()
            {
                DateTime = DateTime.UtcNow.ToLocalTime().AddDays(-7),
                ArtistId = _artistId
            };
            _gigs.SetSource(new[] { gig });

            var result = _repository.GetGigs(_artistId);

            result.Should().BeEmpty();
        }
        
        [TestMethod]
        public void GetGigs_GigsIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig()
            {
                DateTime = DateTime.UtcNow.ToLocalTime().AddDays(7),
                ArtistId = _artistId
            };
            gig.Cancel();
            _gigs.SetSource(new[] { gig });

            var result = _repository.GetGigs(_artistId);

            result.Should().BeEmpty();
        }
        
        [TestMethod]
        public void GetGigs_GigsIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig()
            {
                DateTime = DateTime.UtcNow.ToLocalTime().AddDays(7),
                ArtistId = _artistId
            };
            _gigs.SetSource(new[] { gig });

            var result = _repository.GetGigs(_artistId + "_");

            result.Should().BeEmpty();
        }
        
        [TestMethod]
        public void GetGigs_GigIsForArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig()
            {
                DateTime = DateTime.UtcNow.ToLocalTime().AddDays(7),
                ArtistId = _artistId
            };
            _gigs.SetSource(new[] { gig });

            var result = _repository.GetGigs(_artistId);

            result.Should().Contain(gig);
        }



    }
}
