﻿using GigHub.Controllers;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowerRepository Followers { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }

        void Complete();
    }
}