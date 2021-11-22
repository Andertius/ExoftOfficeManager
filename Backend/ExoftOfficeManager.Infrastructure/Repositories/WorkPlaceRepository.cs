using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories
{
    //TODO Fix BookingStatus filtering
    public class WorkPlaceRepository : IWorkPlaceRepository
    {
        private readonly AppDbContext _context;

        public WorkPlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<WorkPlace>> GetAllWorkPlaces()
        {
            return await _context.WorkPlaces.ToListAsync();
        }

        public async Task<IList<WorkPlace>> GetAllBookedWorkPlaces(DateTime bookingDate)
        {
            var workPlaces = await _context.WorkPlaces
                .Include(x => x.Bookings.Where(x => x.Date == bookingDate))
                .Where(place => place.Bookings.All(x => x.Status == BookingStatus.Approved) &&
                    (place.Bookings.Any(x => x.Type == BookingType.BookedPermanently) ||
                    place.Bookings.Any(x => x.Type == BookingType.Booked) ||
                    place.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked)))
                .ToListAsync();

            return workPlaces.Where(x => x.Bookings.Any()).ToList();
        }

        public async Task<IList<WorkPlace>> GetAllAvailableWorkPlaces(DateTime bookingDate)
        {
            return await _context.WorkPlaces
                   .Include(x => x.Bookings.Where(x => !x.Date.HasValue || x.Date == bookingDate))
                   .Where(place => !place.Bookings.Any() ||
                    place.Bookings.Any(x => x.Status != BookingStatus.Approved) ||
                    !place.Bookings.Any(x => x.Type == BookingType.BookedPermanently) &&
                    !place.Bookings.Any(x => x.Type == BookingType.Booked) &&
                     place.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked) &&
                     place.Bookings.Count < 2)
                   .ToListAsync();
        }
                    
        public async Task<WorkPlace> FindWorkPlaceById(Guid placeId)
        {
            var place = await _context.WorkPlaces
                .Include(x => x.Bookings)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == placeId);

            return place;
        }

        //TODO figure out why TryFindAvailableWorkPlace doesn't work (temporarily fixed)
        public async Task<(bool, WorkPlace)> TryFindAvailableWorkPlace(Guid placeId, DateTime bookingDate)
        {
            var place = (await _context.WorkPlaces
                .Include(x => x.Bookings.Where(x => !x.Date.HasValue || x.Date == bookingDate))
                .ThenInclude(x => x.User)
                .ToListAsync())
                .Where(work =>
                    !work.Bookings.Any() ||
                    !work.Bookings.Any(x => x.Type == BookingType.BookedPermanently) &&
                    !work.Bookings.Any(x => x.Type == BookingType.Booked) &&
                     work.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked) &&
                     work.Bookings.Count < 2)
                .FirstOrDefault(x => x.Id == placeId);

            //var a = _context.WorkPlaces
            //    .Include(x => x.Bookings.Where(x => !x.Date.HasValue || x.Date == bookingDate))
            //    .ThenInclude(x => x.User)
            //    .Count();
                //.Where(work =>
                //    !work.Bookings.Any() ||
                //    !work.Bookings.Any(x => x.Type == BookingType.BookedPermanently) &&
                //    !work.Bookings.Any(x => x.Type == BookingType.Booked) &&
                //    !work.Bookings.All(x => x.Type == BookingType.FirstHalfBooked || x.Type == BookingType.SecondHalfBooked));

            return place is null ? (false, null) : (true, place);
            //throw new Exception();
        }

        public void UpdateWorkPlace(WorkPlace place)
        {
            _context.WorkPlaces.Update(place);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<WorkPlace> FindWorkPlaceByPlaceNumber(int place, int floor)
        {
            return await _context.WorkPlaces
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.PlaceNumber == place && x.FloorNumber == floor);
        }
    }
}
