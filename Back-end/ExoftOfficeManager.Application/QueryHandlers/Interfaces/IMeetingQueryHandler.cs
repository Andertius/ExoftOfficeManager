using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers.Interfaces
{
    public interface IMeetingQueryHandler
    {
        IEnumerable<Meeting> GetAllQuery(DateTime date);

        IEnumerable<TimeSpan> GetAllAvailableHoursQuery(DateTime date, int room);

        Task<Meeting> FindQuery(long meetingId);
    }
}
