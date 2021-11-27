using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceById
{
    public class FindWorkPlaceByIdQueryHandler : IRequestHandler<FindWorkPlaceByIdQuery, WorkPlacesQueryResponse>
    {
        private readonly IWorkPlaceRepository _repository;

        public FindWorkPlaceByIdQueryHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<WorkPlacesQueryResponse> Handle(FindWorkPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            var workPlace = await _repository.FindWorkPlaceById(request.WorkPlaceId);
            return new WorkPlacesQueryResponse(WorkPlaceMapper.MapIntoDto(workPlace));
        }
    }
}
