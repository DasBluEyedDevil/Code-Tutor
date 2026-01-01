using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopFlowDbContext>(options =>
    options.UseInMemoryDatabase("ShopFlow"));

builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// TODO: Implement DELETE /api/account/external-logins/{provider}
// 
// Requirements:
// 1. Get current user's ID from claims ("user_id")
// 2. Find the ExternalLogin record for this user and provider
// 3. Check if user has at least one other login method:
//    - Another ExternalLogin, OR
//    - A password set (PasswordHash is not null)
// 4. If safe to remove, delete and save
// 5. Return appropriate responses for each case

app.Run();

// Entity Models (provided)
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string? PasswordHash { get; set; }
    public string DisplayName { get; set; } = default!;
    public ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();
}

public class ExternalLogin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Provider { get; set; } = default!;
    public string ProviderKey { get; set; } = default!;
    public User User { get; set; } = default!;
}

public class ShopFlowDbContext : DbContext
{
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<ExternalLogin> ExternalLogins => Set<ExternalLogin>();
}