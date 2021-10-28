using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Application.Utilities;
using ExoftOfficeManager.Domain.Dtos;
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
            var place = await _placeRepository.FindWorkPlaceById(request.PlaceId);

            if (BookingHelper.IsBooked(place, request.BookingDate))
            {
                throw new ArgumentException($"The work place with id = {request.PlaceId} is already fully booked");
            }
            else
            {
                if (place.Bookings.Where(x => x.Date == request.BookingDate && x.Type == request.BookingType).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{request.BookingType}', because the work place already has that status.");
                }

                for (int i = 0; i < request.DayNumber; i++)
                {
                    var booking = new BookingDto
                    {
                        Date = new DateTime(request.BookingDate.Year, request.BookingDate.Month, request.BookingDate.Day + i),
                        Type = request.BookingType,
                        Status = request.DayNumber > 1 ? BookingStatus.Pending : BookingStatus.Approved,
                        User = await _userRepository.FindUserById(request.UserId),
                        WorkPlace = await _placeRepository.FindWorkPlaceById(request.PlaceId),
                        DayNumber = request.DayNumber,
                    };

                    await _repository.AddBooking(booking);
                }

                await _repository.Commit();
                return Unit.Value;
            }
        }
    }
}
