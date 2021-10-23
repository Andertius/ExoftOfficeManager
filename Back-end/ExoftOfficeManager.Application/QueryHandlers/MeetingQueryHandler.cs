using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class MeetingQueryHandler : IMeetingQueryHandler
    {
        public Task<Meeting> FindQuery(long meetingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSpan> GetAllAvailableHoursQuery(DateTime date, int room)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meeting> GetAllQuery(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
