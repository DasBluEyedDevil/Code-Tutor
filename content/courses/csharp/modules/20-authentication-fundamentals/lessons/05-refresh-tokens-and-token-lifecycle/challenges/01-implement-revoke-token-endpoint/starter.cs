using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

// Assume these exist
public class RefreshToken
{
    public int Id { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string FamilyId { get; set; } = string.Empty;
    public DateTime? RevokedAt { get; set; }
    public string? RevokedReason { get; set; }
    public bool IsRevoked => RevokedAt is not null;
}

public class ShopFlowDbContext : DbContext
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}

// TODO: Create RevokeTokenCommand
// - Token (string)
// - Reason (string?)
// - CurrentUserId (string) - the user making the request

// TODO: Create RevokeTokenResult
// - Succeeded (bool)
// - Message (string)

// TODO: Create RevokeTokenHandler that:
// 1. Hashes the provided token
// 2. Finds the token in database
// 3. Validates it belongs to CurrentUserId
// 4. Revokes all tokens with the same FamilyId
// 5. Returns appropriate result

// Helper method for hashing
public static string HashToken(string token)
{
    using var sha256 = SHA256.Create();
    var bytes = System.Text.Encoding.UTF8.GetBytes(token);
    var hash = sha256.ComputeHash(bytes);
    return Convert.ToBase64String(hash);
}

// API Endpoint
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShopFlowDbContext>(o => o.UseInMemoryDatabase("Test"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// TODO: Map POST /api/auth/revoke endpoint
// - Require authorization
// - Get current user ID from claims
// - Send RevokeTokenCommand
// - Return appropriate response

app.Run();