using System;
using System.Linq;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.Utilities
{
    internal static class BookingHelper
    {
        public static bool IsBooked(WorkPlaceDto place, DateTime date)
        {
            if (place.Bookings.Any(x => x.Type == BookingType.BookedPermanently))
            {
                return true;
            }

            var bookings = place.Bookings.Where(x => x.Date == date);

            if (!bookings.Any())
            {
                return false;
            }
            else if (bookings.Any(x => x.Type == BookingType.Booked))
            {
                return true;
            }
            else if (bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked))
            {
                return true;
            }

            return false;
        }
    }
}
