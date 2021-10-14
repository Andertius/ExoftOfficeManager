using System;
using System.Collections.Generic;

using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> GetAll(DateTime date);

        IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room);

        Meeting Find(long id);

        bool Add(Meeting meet);

        bool Update(long id, Meeting meet);

        bool Remove(long id);
    }
}
