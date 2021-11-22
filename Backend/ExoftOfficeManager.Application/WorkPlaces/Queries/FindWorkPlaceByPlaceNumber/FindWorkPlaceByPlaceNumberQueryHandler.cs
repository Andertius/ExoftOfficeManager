using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceByPlaceNumber
{
    public class FindWorkPlaceByPlaceNumberQueryHandler : IRequestHandler<FindWorkPlaceByPlaceNumberQuery, WorkPlacesQueryResponse>
    {
        private readonly IWorkPlaceRepository _repository;

        public FindWorkPlaceByPlaceNumberQueryHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<WorkPlacesQueryResponse> Handle(FindWorkPlaceByPlaceNumberQuery request, CancellationToken cancellationToken)
        {
            var place = await _repository.FindWorkPlaceByPlaceNumber(request.PlaceNumber, request.FloorNumber);
            return new WorkPlacesQueryResponse(WorkPlaceMapper.MapIntoDto(place));
        }
    }
}
