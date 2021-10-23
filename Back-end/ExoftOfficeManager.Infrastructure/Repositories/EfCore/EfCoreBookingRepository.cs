using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories.EfCore
{
    public class EfCoreBookingRepository : EfCoreRepository<Booking, AppDbContext>
    {
        public EfCoreBookingRepository(AppDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Booking> GetAll()
            => _context.Bookings.Include(x => x.User).Include(x => x.WorkPlace);

        public override async Task<Booking> Find(long id)
            => await _context.Bookings
                .Include(x => x.WorkPlace)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
