using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingById
{
    public class RemoveBookingByIdCommandHandler : IRequestHandler<RemoveBookingByIdCommand>
    {
        private readonly IBookingRepository _repository;

        public RemoveBookingByIdCommandHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(RemoveBookingByIdCommand request, CancellationToken cancellationToken)
        {
            _repository.RemoveBooking(request.BookingId);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
