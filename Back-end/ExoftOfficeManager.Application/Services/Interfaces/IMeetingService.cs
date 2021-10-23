using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> GetAll(DateTime date);

        IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room);

        Task<Meeting> Find(long meetingId);

        Task<bool> Add(Meeting meet);

        Task<Meeting> Update(Meeting meet);

        Task Remove(long meetingId);
    }
}
