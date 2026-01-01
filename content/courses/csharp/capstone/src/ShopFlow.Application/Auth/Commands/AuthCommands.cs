namespace ShopFlow.Application.Auth.Commands;

/// <summary>
/// Command to register a new user.
/// </summary>
public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
);

/// <summary>
/// Command to log in an existing user.
/// </summary>
public record LoginCommand(
    string Email,
    string Password
);

/// <summary>
/// Command to refresh an access token.
/// </summary>
public record RefreshTokenCommand(
    string RefreshToken
);

/// <summary>
/// Command to log out a user (revoke refresh token).
/// </summary>
public record LogoutCommand(
    string RefreshToken
);
