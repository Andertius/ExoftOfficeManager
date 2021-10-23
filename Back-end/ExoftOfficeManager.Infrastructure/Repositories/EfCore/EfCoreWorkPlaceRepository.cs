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

        public override IQueryable<WorkPlace> GetAll()
            => _context.WorkPlaces.Include(x => x.Bookings);

        public override async Task<WorkPlace> Find(long id)
            => await _context.WorkPlaces.Include(x => x.Bookings).FirstOrDefaultAsync(x => x.Id == id);
    }
}
