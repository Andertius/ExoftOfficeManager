using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingByWorkplace
{
    public class RemoveBookingByWorkplaceCommandHandler : IRequestHandler<RemoveBookingByWorkplaceCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IWorkPlaceRepository _placeRepository;

        public RemoveBookingByWorkplaceCommandHandler(
            IBookingRepository bookingRepo,
            IWorkPlaceRepository placeRepo)
        {
            _bookingRepository = bookingRepo;
            _placeRepository = placeRepo;
        }

        public async Task<Unit> Handle(RemoveBookingByWorkplaceCommand request, CancellationToken cancellationToken)
        {
            var place = await _placeRepository.FindWorkPlaceById(request.PlaceId);
            var booking = place.Bookings.FirstOrDefault(x => x.Date == request.Date && x.User.Id == request.UserId);

            _bookingRepository.RemoveBooking(booking.Id);
            await _bookingRepository.Commit();

            return Unit.Value;
        }
    }
}
