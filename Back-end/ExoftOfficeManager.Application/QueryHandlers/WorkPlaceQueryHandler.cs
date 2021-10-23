using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class WorkPlaceQueryHandler : IWorkPlaceQueryHandler
    {
        public Task<WorkPlace> FindQuery(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkPlace> GetAllAvailableQuery(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkPlace> GetAllBookedQuery(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkPlace> GetAllQuery()
        {
            throw new NotImplementedException();
        }
    }
}
