using System;

namespace ExoftOfficeManager.Domain.Exceptions.Booking
{
    public class PlaceAlreadyBookedException : BookingException
    {
        public PlaceAlreadyBookedException(DateTime date)
            : base($"The work place is already fully booked for {date.Day}-{date.Month}-{date.Year}.")
        {
        }
    }
}
