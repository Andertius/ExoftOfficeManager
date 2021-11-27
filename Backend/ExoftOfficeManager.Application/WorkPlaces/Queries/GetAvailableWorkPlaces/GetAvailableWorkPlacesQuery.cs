using System;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetAvailableWorkPlaces
{
    public class GetAvailableWorkPlacesQuery : IRequest<WorkPlacesQueryResponse[]>
    {
        public GetAvailableWorkPlacesQuery(DateTime bookingDate)
        {
            BookingDate = bookingDate;
        }

        public DateTime BookingDate { get; set; }
    }
}
