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

app.MapDelete("/api/account/external-logins/{provider}", async (
    string provider,
    ClaimsPrincipal user,
    ShopFlowDbContext db,
    ILogger<Program> logger) =>
{
    var userId = int.Parse(user.FindFirstValue("user_id")
        ?? throw new InvalidOperationException("User ID claim not found"));
    
    // Find the external login to remove
    var loginToRemove = await db.ExternalLogins
        .FirstOrDefaultAsync(el => 
            el.UserId == userId && 
            el.Provider.ToLower() == provider.ToLower());
    
    if (loginToRemove == null)
    {
        return Results.NotFound(new { Error = $"Provider '{provider}' is not linked to your account." });
    }
    
    // Get user with all their login methods
    var dbUser = await db.Users
        .Include(u => u.ExternalLogins)
        .FirstOrDefaultAsync(u => u.Id == userId);
    
    if (dbUser == null)
    {
        return Results.NotFound(new { Error = "User not found." });
    }
    
    // Check if user would have any login method remaining
    var hasPassword = !string.IsNullOrEmpty(dbUser.PasswordHash);
    var otherLoginsCount = dbUser.ExternalLogins.Count(el => el.Id != loginToRemove.Id);
    
    if (!hasPassword && otherLoginsCount == 0)
    {
        return Results.BadRequest(new 
        { 
            Error = "Cannot unlink your only login method. Add a password or link another provider first.",
            HasPassword = hasPassword,
            ExternalLoginCount = dbUser.ExternalLogins.Count
        });
    }
    
    // Safe to remove
    db.ExternalLogins.Remove(loginToRemove);
    await db.SaveChangesAsync();
    
    logger.LogInformation(
        "User {UserId} unlinked {Provider} external login",
        userId, provider);
    
    return Results.Ok(new 
    { 
        Message = $"Successfully unlinked {provider} from your account.",
        RemainingLogins = otherLoginsCount,
        HasPassword = hasPassword
    });
}).RequireAuthorization();

app.Run();

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