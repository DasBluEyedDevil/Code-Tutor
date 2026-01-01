using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// TODO: Create ApplicationUser class extending IdentityUser
// - Add FirstName (string)
// - Add LastName (string)
// - Add CreatedAt (DateTime, default DateTime.UtcNow)

public class ApplicationUser : IdentityUser
{
    // Add properties here
}

// TODO: Create ShopFlowDbContext inheriting from IdentityDbContext<ApplicationUser>

public class ShopFlowDbContext : IdentityDbContext<ApplicationUser>
{
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options)
        : base(options) { }
}

// Program.cs configuration
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopFlowDbContext>(options =>
    options.UseInMemoryDatabase("ShopFlow"));

// TODO: Configure Identity with strict password policy:
// Password:
//   - RequiredLength = 14
//   - RequireDigit = true
//   - RequireLowercase = true
//   - RequireUppercase = true
//   - RequireNonAlphanumeric = true
//   - RequiredUniqueChars = 6
// Lockout:
//   - DefaultLockoutTimeSpan = 30 minutes
//   - MaxFailedAccessAttempts = 3
//   - AllowedForNewUsers = true
// User:
//   - RequireUniqueEmail = true
// SignIn:
//   - RequireConfirmedEmail = true

var app = builder.Build();
app.Run();