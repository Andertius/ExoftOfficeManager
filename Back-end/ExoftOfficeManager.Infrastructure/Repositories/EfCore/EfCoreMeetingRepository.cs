using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories.EfCore
{
    public class EfCoreMeetingRepository : EfCoreRepository<Meeting, AppDbContext>
    {
        public EfCoreMeetingRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all meetings from the database with possible inclusion of certain properties.
        /// </summary>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        /// <returns>An <see cref="IQueryable&lt;&gt;"/> object of all Meetings.</returns>
        public override IQueryable<Meeting> GetAll(IEnumerable<string> include)
        {
            IQueryable<Meeting> query = _context.Meetings.AsQueryable();

            if (include.Contains(nameof(Meeting.Owner)))
            {
                query = query.Include(x => x.Owner);
            }

            if (include.Contains(nameof(Meeting.RequiredUserMeetings)))
            {
                query = query.Include(x => x.RequiredUserMeetings);
            }

            if (include.Contains(nameof(Meeting.NotRequiredUserMeetings)))
            {
                query = query.Include(x => x.NotRequiredUserMeetings);
            }

            return query.Select(x => x);
        }

        /// <summary>
        /// Finds a meeting with the given <paramref name="id"/> with possible inclusion of certain properties.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        public override async Task<Meeting> Find(long id, IEnumerable<string> include)
        {
            IQueryable<Meeting> query = _context.Meetings.AsQueryable();

            if (include.Contains(nameof(Meeting.Owner)))
            {
                query = query.Include(x => x.Owner);
            }

            if (include.Contains(nameof(Meeting.NotRequiredUserMeetings)))
            {
                query = query.Include(x => x.NotRequiredUserMeetings);
            }

            if (include.Contains(nameof(Meeting.RequiredUserMeetings)))
            {
                query = query.Include(x => x.RequiredUserMeetings);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
