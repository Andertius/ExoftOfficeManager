using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Domain.Exceptions.Booking;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.AddBooking
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IWorkPlaceRepository _placeRepository;

        public AddBookingCommandHandler(
            IBookingRepository bookingRepository,
            IWorkPlaceRepository placeRepository)
        {
            _bookingRepository = bookingRepository;
            _placeRepository = placeRepository;
        }

        public async Task<Unit> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            if (await _placeRepository.TryFindAvailableWorkPlace(request.PlaceId, request.BookingDate) is (true, var place))
            {
                if (place.Bookings.Where(x => x.Date == request.BookingDate && x.Type == request.BookingType).Any())
                {
                    throw new BookingStatusException(request.BookingType);
                }
                else if (place.Bookings.Any() &&
                    ((place.Bookings.First().Type == BookingType.FirstHalfBooked &&
                    request.BookingType != BookingType.SecondHalfBooked) ||
                    (place.Bookings.First().Type == BookingType.SecondHalfBooked &&
                    request.BookingType != BookingType.FirstHalfBooked)))
                {
                    string message = $"Cannot book with status '{request.BookingType}', because the work place is booked for half a day.";
                    throw new BookingStatusException(message);
                }

                for (int i = 0; i < (request.DayNumber.HasValue ? request.DayNumber : 1); i++)
                {
                    var booking = new Booking
                    {
                        Date = request.BookingType == BookingType.BookedPermanently ? null
                            : new DateTime(
                                request.BookingDate.Year,
                                request.BookingDate.Month,
                                request.BookingDate.Day + i),

                        Type = request.BookingType,
                        Status = request.DayNumber > 1 ? BookingStatus.Pending : BookingStatus.Approved,
                        UserId = request.UserId,
                        WorkPlaceId = request.PlaceId,
                        DayNumber = request.DayNumber,
                    };

                    await _bookingRepository.AddBooking(booking);
                }

                await _bookingRepository.Commit();
                return Unit.Value;
            }

            throw new PlaceAlreadyBookedException(request.BookingDate);
        }
    }
}
