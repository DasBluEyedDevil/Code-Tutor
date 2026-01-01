namespace ShopFlow.Infrastructure.Auth;

/// <summary>
/// Configuration settings for JWT token generation.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// The secret key used for signing tokens.
    /// Should be at least 32 characters (256 bits) for HS256.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// The token issuer (who created the token).
    /// </summary>
    public string Issuer { get; set; } = "ShopFlow";

    /// <summary>
    /// The token audience (who the token is intended for).
    /// </summary>
    public string Audience { get; set; } = "ShopFlowUsers";

    /// <summary>
    /// Access token expiration time in minutes.
    /// Default: 15 minutes for security.
    /// </summary>
    public int AccessTokenExpirationMinutes { get; set; } = 15;

    /// <summary>
    /// Refresh token expiration time in days.
    /// Default: 7 days.
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 7;
}
