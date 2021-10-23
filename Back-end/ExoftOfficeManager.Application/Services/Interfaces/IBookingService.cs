using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAll(DateTime date);

        IEnumerable<Booking> GetAllPendingBookings();

        IEnumerable<Booking> GetAllUserBooked(long userId);

        Task<Booking> Find(long bookingId);

        Task<Booking> Find(long placeId, DateTime date, long devId);

        Task Remove(long bookingId);

        Task<Booking> Update(Booking booking);

        Task<Booking> Update(long id, BookingStatus status);
    }
}
