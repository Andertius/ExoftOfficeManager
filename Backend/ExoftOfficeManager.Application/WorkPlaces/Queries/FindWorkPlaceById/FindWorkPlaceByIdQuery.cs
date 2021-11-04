using System;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceById
{
    public class FindWorkPlaceByIdQuery : IRequest<WorkPlacesQueryResponse>
    {
        public FindWorkPlaceByIdQuery(Guid workPlaceId)
        {
            WorkPlaceId = workPlaceId;
        }

        public Guid WorkPlaceId { get; set; }
    }
}
