using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;
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

        public async Task<IList<Booking>> GetAllBookings(DateTime bookingDate)
        {
            return await _context.Bookings
                .Where(x => x.Date == bookingDate)
                .ToListAsync();
        }

        public async Task<IList<Booking>> GetAllPendingBookings()
        {
            return await _context.Bookings
                .Where(x => x.Status == BookingStatus.Pending)
                .ToListAsync();
        }

        public async Task<IList<Booking>> GetBookingsByUser(Guid userId)
        {
            return await _context.Bookings
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .ToListAsync();
        }

        public async Task<Booking> FindById(Guid id)
        {
            var result = await _context.Bookings
                .Include(x => x.User)
                .Include(x => x.WorkPlace)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public void RemoveBooking(Guid id)
        {
            _context.Remove(id);
        }

        public async Task AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
