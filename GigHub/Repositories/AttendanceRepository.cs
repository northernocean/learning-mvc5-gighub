using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsAttending(int gigId, string userId)
        {
            return _context.Attendances
                    .Any(a => a.AttendeeId == userId && a.GigId == gigId);
        }

        public IEnumerable<Attendance> GetUpcomingAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public ILookup<int, Attendance> GetFutureAttendancesLookup(string userId)
        {
            //Here as an example so I dont forget ToLookup()
            return GetUpcomingAttendances(userId).ToLookup(a => a.GigId);
        }

    }
}