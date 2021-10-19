using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.DataAccess.Repositories.EfCore
{
    public class EfCoreUserRepository : EfCoreRepository<User, AppDbContext>
    {
        public EfCoreUserRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all users from the database with possible inclusion of certain properties.
        /// </summary>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        /// <returns>An <see cref="IQueryable&lt;&gt;"/> object of all Users.</returns>
        public override IQueryable<User> GetAll(IEnumerable<string> include)
        {
            IQueryable<User> query = _context.Users.AsQueryable();

            if (include.Contains(nameof(User.NotRequiredUserMeetings)))
            {
                query = query.Include(x => x.NotRequiredUserMeetings);
            }

            if (include.Contains(nameof(User.RequiredUserMeetings)))
            {
                query = query.Include(x => x.RequiredUserMeetings);
            }

            if (include.Contains(nameof(User.OwnerMeetings)))
            {
                query = query.Include(x => x.OwnerMeetings);
            }

            return query.Select(x => x);
        }

        /// <summary>
        /// Finds a user with the given <paramref name="id"/> with possible inclusion of certain properties.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="include">An enumeration of all properties the method should include.</param>
        public override async Task<User> Find(long id, IEnumerable<string> include)
        {
            IQueryable<User> query = _context.Users.AsQueryable();

            if (include.Contains(nameof(User.NotRequiredUserMeetings)))
            {
                query = query.Include(x => x.NotRequiredUserMeetings);
            }

            if (include.Contains(nameof(User.RequiredUserMeetings)))
            {
                query = query.Include(x => x.RequiredUserMeetings);
            }

            if (include.Contains(nameof(User.OwnerMeetings)))
            {
                query = query.Include(x => x.OwnerMeetings);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
