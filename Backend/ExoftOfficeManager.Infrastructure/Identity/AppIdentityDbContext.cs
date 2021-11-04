using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> opts)
               : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppIdentityUser>()
                .HasOne(x => x.ApiUser)
                .WithOne(x => x.IdentityUser as AppIdentityUser);
        }
    }
}
