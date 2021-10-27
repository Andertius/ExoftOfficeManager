using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _repository;

        public UpdateBookingCommandHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            _repository.Update(request.Booking);
            _repository.Commit();

            return Unit.Task;
        }
    }
}
