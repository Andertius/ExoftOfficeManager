using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.AddBooking
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand>
    {
        private readonly IBookingRepository _repository;
        private readonly IWorkPlaceRepository _placeRepository;
        private readonly IUserRepository _userRepository;

        public AddBookingCommandHandler(
            IBookingRepository bookingRepository,
            IWorkPlaceRepository placeRepository,
            IUserRepository userRepository)
        {
            _repository = bookingRepository;
            _placeRepository = placeRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            if (await _placeRepository.TryFindAvailableWorkPlace(request.PlaceId, request.BookingDate) is (true, var place))
            {
                if (place.Bookings.Where(x => x.Date == request.BookingDate && x.Type == request.BookingType).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{request.BookingType}', because the work place already has that status.");
                }

                for (int i = 0; i < request.DayNumber; i++)
                {
                    var booking = new Booking
                    {
                        Date = request.BookingType == BookingType.BookedPermanently ? null
                            : new DateTime(request.BookingDate.Year, request.BookingDate.Month, request.BookingDate.Day + i),

                        Type = request.BookingType,
                        Status = request.DayNumber > 1 ? BookingStatus.Pending : BookingStatus.Approved,
                        UserId = request.UserId,
                        WorkPlaceId = request.PlaceId,
                        DayNumber = request.DayNumber,
                    };

                    await _repository.AddBooking(booking);
                }

                await _repository.Commit();
                return Unit.Value;
            }

            throw new ArgumentException($"The work place with id = '{request.PlaceId}' is already fully booked");
        }
    }
}
