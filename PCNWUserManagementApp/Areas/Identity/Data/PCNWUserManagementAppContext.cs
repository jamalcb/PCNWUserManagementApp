using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCNWUserManagementApp.Models;
using System.Data;

namespace PCNWUserManagementApp.Data;

public class PCNWUserManagementAppContext : IdentityDbContext<IdentityUser>
{
    public PCNWUserManagementAppContext(DbContextOptions<PCNWUserManagementAppContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
