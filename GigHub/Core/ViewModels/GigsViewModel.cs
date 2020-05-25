using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        public IEnumerable<int> Attendances { get; set; } = new List<int>();
        public IEnumerable<string> Followings { get; set; } = new List<string>();
        public string AuthenticatedUserId { get; set; } = "";
    }
}