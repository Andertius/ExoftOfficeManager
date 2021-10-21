using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories.EfCore
{
    public class EfCoreWorkPlaceRepository : EfCoreRepository<WorkPlace, AppDbContext>
    {
        public EfCoreWorkPlaceRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all work places from the database with possible inclusion of certain properties.
        /// </summary>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        /// <returns>An <see cref="IQueryable&lt;&gt;"/> object of all WorkPlaces.</returns>
        public override IQueryable<WorkPlace> GetAll(IEnumerable<string> include)
        {
            IQueryable<WorkPlace> query = _context.WorkPlaces.AsQueryable();

            if (include.Contains(nameof(WorkPlace.Bookings)))
            {
                query = query.Include(x => x.Bookings);
            }

            return query.Select(x => x);
        }

        /// <summary>
        /// Finds a work place with the given <paramref name="id"/> with possible inclusion of certain properties.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        public override async Task<WorkPlace> Find(long id, IEnumerable<string> include)
        {
            IQueryable<WorkPlace> query = _context.WorkPlaces.AsQueryable();

            if (include.Contains(nameof(WorkPlace.Bookings)))
            {
                query = query.Include(x => x.Bookings);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
