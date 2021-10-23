using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers.Interfaces
{
    public interface IWorkPlaceQueryHandler
    {
        IEnumerable<WorkPlace> GetAllQuery();

        IEnumerable<WorkPlace> GetAllBookedQuery(DateTime date);

        IEnumerable<WorkPlace> GetAllAvailableQuery(DateTime date);

        Task<WorkPlace> FindQuery(long id);
    }
}
