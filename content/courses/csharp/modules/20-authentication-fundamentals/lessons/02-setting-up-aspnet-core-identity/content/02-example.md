---
type: "EXAMPLE"
title: "Adding Identity to ShopFlow"
---

This example shows the complete setup of ASP.NET Core Identity in the ShopFlow e-commerce application, including the custom ApplicationUser, DbContext configuration, and service registration.

```csharp
// ===== STEP 1: INSTALL REQUIRED PACKAGES =====
// Run these commands in your terminal:
// dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Tools

// ===== STEP 2: CREATE CUSTOM APPLICATION USER =====
// ShopFlow.Domain/Entities/ApplicationUser.cs

using Microsoft.AspNetCore.Identity;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Extended user class for ShopFlow with additional properties.
/// Inherits all standard Identity properties (Email, PasswordHash, etc.)
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Custom properties for ShopFlow
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<Order> Orders { get; set; } = [];
    public ShoppingCart? ShoppingCart { get; set; }
    public CustomerProfile? Profile { get; set; }
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
}

// ===== STEP 3: CONFIGURE DB CONTEXT =====
// ShopFlow.Infrastructure/Data/ShopFlowDbContext.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Infrastructure.Data;

public class ShopFlowDbContext : IdentityDbContext<ApplicationUser>
{
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options)
        : base(options)
    {
    }
    
    // ShopFlow domain entities
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // IMPORTANT: Call base to configure Identity tables
        base.OnModelCreating(builder);
        
        // Customize Identity table names (optional but cleaner)
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();
        });
        
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable("Roles");
        });
        
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
        
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        
        // Configure relationships
        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

// ===== STEP 4: CONFIGURE IDENTITY IN PROGRAM.CS =====
// ShopFlow.Api/Program.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopFlow.Domain.Entities;
using ShopFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<ShopFlowDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ShopFlow.Infrastructure")
    ));

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password requirements
    options.Password.RequiredLength = 12;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 4;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    
    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ShopFlowDbContext>()
.AddDefaultTokenProviders()  // For password reset, email confirmation tokens
.AddSignInManager<SignInManager<ApplicationUser>>();

// Configure cookie authentication (for web apps)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
    options.LoginPath = "/auth/login";
    options.LogoutPath = "/auth/logout";
    options.AccessDeniedPath = "/auth/access-denied";
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

// ===== STEP 5: CREATE AND APPLY MIGRATIONS =====
// Run these commands:
// dotnet ef migrations add InitialIdentity -p ShopFlow.Infrastructure -s ShopFlow.Api
// dotnet ef database update -p ShopFlow.Infrastructure -s ShopFlow.Api
```
