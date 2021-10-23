using System;
using System.Linq;

using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Utilities
{
    internal static class IsBookedHelper
    {
        public static bool IsBooked(WorkPlace place, DateTime date)
        {
            var bookings = place.Bookings.Where(x => x.Date == date);

            if (!bookings.Any())
            {
                return false;
            }
            else if (bookings.Count() == 1 &&
                (bookings.First().Type == BookingType.Booked || bookings.First().Type == BookingType.BookedPermanently))
            {
                return true;
            }
            else if (bookings.Count() == 2)
            {
                return true;
            }

            return false;
        }
    }
}
