using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceByPlaceNumber
{
    public class FindWorkPlaceByPlaceNumberQuery : IRequest<WorkPlacesQueryResponse>
    {
        public FindWorkPlaceByPlaceNumberQuery(int place, int floor)
        {
            PlaceNumber = place;
            FloorNumber = floor;
        }

        public int PlaceNumber { get; set; }

        public int FloorNumber { get; set; }
    }
}
