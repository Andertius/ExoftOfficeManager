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
        private readonly IRepository<WorkPlace> _placeRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public WorkPlaceService(IRepository<WorkPlace> placeRepository, IRepository<Booking> bookingRepository)
        {
            _placeRepository = placeRepository;
            _bookingRepository = bookingRepository;
        }

        private bool IsBooked(long id, DateTime date)
        {
            var place = Find(id);
            var bookings = place.Bookings.Where(x => x.Date == date);

            if (!bookings.Any())
            {
                return false;
            }
            else if (bookings.Count() == 1 &&
                (bookings.First().Status == WorkPlaceStatus.Booked || bookings.First().Status == WorkPlaceStatus.BookedPermanently))
            {
                return true;
            }
            else if (bookings.Count() == 2)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<WorkPlace> GetAll()
            => _placeRepository.GetAll().ToList();

        public IEnumerable<WorkPlace> GetAllBooked(DateTime date)
            => _placeRepository.GetAll().Where(x => IsBooked(x.Id, date)).ToList();

        public IEnumerable<WorkPlace> GetAllAvailable(DateTime date)
            => _placeRepository.GetAll().Where(x => !IsBooked(x.Id, date)).ToList();

        public WorkPlace Find(long id)
            => _placeRepository.Find(id);

        public async Task Book(long id, long developerId, WorkPlaceStatus status, DateTime date, int days)
        {
            if (status == WorkPlaceStatus.Available)
            {
                throw new ArgumentException($"Cannot book with status '{status}'.");
            }

            if (IsBooked(id, date))
            {
                throw new ArgumentException($"The work place with id = {id} is already fully booked");
            }
            else
            {
                var place = Find(id);

                if (place.Bookings.Where(x => x.Date == date && x.Status == status).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{status}', because the work place already has that status.");
                }

                for (int i = 0; i < days; i++)
                {
                    place.Bookings.Add(new Booking { Date = new DateTime(date.Year, date.Month, date.Day + i), Status = status, UserId = developerId });
                }

                await _placeRepository.Update(place);
                await _placeRepository.Commit();
            }
        }

        public async Task MakeAvailable(long id, DateTime date, long devId)
        {
            var place = Find(id);
            place.Bookings.Remove(place.Bookings.Where(x => x.Date == date && x.UserId == devId).FirstOrDefault());
            await _placeRepository.Update(place);
            await _placeRepository.Commit();
        }

        public async Task<WorkPlace> Update(WorkPlace place)
        {
            var result = await _placeRepository.Update(place);
            await _placeRepository.Commit();

            return result;
        }

        public async Task Remove(long id)
        {
            await _placeRepository.Remove(id);
            await _placeRepository.Commit();
        }
    }
}
