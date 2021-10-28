using System;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours
{
    public class GetAvailableHoursQuery : IRequest<GetAvailableHoursQueryResponse[]>
    {
        public GetAvailableHoursQuery(DateTime date, int room)
        {
            Date = date;
            RoomNumber = room;
        }

        public DateTime Date { get; set; }

        public int RoomNumber { get; set; }
    }
}
