using System;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<BookingDto[]> GetAllBookings(DateTime bookingDate)
        {
            return await _context.Bookings
                .Where(x => x.Date == bookingDate)
                .Select(x => BookingMapper.MapIntoDto(x))
                .ToArrayAsync();
        }

        public async Task<BookingDto[]> GetAllPendingBookings()
        {
            return await _context.Bookings
                .Where(x => x.Status == BookingStatus.Pending)
                .Select(x => BookingMapper.MapIntoDto(x))
                .ToArrayAsync();
        }

        public async Task<BookingDto[]> GetBookingsByUser(Guid userId)
        {
            return await _context.Bookings
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .Select(x => BookingMapper.MapIntoDto(x))
                .ToArrayAsync();
        }

        public async Task<BookingDto> FindById(Guid id)
        {
            var result = await _context.Bookings
                .Include(x => x.User)
                .Include(x => x.WorkPlace)
                .FirstOrDefaultAsync(x => x.Id == id);

            return BookingMapper.MapIntoDto(result);
        }

        public void RemoveBooking(Guid id)
        {
            _context.Remove(id);
        }

        public async Task AddBooking(BookingDto bookingDto)
        {
            await _context.Bookings.AddAsync(BookingMapper.MapFromDto(bookingDto));
        }

        public void UpdateBooking(BookingDto bookingDto)
        {
            var bookingModel = BookingMapper.MapFromDto(bookingDto);
            _context.Bookings.Update(bookingModel);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
