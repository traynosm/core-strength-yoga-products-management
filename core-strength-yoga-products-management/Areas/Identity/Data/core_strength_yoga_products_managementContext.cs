using core_strength_yoga_products_management.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_management.Data;

public class core_strength_yoga_products_managementContext : IdentityDbContext<core_strength_yoga_products_managementUser>
{
    public core_strength_yoga_products_managementContext(DbContextOptions<core_strength_yoga_products_managementContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
