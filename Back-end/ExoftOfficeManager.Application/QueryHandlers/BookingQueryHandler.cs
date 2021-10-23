using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class BookingQueryHandler : IBookingQueryHandler
    {
        private readonly IRepository<Booking> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<WorkPlace> _placeRepository;

        public BookingQueryHandler(IRepository<Booking> repo, IRepository<User> userRepository, IRepository<WorkPlace> placeRepository)
        {
            _repository = repo;
            _userRepository = userRepository;
            _placeRepository = placeRepository;
        }

        public IEnumerable<Booking> GetAllQuery(DateTime date)
            => _repository.GetAll().Where(x => x.Date == date);

        public IEnumerable<Booking> GetAllPendingBookingsQuery()
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

        public IEnumerable<Booking> GetAllUserBookedQuery(long userId)
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

        public async Task<Booking> FindQuery(long bookingId)
            => await _repository.Find(bookingId);

        public async Task<Booking> FindQuery(long placeId, DateTime date, long userId)
        {
            var place = await _placeRepository.Find(placeId);
            var user = await _userRepository.Find(userId);
            var booking = place.Bookings.FirstOrDefault(x => x.User == user && x.Date == date);

            booking.WorkPlace.Bookings = new List<Booking>();
            user.Bookings = new List<Booking>();

            return booking;
        }
    }
}
