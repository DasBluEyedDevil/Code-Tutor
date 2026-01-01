---
type: "EXAMPLE"
title: "Implementing Refresh Tokens"
---

This example shows a complete refresh token implementation with secure storage, rotation, and reuse detection.

```csharp
// ===== REFRESH TOKEN ENTITY =====
// ShopFlow.Domain/Entities/RefreshToken.cs

namespace ShopFlow.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    
    /// <summary>
    /// The actual token value (hashed for storage).
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;
    
    /// <summary>
    /// User who owns this token.
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    
    /// <summary>
    /// When the token was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// When the token expires.
    /// </summary>
    public DateTime ExpiresAt { get; set; }
    
    /// <summary>
    /// When the token was revoked (null if still active).
    /// </summary>
    public DateTime? RevokedAt { get; set; }
    
    /// <summary>
    /// IP address that created this token.
    /// </summary>
    public string? CreatedByIp { get; set; }
    
    /// <summary>
    /// The token that replaced this one (for rotation tracking).
    /// </summary>
    public string? ReplacedByTokenHash { get; set; }
    
    /// <summary>
    /// Reason for revocation if revoked.
    /// </summary>
    public string? RevokedReason { get; set; }
    
    /// <summary>
    /// Token family ID for detecting reuse attacks.
    /// All rotated tokens share the same family.
    /// </summary>
    public string FamilyId { get; set; } = Guid.NewGuid().ToString();
    
    public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt is not null;
}

// ===== REFRESH TOKEN SERVICE =====
// ShopFlow.Infrastructure/Services/RefreshTokenService.cs

using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Common.Settings;
using ShopFlow.Domain.Entities;
using ShopFlow.Infrastructure.Data;

namespace ShopFlow.Infrastructure.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly ShopFlowDbContext _context;
    private readonly JwtSettings _settings;
    private readonly ILogger<RefreshTokenService> _logger;
    
    public RefreshTokenService(
        ShopFlowDbContext context,
        IOptions<JwtSettings> settings,
        ILogger<RefreshTokenService> logger)
    {
        _context = context;
        _settings = settings.Value;
        _logger = logger;
    }
    
    /// <summary>
    /// Creates a new refresh token for the user.
    /// </summary>
    public async Task<(string Token, RefreshToken Entity)> CreateRefreshTokenAsync(
        string userId,
        string? ipAddress = null,
        string? existingFamilyId = null)
    {
        // Generate cryptographically random token
        var tokenBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(tokenBytes);
        var token = Convert.ToBase64String(tokenBytes);
        
        // Hash the token for storage (don't store plaintext!)
        var tokenHash = HashToken(token);
        
        var refreshToken = new RefreshToken
        {
            TokenHash = tokenHash,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
            CreatedByIp = ipAddress,
            FamilyId = existingFamilyId ?? Guid.NewGuid().ToString()
        };
        
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation(
            "Created refresh token for user {UserId}, family {FamilyId}",
            userId,
            refreshToken.FamilyId);
        
        // Return plaintext token to send to client, and entity for reference
        return (token, refreshToken);
    }
    
    /// <summary>
    /// Rotates a refresh token - validates old token, creates new one, invalidates old.
    /// </summary>
    public async Task<RefreshResult> RotateRefreshTokenAsync(
        string token,
        string? ipAddress = null)
    {
        var tokenHash = HashToken(token);
        
        var existingToken = await _context.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
        
        if (existingToken is null)
        {
            _logger.LogWarning("Refresh token not found");
            return RefreshResult.Failed("Invalid refresh token");
        }
        
        // Check if token was already used (reuse attack detection!)
        if (existingToken.IsRevoked)
        {
            _logger.LogWarning(
                "Reuse detected for token family {FamilyId}, user {UserId}. Revoking all family tokens.",
                existingToken.FamilyId,
                existingToken.UserId);
            
            // Revoke ALL tokens in this family - potential attack!
            await RevokeTokenFamilyAsync(
                existingToken.FamilyId,
                "Reuse detected - potential token theft");
            
            return RefreshResult.Failed(
                "Token reuse detected. All sessions have been terminated for security.");
        }
        
        if (existingToken.IsExpired)
        {
            _logger.LogWarning(
                "Expired refresh token used for user {UserId}",
                existingToken.UserId);
            
            return RefreshResult.Failed("Refresh token has expired. Please log in again.");
        }
        
        // Check if user is still active
        if (!existingToken.User.IsActive)
        {
            await RevokeTokenFamilyAsync(existingToken.FamilyId, "User deactivated");
            return RefreshResult.Failed("Account has been deactivated.");
        }
        
        // Create new token (same family for tracking)
        var (newToken, newEntity) = await CreateRefreshTokenAsync(
            existingToken.UserId,
            ipAddress,
            existingToken.FamilyId);
        
        // Revoke old token (mark as replaced)
        existingToken.RevokedAt = DateTime.UtcNow;
        existingToken.ReplacedByTokenHash = newEntity.TokenHash;
        existingToken.RevokedReason = "Rotated";
        
        await _context.SaveChangesAsync();
        
        _logger.LogInformation(
            "Rotated refresh token for user {UserId}, family {FamilyId}",
            existingToken.UserId,
            existingToken.FamilyId);
        
        return RefreshResult.Success(newToken, existingToken.User);
    }
    
    /// <summary>
    /// Revokes all tokens in a family (used for logout or security events).
    /// </summary>
    public async Task RevokeTokenFamilyAsync(string familyId, string reason)
    {
        var tokens = await _context.RefreshTokens
            .Where(t => t.FamilyId == familyId && t.RevokedAt == null)
            .ToListAsync();
        
        foreach (var token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = reason;
        }
        
        await _context.SaveChangesAsync();
        
        _logger.LogInformation(
            "Revoked {Count} tokens in family {FamilyId}: {Reason}",
            tokens.Count,
            familyId,
            reason);
    }
    
    /// <summary>
    /// Revokes all tokens for a user (used for password change, security events).
    /// </summary>
    public async Task RevokeAllUserTokensAsync(string userId, string reason)
    {
        var tokens = await _context.RefreshTokens
            .Where(t => t.UserId == userId && t.RevokedAt == null)
            .ToListAsync();
        
        foreach (var token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = reason;
        }
        
        await _context.SaveChangesAsync();
        
        _logger.LogInformation(
            "Revoked all {Count} tokens for user {UserId}: {Reason}",
            tokens.Count,
            userId,
            reason);
    }
    
    /// <summary>
    /// Cleans up expired tokens (run periodically).
    /// </summary>
    public async Task CleanupExpiredTokensAsync(int daysToKeep = 30)
    {
        var cutoff = DateTime.UtcNow.AddDays(-daysToKeep);
        
        var expiredTokens = await _context.RefreshTokens
            .Where(t => t.ExpiresAt < cutoff)
            .ToListAsync();
        
        _context.RefreshTokens.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation(
            "Cleaned up {Count} expired refresh tokens",
            expiredTokens.Count);
    }
    
    private static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}

public record RefreshResult(
    bool Succeeded,
    string? NewToken,
    ApplicationUser? User,
    string? ErrorMessage)
{
    public static RefreshResult Success(string token, ApplicationUser user) =>
        new(true, token, user, null);
    
    public static RefreshResult Failed(string error) =>
        new(false, null, null, error);
}
```
