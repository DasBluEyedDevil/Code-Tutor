using ShopFlow.Application.Auth.Services;

namespace ShopFlow.Infrastructure.Auth;

/// <summary>
/// BCrypt-based password hasher implementation.
/// Uses BCrypt for secure password hashing with automatic salting.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 11;

    /// <summary>
    /// Hashes a plain-text password using BCrypt.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The BCrypt hash of the password.</returns>
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    /// <summary>
    /// Verifies a plain-text password against a BCrypt hash.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <param name="hash">The BCrypt hash to verify against.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            return false;

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            // Invalid hash format
            return false;
        }
    }
}
