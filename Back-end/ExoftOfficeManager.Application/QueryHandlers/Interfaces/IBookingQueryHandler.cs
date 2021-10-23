using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers.Interfaces
{
    public interface IBookingQueryHandler
    {
        IEnumerable<Booking> GetAllQuery(DateTime date);

        IEnumerable<Booking> GetAllPendingBookingsQuery();

        IEnumerable<Booking> GetAllUserBookedQuery(long userId);

        Task<Booking> FindQuery(long bookingId);

        Task<Booking> FindQuery(long placeId, DateTime date, long devId);
    }
}
