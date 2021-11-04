using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries
{
    public class WorkPlacesQueryResponse
    {
        public WorkPlacesQueryResponse(WorkPlaceDto workPlace)
        {
            WorkPlace = workPlace;
        }

        public WorkPlaceDto WorkPlace { get; private set; }
    }
}
