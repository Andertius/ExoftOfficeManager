using System;
using System.Collections.Generic;
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
    public class WorkPlaceRepository : IWorkPlaceRepository
    {
        private readonly AppDbContext _context;

        public WorkPlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<WorkPlaceDto>> GetAllWorkPlaces()
        {
            return await _context.WorkPlaces
                .Select(x => WorkPlaceMapper.MapIntoDto(x))
                .ToListAsync();
        }

        public async Task<IList<WorkPlaceDto>> GetAllBookedWorkPlaces(DateTime bookingDate)
        {
            var workPlaces = await _context.WorkPlaces
                .Include(x => x.Bookings.Where(x => x.Date == bookingDate))
                .Where(x => (x.Bookings.Count == 1 &&
                    (x.Bookings.First().Type == BookingType.Booked ||
                    x.Bookings.First().Type == BookingType.BookedPermanently)) ||
                    x.Bookings.Count == 2)
                .ToListAsync();

            return workPlaces.Where(x => x.Bookings.Any())
                .Select(x => WorkPlaceMapper.MapIntoDto(x))
                .ToList();
        }

        public async Task<IList<WorkPlaceDto>> GetAllAvailableWorkPlaces(DateTime bookingDate)
        {
            return await _context.WorkPlaces
                   .Include(x => x.Bookings.Where(x => x.Date == bookingDate))
                   .Where(x => !((x.Bookings.Count == 1 &&
                       (x.Bookings.First().Type == BookingType.Booked ||
                       x.Bookings.First().Type == BookingType.BookedPermanently)) ||
                       x.Bookings.Count == 2))
                   .Select(x => WorkPlaceMapper.MapIntoDto(x))
                   .ToListAsync();
        }

        public async Task<WorkPlaceDto> FindWorkPlaceById(Guid placeId)
        {
            var place = await _context.WorkPlaces
                .Include(x => x.Bookings)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == placeId);

            return WorkPlaceMapper.MapIntoDto(place);
        }

        //if (place.Bookings.Any(x => x.Type == BookingType.BookedPermanently))
        //{
        //    return true;
        //}

        //var bookings = place.Bookings.Where(x => x.Date == date);

        //if (!bookings.Any())
        //{
        //    return false;
        //}
        //else if (bookings.Any(x => x.Type == BookingType.Booked))
        //{
        //    return true;
        //}
        //else if (bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked))
        //{
        //    return true;
        //}

        //return false;

        //TODO figure out why TryFindAvailableWorkPlace doesn't work
        public async Task<(bool, WorkPlace)> TryFindAvailableWorkPlace(Guid placeId, DateTime bookingDate)
        {
            var place = _context.WorkPlaces
                .Include(x => x.Bookings.Where(x => !x.Date.HasValue || x.Date == bookingDate))
                .ThenInclude(x => x.User)
                .Where(work =>
                    !work.Bookings.Any() ||
                    !work.Bookings.Any(x => x.Type == BookingType.BookedPermanently) &&
                    !work.Bookings.Any(x => x.Type == BookingType.Booked) &&
                    !work.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked));
                //.FirstOrDefaultAsync(x => x.Id == placeId);

            var a = _context.WorkPlaces
                .Include(x => x.Bookings.Where(x => !x.Date.HasValue || x.Date == bookingDate))
                .ThenInclude(x => x.User)
                .ToList()
                .Where(work =>
                    !work.Bookings.Any() ||
                    !work.Bookings.Any(x => x.Type == BookingType.BookedPermanently) &&
                    !work.Bookings.Any(x => x.Type == BookingType.Booked) &&
                    !work.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked));

            //return place is null ? (false, null) : (true, place);
            throw new Exception();
        }

        public void UpdateWorkPlace(WorkPlaceDto place)
        {
            _context.WorkPlaces.Update(WorkPlaceMapper.MapFromDto(place));
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
