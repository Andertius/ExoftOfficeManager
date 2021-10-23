using System.Linq;
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

        public override IQueryable<Meeting> GetAll()
            => _context.Meetings.Include(x => x.Owner);

        public override async Task<Meeting> Find(long id)
            => await _context.Meetings
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
