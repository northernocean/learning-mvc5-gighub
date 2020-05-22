using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsAttending(string userId, int gigId)
        {
            return _context.Attendances
                    .Any(a => a.AttendeeId == userId && a.GigId == gigId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public ILookup<int, Attendance> GetFutureAttendancesLookup(string userId)
        {
            //Here as an example so I dont forget ToLookup()
            return GetFutureAttendances(userId).ToLookup(a => a.GigId);
        }

    }
}