namespace ShopFlow.Domain.ValueObjects;

/// <summary>
/// Represents a refresh token for JWT authentication.
/// Tracks token lifecycle including expiration and revocation.
/// </summary>
public class RefreshToken
{
    public int Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public int UserId { get; private set; }

    /// <summary>
    /// Indicates if the token is still valid (not expired and not revoked).
    /// </summary>
    public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;

    /// <summary>
    /// Indicates if the token has expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    /// <summary>
    /// Indicates if the token has been revoked.
    /// </summary>
    public bool IsRevoked => RevokedAt != null;

    // EF Core requires a parameterless constructor
    private RefreshToken() { }

    /// <summary>
    /// Creates a new RefreshToken.
    /// </summary>
    /// <param name="token">The unique token string.</param>
    /// <param name="expiresAt">When the token expires.</param>
    /// <param name="userId">The ID of the user this token belongs to.</param>
    public static RefreshToken Create(string token, DateTime expiresAt, int userId)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be empty.", nameof(token));

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future.", nameof(expiresAt));

        if (userId <= 0)
            throw new ArgumentException("User ID must be positive.", nameof(userId));

        return new RefreshToken
        {
            Token = token,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
    }

    /// <summary>
    /// Creates a new RefreshToken with default 7-day expiration.
    /// </summary>
    /// <param name="token">The unique token string.</param>
    /// <param name="userId">The ID of the user this token belongs to.</param>
    public static RefreshToken CreateWithDefaultExpiry(string token, int userId)
    {
        return Create(token, DateTime.UtcNow.AddDays(7), userId);
    }

    /// <summary>
    /// Revokes the token, making it invalid for future use.
    /// </summary>
    public void Revoke()
    {
        if (RevokedAt == null)
        {
            RevokedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Checks if this token can be used for token refresh.
    /// </summary>
    public bool CanBeUsedForRefresh()
    {
        return IsActive;
    }
}
