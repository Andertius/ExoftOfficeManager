using System;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetBookedWorkPlaces
{
    public class GetBookedWorkPlacesQuery : IRequest<WorkPlacesQueryResponse[]>
    {
        public GetBookedWorkPlacesQuery(DateTime bookingDate)
        {
            BookingDate = bookingDate;
        }

        public DateTime BookingDate { get; set; }
    }
}
