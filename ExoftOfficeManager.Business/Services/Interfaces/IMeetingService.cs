using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> GetAll(DateTime date);

        IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room);

        Meeting Find(long id);

        Task<bool> Add(Meeting meet);

        Task<Meeting> Update(Meeting meet);

        Task Remove(long id);
    }
}
