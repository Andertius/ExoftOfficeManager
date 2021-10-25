using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Application.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IRepository<WorkPlace> _placeRepository;
        private readonly IRepository<User> _userRepository;

        public WorkPlaceService(IRepository<WorkPlace> placeRepository, IRepository<User> userRepository)
        {
            _placeRepository = placeRepository;
            _userRepository = userRepository;
        }

        private static bool IsBooked(WorkPlace place, DateTime date)
        {
            var bookings = place.Bookings.Where(x => x.Date == date);

            if (!bookings.Any())
            {
                return false;
            }
            else if (bookings.Count() == 1 &&
                (bookings.First().Type == BookingType.Booked || bookings.First().Type == BookingType.BookedPermanently))
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
            => _placeRepository.GetAll().Include(x => x.Bookings).ToList().Where(x => IsBooked(x, date));

        public IEnumerable<WorkPlace> GetAllAvailable(DateTime date)
            => _placeRepository.GetAll().Include(x => x.Bookings).ToList().Where(x => !IsBooked(x, date));

        public async Task<WorkPlace> Find(long id)
            => await _placeRepository.Find(id);

        public async Task Book(long id, long developerId, BookingType type, DateTime date, int days)
        {
            if (type == BookingType.Available)
            {
                throw new ArgumentException($"Cannot book with status '{type}'.");
            }

            var place = await Find(id);

            if (IsBooked(place, date))
            {
                throw new ArgumentException($"The work place with id = {id} is already fully booked");
            }
            else
            {
                if (place.Bookings.Where(x => x.Date == date && x.Type == type).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{type}', because the work place already has that status.");
                }

                if (days > 1)
                {
                    for (int i = 0; i < days; i++)
                    {
                        place.Bookings.Add(new Booking
                        {
                            Date = new DateTime(date.Year, date.Month, date.Day + i),
                            Type = type,
                            Status = BookingStatus.Pending,
                            User = await _userRepository.Find(developerId),
                            WorkPlace = await _placeRepository.Find(id),
                            DayNumber = days,
                        });
                    }
                }
                else
                {
                    place.Bookings.Add(new Booking
                    {
                        Date = new DateTime(date.Year, date.Month, date.Day),
                        Type = type,
                        Status = BookingStatus.Approved,
                        User = await _userRepository.Find(developerId),
                        WorkPlace = await _placeRepository.Find(id),
                        DayNumber = days,
                    });
                }

                _placeRepository.Update(place);
                await _placeRepository.Commit();
            }
        }

        public async Task<WorkPlace> Update(WorkPlace place)
        {
            var result = _placeRepository.Update(place);
            await _placeRepository.Commit();

            return result;
        }

        public async Task Remove(long id)
        {
            _placeRepository.Remove(id);
            await _placeRepository.Commit();
        }
    }
}
