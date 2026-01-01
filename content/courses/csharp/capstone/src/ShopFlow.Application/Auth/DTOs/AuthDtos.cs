namespace ShopFlow.Application.Auth.DTOs;

/// <summary>
/// Represents the result of a successful authentication.
/// </summary>
public record AuthResultDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);

/// <summary>
/// Represents user information for the client.
/// </summary>
public record UserDto(
    int Id,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    IReadOnlyList<string> Roles,
    DateTime CreatedAt
);

/// <summary>
/// Represents the result of a token refresh.
/// </summary>
public record TokenRefreshResultDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);
