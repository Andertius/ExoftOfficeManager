using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Infrastructure.Repositories.EfCore
{
    public class EfCoreUserRepository : EfCoreRepository<User, AppDbContext>
    {
        public EfCoreUserRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
