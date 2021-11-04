using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IBookingRepository
    {
        Task<IList<Booking>> GetAllBookings(DateTime bookingDate);

        Task<IList<Booking>> GetAllPendingBookings();

        Task<IList<Booking>> GetBookingsByUser(Guid userId);

        Task<Booking> FindById(Guid id);

        Task AddBooking(Booking booking);

        void RemoveBooking(Guid id);

        void UpdateBooking(Booking booking);

        Task Commit();
    }
}
