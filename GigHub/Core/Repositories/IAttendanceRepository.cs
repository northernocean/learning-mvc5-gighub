﻿using GigHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        void AddAttendance(Attendance attendance);
        Attendance GetAttendance(int gigId, string userId);
        ILookup<int, Attendance> GetFutureAttendancesLookup(string userId);
        IEnumerable<Attendance> GetUpcomingAttendances(string userId);
        bool IsAttending(int gigId, string userId);
        void RemoveAttendance(Attendance attendance);
    }
}