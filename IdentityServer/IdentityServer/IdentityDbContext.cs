using ExoftOfficeManager.IdentityServer;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}
