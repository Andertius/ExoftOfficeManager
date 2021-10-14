using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Repositories;

namespace ExoftOfficeManager.Business.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IRepository<WorkPlace> _repository;

        public WorkPlaceService(IRepository<WorkPlace> repository)
            => _repository = repository;

        private bool IsBooked(long id)
        {
            var place = Find(id);
            return place.Status == WorkPlaceStatus.Booked ||
                place.Status == WorkPlaceStatus.BookedPermanently ||
                place.Status == (WorkPlaceStatus.FirstHalfBooked | WorkPlaceStatus.SecondHalfBooked);
        }

        public IEnumerable<WorkPlace> GetAll(DateTime date)
            => _repository.GetAll().Where(x => x.Date == date).ToList();

        public IEnumerable<WorkPlace> GetAllBooked(DateTime date)
            => _repository.GetAll().Where(x => x.Date == date && IsBooked(x.Id)).ToList();

        public IEnumerable<WorkPlace> GetAllAvailable(DateTime date)
            => _repository.GetAll().Where(x => x.Date == date && !IsBooked(x.Id)).ToList();

        public WorkPlace Find(long id)
            => _repository.Find(id);

        public async Task Book(long id, long developerId, WorkPlaceStatus status)
        {
            if (status == WorkPlaceStatus.Available)
            {
                throw new ArgumentException($"Cannot book with status '{status}'.");
            }

            if (IsBooked(id))
            {
                throw new ArgumentException($"The work place with id = {id} is already fully booked");
            }
            else
            {
                var place = Find(id);

                if (status == place.Status)
                {
                    throw new ArgumentException($"Cannot book with status '{status}', because the work place already has that status.");
                }

                if (place.Status == WorkPlaceStatus.FirstHalfBooked && status == WorkPlaceStatus.SecondHalfBooked ||
                    place.Status == WorkPlaceStatus.SecondHalfBooked && status == WorkPlaceStatus.FirstHalfBooked)
                {
                    place.Status |= status;
                }
                else
                {
                    place.Status = status;
                }

                place.DeveloperId = developerId;
                await _repository.Update(place);
            }
        }

        public async Task MakeAvailable(long id)
        {
            var place = Find(id);
            place.Status = WorkPlaceStatus.Available;
            await _repository.Update(place);
        }

        public async Task<WorkPlace> Update(WorkPlace place)
            => await _repository.Update(place);

        public async Task Remove(long id)
            => await _repository.Remove(id);
    }
}
