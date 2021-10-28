using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBooking
{
    public class RemoveBookingCommandHandler : IRequestHandler<RemoveBookingCommand>
    {
        private readonly IBookingRepository _repository;
        private readonly IWorkPlaceRepository _placeRepository;

        public RemoveBookingCommandHandler(IBookingRepository repo, IWorkPlaceRepository placeRepo)
        {
            _repository = repo;
            _placeRepository = placeRepo;
        }

        //TODO ask why User is not getting included
        public async Task<Unit> Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty)
            {
                var placeDto = await _placeRepository.FindWorkPlaceById(request.PlaceId);
                var booking = placeDto.Bookings.FirstOrDefault(x => x.Date == request.Date && x.User.Id == request.UserId);
                request.BookingId = booking.Id;
            }

            _repository.RemoveBooking(request.BookingId);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
