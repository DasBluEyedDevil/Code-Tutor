using ShopFlow.Domain.Entities;

namespace ShopFlow.Application.Auth.Services;

/// <summary>
/// Interface for JWT token generation operations.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    /// <param name="user">The user to generate a token for.</param>
    /// <returns>A tuple containing the access token and its expiration time.</returns>
    (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);

    /// <summary>
    /// Generates a unique refresh token string.
    /// </summary>
    /// <returns>A unique refresh token string.</returns>
    string GenerateRefreshToken();
}
