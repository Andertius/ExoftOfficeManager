using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Entities;
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
            var bookings = _placeRepository
                .GetAll(new[] { nameof(WorkPlace.Bookings) })
                .FirstOrDefault(x => x.Id == id)
                .Bookings.Where(x => x.Date == date);

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

        public IEnumerable<WorkPlace> GetAll(IEnumerable<string> inclusion)
            => _placeRepository.GetAll(inclusion).ToList();

        public IEnumerable<WorkPlace> GetAllBooked(DateTime date, IEnumerable<string> inclusion)
            =>_placeRepository.GetAll(inclusion).Where(x => IsBooked(x.Id, date)).ToList();

        public IEnumerable<WorkPlace> GetAllAvailable(DateTime date, IEnumerable<string> inclusion)
            => _placeRepository.GetAll(inclusion).Where(x => !IsBooked(x.Id, date)).ToList();

        public IEnumerable<Booking> GetAllUserBooked(long id, IEnumerable<string> inclusion)
            => _placeRepository.GetAll(inclusion).SelectMany(x => x.Bookings).Where(x => x.UserId == id);

        public async Task<WorkPlace> Find(long id, IEnumerable<string> inclusion)
            => await _placeRepository.Find(id, inclusion);

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
                var place = await Find(id, new[] { nameof(WorkPlace.Bookings) });

                if (place.Bookings.Where(x => x.Date == date && x.Status == status).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{status}', because the work place already has that status.");
                }

                for (int i = 0; i < days; i++)
                {
                    place.Bookings.Add(new Booking
                    {
                        Date = new DateTime(date.Year, date.Month, date.Day + i),
                        Status = status,
                        UserId = developerId,
                        WorkPlaceId = id,
                    });
                }

                _placeRepository.Update(place);
                await _placeRepository.Commit();
            }
        }

        public async Task MakeAvailable(long id, DateTime date, long devId)
        {
            var place = await Find(id, new[] { nameof(WorkPlace.Bookings) });
            place.Bookings.Remove(place.Bookings.FirstOrDefault(x => x.Date == date && x.UserId == devId));
            _placeRepository.Update(place);
            await _placeRepository.Commit();
        }

        public async Task<WorkPlace> Update(WorkPlace place)
        {
            var result = _placeRepository.Update(place);
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
