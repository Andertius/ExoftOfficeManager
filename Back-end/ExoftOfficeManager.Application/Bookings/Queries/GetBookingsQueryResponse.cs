using System;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.Bookings.Queries
{
    public class BookingsQueryResponse
    {
        public BookingsQueryResponse(BookingDto booking)
        {
            Date = booking.Date;
            Type = booking.Type;
            Status = booking.Status;
            DayNumber = booking.DayNumber;
            User = booking.User;
            WorkPlace = booking.WorkPlace;
        }

        public DateTime Date { get; private set; }

        public BookingType Type { get; private set; }

        public BookingStatus Status { get; private set; }

        public int? DayNumber { get; private set; }

        public UserDto User { get; private set; }

        public WorkPlaceDto WorkPlace { get; private set; }
    }
}
