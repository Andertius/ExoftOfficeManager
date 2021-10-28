using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces
{
    public class GetWorkPlacesQueryHandler : IRequestHandler<GetWorkPlacesQuery, WorkPlacesQueryResponse[]>
    {
        private readonly IWorkPlaceRepository _repository;

        public GetWorkPlacesQueryHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<WorkPlacesQueryResponse[]> Handle(GetWorkPlacesQuery request, CancellationToken cancellationToken)
        {
            var aa = await _repository.GetAllWorkPlaces();

            return aa.Select(x => new WorkPlacesQueryResponse(x)).ToArray();
        }
    }
}
