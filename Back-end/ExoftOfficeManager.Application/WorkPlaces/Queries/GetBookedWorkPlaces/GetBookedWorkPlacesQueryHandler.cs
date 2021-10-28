using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetBookedWorkPlaces
{
    public class GetBookedWorkPlacesQueryHandler : IRequestHandler<GetBookedWorkPlacesQuery, WorkPlacesQueryResponse[]>
    {
        private readonly IWorkPlaceRepository _repository;

        public GetBookedWorkPlacesQueryHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<WorkPlacesQueryResponse[]> Handle(GetBookedWorkPlacesQuery request, CancellationToken cancellationToken)
        {
            var bookedWorkPlaceDtos = await _repository.GetAllBookedWorkPlaces(request.BookingDate);
            return bookedWorkPlaceDtos.Select(x => new WorkPlacesQueryResponse(x)).ToArray();
        }
    }
}
