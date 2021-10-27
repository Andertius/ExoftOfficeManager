using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingDto[]> GetAllBookings(DateTime bookingDate);

        Task<BookingDto[]> GetAllPendingBookings();

        Task<BookingDto[]> GetBookingsByUser(Guid userId);

        Task<BookingDto> FindById(Guid id);

        void Remove(Guid id);

        void Update(BookingDto bookingDto);

        Task Commit();
    }
}
