using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBooking
{
    public class RemoveBookingCommandHandler : IRequestHandler<RemoveBookingCommand>
    {
        private readonly IBookingRepository _repository;

        public RemoveBookingCommandHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public Task<Unit> Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            _repository.Commit();

            return Unit.Task;
        }
    }
}
