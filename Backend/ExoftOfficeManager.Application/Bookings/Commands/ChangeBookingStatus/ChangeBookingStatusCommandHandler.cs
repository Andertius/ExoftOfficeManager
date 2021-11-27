using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.ChangeBookingStatus
{
    public class ChangeBookingStatusCommandHandler : IRequestHandler<ChangeBookingStatusCommand>
    {
        private readonly IBookingRepository _repository;

        public ChangeBookingStatusCommandHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repository.FindById(request.BookingId);
            booking.Status = request.BookingStatus;

            _repository.UpdateBooking(booking);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
