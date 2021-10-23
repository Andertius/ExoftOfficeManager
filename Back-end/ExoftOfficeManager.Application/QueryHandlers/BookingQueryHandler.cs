using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class BookingQueryHandler : IBookingQueryHandler
    {
        public Task<Booking> FindQuery(long bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> FindQuery(long placeId, DateTime date, long devId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAllPendingBookingsQuery()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAllQuery(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAllUserBookedQuery(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
