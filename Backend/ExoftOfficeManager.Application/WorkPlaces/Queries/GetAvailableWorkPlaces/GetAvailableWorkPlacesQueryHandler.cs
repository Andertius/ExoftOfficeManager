using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetAvailableWorkPlaces
{
    public class GetAvailableWorkPlacesQueryHandler : IRequestHandler<GetAvailableWorkPlacesQuery, WorkPlacesQueryResponse[]>
    {
        private readonly IWorkPlaceRepository _repository;

        public GetAvailableWorkPlacesQueryHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<WorkPlacesQueryResponse[]> Handle(GetAvailableWorkPlacesQuery request, CancellationToken cancellationToken)
        {
            var availableWorkPlaces = await _repository.GetAllAvailableWorkPlaces(request.BookingDate);
            return availableWorkPlaces.Select(x => new WorkPlacesQueryResponse(WorkPlaceMapper.MapIntoDto(x))).ToArray();
        }
    }
}
