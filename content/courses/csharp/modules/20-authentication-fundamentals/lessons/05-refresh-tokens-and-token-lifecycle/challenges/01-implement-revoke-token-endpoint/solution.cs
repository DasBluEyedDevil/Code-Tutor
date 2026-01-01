using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

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
    public ShopFlowDbContext(DbContextOptions<ShopFlowDbContext> options) : base(options) { }
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}

public record RevokeTokenCommand(
    string Token,
    string? Reason,
    string CurrentUserId
) : IRequest<RevokeTokenResult>;

public record RevokeTokenResult(
    bool Succeeded,
    string Message
);

public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, RevokeTokenResult>
{
    private readonly ShopFlowDbContext _context;
    private readonly ILogger<RevokeTokenHandler> _logger;
    
    public RevokeTokenHandler(ShopFlowDbContext context, ILogger<RevokeTokenHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<RevokeTokenResult> Handle(
        RevokeTokenCommand request,
        CancellationToken cancellationToken)
    {
        var tokenHash = HashToken(request.Token);
        
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);
        
        if (token is null)
        {
            return new RevokeTokenResult(false, "Token not found");
        }
        
        // Security: Users can only revoke their own tokens
        if (token.UserId != request.CurrentUserId)
        {
            _logger.LogWarning(
                "User {UserId} attempted to revoke token belonging to {TokenOwner}",
                request.CurrentUserId,
                token.UserId);
            
            return new RevokeTokenResult(false, "Token not found");
        }
        
        if (token.IsRevoked)
        {
            return new RevokeTokenResult(true, "Token was already revoked");
        }
        
        // Revoke entire token family for security
        var familyTokens = await _context.RefreshTokens
            .Where(t => t.FamilyId == token.FamilyId && t.RevokedAt == null)
            .ToListAsync(cancellationToken);
        
        var reason = request.Reason ?? "User requested revocation";
        
        foreach (var familyToken in familyTokens)
        {
            familyToken.RevokedAt = DateTime.UtcNow;
            familyToken.RevokedReason = reason;
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            "Revoked {Count} tokens in family {FamilyId} for user {UserId}: {Reason}",
            familyTokens.Count,
            token.FamilyId,
            request.CurrentUserId,
            reason);
        
        return new RevokeTokenResult(true, $"Revoked {familyTokens.Count} tokens");
    }
    
    private static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShopFlowDbContext>(o => o.UseInMemoryDatabase("Test"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.MapPost("/api/auth/revoke", async (
    RevokeTokenRequest request,
    HttpContext httpContext,
    ISender sender) =>
{
    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    
    if (string.IsNullOrEmpty(userId))
    {
        return Results.Unauthorized();
    }
    
    var command = new RevokeTokenCommand(
        Token: request.Token,
        Reason: request.Reason,
        CurrentUserId: userId
    );
    
    var result = await sender.Send(command);
    
    if (!result.Succeeded)
    {
        return Results.BadRequest(new { result.Message });
    }
    
    return Results.Ok(new { result.Message });
})
.RequireAuthorization()
.WithName("RevokeToken")
.WithTags("Authentication");

app.Run();

public record RevokeTokenRequest(string Token, string? Reason = null);