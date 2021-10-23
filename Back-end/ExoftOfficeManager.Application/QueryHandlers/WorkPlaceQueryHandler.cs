using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Application.Utilities;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class WorkPlaceQueryHandler : IWorkPlaceQueryHandler
    {
        private readonly IRepository<WorkPlace> _repository;

        public WorkPlaceQueryHandler(IRepository<WorkPlace> repo)
        {
            _repository = repo;
        }

        public IEnumerable<WorkPlace> GetAllQuery()
           => _repository.GetAll().ToList();

        public IEnumerable<WorkPlace> GetAllBookedQuery(DateTime date)
            => _repository.GetAll().Include(x => x.Bookings).ToList().Where(x => IsBookedHelper.IsBooked(x, date));

        public IEnumerable<WorkPlace> GetAllAvailableQuery(DateTime date)
            => _repository.GetAll().Include(x => x.Bookings).ToList().Where(x => !IsBookedHelper.IsBooked(x, date));

        public async Task<WorkPlace> FindQuery(long id)
            => await _repository.Find(id);
    }
}
