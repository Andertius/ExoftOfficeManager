using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces
{
    public class GetWorkPlacesQuery : IRequest<WorkPlacesQueryResponse[]>
    {
    }
}
