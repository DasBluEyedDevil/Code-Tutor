namespace ShopFlow.Application.Auth.Services;

/// <summary>
/// Interface for password hashing operations.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain-text password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a plain-text password against a hash.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <param name="hash">The hashed password to verify against.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    bool VerifyPassword(string password, string hash);
}
