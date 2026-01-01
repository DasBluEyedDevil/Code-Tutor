using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// Encapsulates business rules for user authentication and management.
/// </summary>
public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Roles { get; private set; } = "User";
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Navigation property for refresh tokens
    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    // EF Core requires a parameterless constructor
    private User() { }

    /// <summary>
    /// Creates a new User with the specified details.
    /// </summary>
    /// <param name="email">User email (required, must be valid format).</param>
    /// <param name="passwordHash">Hashed password (required).</param>
    /// <param name="firstName">User's first name (required).</param>
    /// <param name="lastName">User's last name (required).</param>
    /// <param name="roles">User roles (comma-separated, default: "User").</param>
    public static User Create(
        string email,
        string passwordHash,
        string firstName,
        string lastName,
        string roles = "User")
    {
        ValidateEmail(email);
        ValidatePasswordHash(passwordHash);
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        return new User
        {
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Roles = string.IsNullOrWhiteSpace(roles) ? "User" : roles,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    /// Gets the user's full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Updates the user's name.
    /// </summary>
    public void UpdateName(string firstName, string lastName)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the user's email.
    /// </summary>
    public void UpdateEmail(string email)
    {
        ValidateEmail(email);
        Email = email.Trim().ToLowerInvariant();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the user's password hash.
    /// </summary>
    public void UpdatePasswordHash(string passwordHash)
    {
        ValidatePasswordHash(passwordHash);
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the user's roles.
    /// </summary>
    public void UpdateRoles(string roles)
    {
        Roles = string.IsNullOrWhiteSpace(roles) ? "User" : roles;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the user has a specific role.
    /// </summary>
    public bool HasRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return false;

        return Roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .Contains(role, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets all roles as a list.
    /// </summary>
    public IReadOnlyList<string> GetRoles()
    {
        return Roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .ToList();
    }

    /// <summary>
    /// Adds a new refresh token for the user.
    /// </summary>
    public void AddRefreshToken(RefreshToken token)
    {
        if (token == null)
            throw new ArgumentNullException(nameof(token));

        _refreshTokens.Add(token);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Revokes a specific refresh token.
    /// </summary>
    public void RevokeRefreshToken(string tokenValue)
    {
        var token = _refreshTokens.FirstOrDefault(t => t.Token == tokenValue);
        if (token != null)
        {
            token.Revoke();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Revokes all refresh tokens for the user.
    /// </summary>
    public void RevokeAllRefreshTokens()
    {
        foreach (var token in _refreshTokens.Where(t => t.IsActive))
        {
            token.Revoke();
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the active refresh token matching the provided value.
    /// </summary>
    public RefreshToken? GetActiveRefreshToken(string tokenValue)
    {
        return _refreshTokens.FirstOrDefault(t => t.Token == tokenValue && t.IsActive);
    }

    /// <summary>
    /// Removes expired refresh tokens.
    /// </summary>
    public void RemoveExpiredRefreshTokens()
    {
        var expiredTokens = _refreshTokens.Where(t => !t.IsActive).ToList();
        foreach (var token in expiredTokens)
        {
            _refreshTokens.Remove(token);
        }
    }

    /// <summary>
    /// Deactivates the user.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        RevokeAllRefreshTokens();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reactivates the user.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (email.Length > 256)
            throw new ArgumentException("Email cannot exceed 256 characters.", nameof(email));

        // Basic email format validation
        if (!email.Contains('@') || !email.Contains('.'))
            throw new ArgumentException("Email must be a valid email address.", nameof(email));
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));
    }

    private static void ValidateName(string name, string paramName)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"{paramName} is required.", paramName);

        if (name.Length > 100)
            throw new ArgumentException($"{paramName} cannot exceed 100 characters.", paramName);
    }
}
