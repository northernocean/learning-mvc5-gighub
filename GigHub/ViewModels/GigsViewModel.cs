using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> upcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        public IEnumerable<int> Attendances { get; set; }
        public string AuthenticatedUserId { get; set; } = "";
    }
}