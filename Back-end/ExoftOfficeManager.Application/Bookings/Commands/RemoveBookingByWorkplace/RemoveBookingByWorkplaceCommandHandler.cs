using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingByWorkplace
{
    public class RemoveBookingByWorkplaceCommandHandler : IRequestHandler<RemoveBookingByWorkplaceCommand>
    {
        private readonly IBookingRepository _repository;
        private readonly IWorkPlaceRepository _placeRepository;

        public RemoveBookingByWorkplaceCommandHandler(IBookingRepository repo, IWorkPlaceRepository placeRepo)
        {
            _repository = repo;
            _placeRepository = placeRepo;
        }

        public async Task<Unit> Handle(RemoveBookingByWorkplaceCommand request, CancellationToken cancellationToken)
        {
            var placeDto = await _placeRepository.FindWorkPlaceById(request.PlaceId);
            var booking = placeDto.Bookings.FirstOrDefault(x => x.Date == request.Date && x.User.Id == request.UserId);

            _repository.RemoveBooking(booking.Id);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
