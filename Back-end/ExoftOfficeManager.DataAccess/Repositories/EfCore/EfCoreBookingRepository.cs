using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.DataAccess.Repositories.EfCore
{
    public class EfCoreBookingRepository : EfCoreRepository<Booking, AppDbContext>
    {
        public EfCoreBookingRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all bookings from the database with possible inclusion of certain properties.
        /// </summary>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        /// <returns>An <see cref="IQueryable&lt;&gt;"/> object of all Bookings.</returns>
        public override IQueryable<Booking> GetAll(IEnumerable<string> include)
        {
            IQueryable<Booking> query = _context.Bookings.AsQueryable();

            if (include.Contains(nameof(Booking.WorkPlace)))
            {
                query = query.Include(x => x.WorkPlace);
            }

            if (include.Contains(nameof(Booking.User)))
            {
                query = query.Include(x => x.User);
            }

            return query.Select(x => x);
        }

        /// <summary>
        /// Finds a booking with the given <paramref name="id"/> with possible inclusion of certain properties.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        public override async Task<Booking> Find(long id, IEnumerable<string> include)
        {
            IQueryable<Booking> query = _context.Bookings.AsQueryable();

            if (include.Contains(nameof(Booking.WorkPlace)))
            {
                query = query.Include(x => x.WorkPlace);
            }

            if (include.Contains(nameof(Booking.User)))
            {
                query = query.Include(x => x.User);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
