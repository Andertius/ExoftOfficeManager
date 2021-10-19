using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess.Entities;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> GetAll(DateTime date, IEnumerable<string> inclusion);

        IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room);

        Task<Meeting> Find(long id, IEnumerable<string> inclusion);

        Task<bool> Add(Meeting meet);

        Task<Meeting> Update(Meeting meet);

        Task Remove(long id);
    }
}
