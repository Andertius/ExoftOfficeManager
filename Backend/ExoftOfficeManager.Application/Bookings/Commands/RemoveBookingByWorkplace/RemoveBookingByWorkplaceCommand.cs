using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingByWorkplace
{
    public class RemoveBookingByWorkplaceCommand : IRequest
    {
        public RemoveBookingByWorkplaceCommand(
            Guid placeId,
            DateTime date,
            Guid userId)
        {
            PlaceId = placeId;
            Date = date;
            UserId = userId;
        }

        public Guid PlaceId { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}
