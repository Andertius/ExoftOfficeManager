using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;
        private readonly IRepository<WorkPlace> _placeRepository;
        private readonly IRepository<User> _userRepository;

        public BookingService(IRepository<Booking> repository, IRepository<WorkPlace> placeRepository, IRepository<User> userRepository)
        {
            _repository = repository;
            _placeRepository = placeRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Booking> GetAll(DateTime date)
            => _repository.GetAll().Where(x => x.Date == date);

        public IEnumerable<Booking> GetAllPendingBookings()
            => _repository.GetAll()
                .Where(x => x.Status == BookingStatus.Pending)
                .Select(x => new Booking
                {
                    Id = x.Id,
                    Date = x.Date,
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated,
                    DayNumber = x.DayNumber,
                    Status = x.Status,
                    Type = x.Type,
                    User = new User { Id = x.User.Id, Avatar = x.User.Avatar, FullName = x.User.FullName },
                    WorkPlace = new WorkPlace { Id = x.WorkPlace.Id, FloorNumber = x.WorkPlace.FloorNumber, PlaceNumber = x.WorkPlace.PlaceNumber },
                });

        public IEnumerable<Booking> GetAllUserBooked(long userId)
        {
            var user = _userRepository.Find(userId).Result;
            return _repository
                .GetAll()
                .Where(x => x.User == user)
                .Select(x => new Booking
                {
                    Id = x.Id,
                    Date = x.Date,
                    DateCreated = x.DateCreated,
                    DateUpdated = x.DateUpdated,
                    DayNumber = x.DayNumber,
                    Status = x.Status,
                    Type = x.Type,
                    User = null,
                    WorkPlace = new WorkPlace { Id = x.WorkPlace.Id, FloorNumber = x.WorkPlace.FloorNumber, PlaceNumber = x.WorkPlace.PlaceNumber },
                });
        }

        public async Task<Booking> Find(long bookingId)
            => await _repository.Find(bookingId);

        public async Task<Booking> Find(long placeId, DateTime date, long userId)
        {
            var place = await _placeRepository.Find(placeId);
            var user = await _userRepository.Find(userId);
            var booking = place.Bookings.FirstOrDefault(x => x.User == user && x.Date == date);

            booking.WorkPlace.Bookings = new List<Booking>();
            user.Bookings = new List<Booking>();

            return booking;
        }

        public async Task Remove(long bookingId)
        {
            _repository.Remove(bookingId);
            await _repository.Commit();
        }

        public async Task<Booking> Update(Booking booking)
        {
            var result = _repository.Update(booking);
            await _repository.Commit();
            return result;
        }

        public async Task<Booking> Update(long id, BookingStatus status)
        {
            var result = await _repository.Find(id);
            result.Status = status;

            result = _repository.Update(result);
            await _repository.Commit();
            return result;
        }
    }
}
