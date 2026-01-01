using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ShopFlowDbContext : IdentityDbContext<ApplicationUser>
{
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options)
        : base(options) { }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopFlowDbContext>(options =>
    options.UseInMemoryDatabase("ShopFlow"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Strict password policy
    options.Password.RequiredLength = 14;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 6;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings
    options.User.RequireUniqueEmail = true;
    
    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ShopFlowDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();
app.Run();