using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        ILookup<int, Attendance> GetFutureAttendancesLookup(string userId);
        IEnumerable<Attendance> GetUpcomingAttendances(string userId);
        bool IsAttending(int gigId, string userId);
    }
}