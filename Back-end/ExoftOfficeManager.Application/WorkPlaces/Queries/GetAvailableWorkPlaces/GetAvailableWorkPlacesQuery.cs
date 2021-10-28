using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
