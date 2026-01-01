using ShopFlow.Domain.Entities;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class UserTests
{
    [Fact]
    public void Create_WithValidData_ReturnsUser()
    {
        // Arrange & Act
        var user = User.Create(
            "test@example.com",
            "hashedPassword123",
            "John",
            "Doe"
        );

        // Assert
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("hashedPassword123", user.PasswordHash);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal("John Doe", user.FullName);
        Assert.Equal("User", user.Roles);
        Assert.True(user.IsActive);
    }

    [Fact]
    public void Create_WithCustomRoles_SetsRoles()
    {
        // Arrange & Act
        var user = User.Create(
            "admin@example.com",
            "hashedPassword123",
            "Admin",
            "User",
            "Admin,Manager"
        );

        // Assert
        Assert.Equal("Admin,Manager", user.Roles);
        Assert.True(user.HasRole("Admin"));
        Assert.True(user.HasRole("Manager"));
        Assert.False(user.HasRole("User"));
    }

    [Fact]
    public void Create_WithInvalidEmail_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            User.Create("invalid-email", "hash", "John", "Doe"));
    }

    [Fact]
    public void Create_WithEmptyEmail_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            User.Create("", "hash", "John", "Doe"));
    }

    [Fact]
    public void Create_WithEmptyPasswordHash_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            User.Create("test@example.com", "", "John", "Doe"));
    }

    [Fact]
    public void Create_WithEmptyFirstName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            User.Create("test@example.com", "hash", "", "Doe"));
    }

    [Fact]
    public void Create_WithEmptyLastName_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            User.Create("test@example.com", "hash", "John", ""));
    }

    [Fact]
    public void Create_NormalizesEmail_ToLowerCase()
    {
        // Arrange & Act
        var user = User.Create(
            "TEST@EXAMPLE.COM",
            "hash",
            "John",
            "Doe"
        );

        // Assert
        Assert.Equal("test@example.com", user.Email);
    }

    [Fact]
    public void HasRole_WithExistingRole_ReturnsTrue()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe", "Admin,User");

        // Act & Assert
        Assert.True(user.HasRole("Admin"));
        Assert.True(user.HasRole("User"));
        Assert.True(user.HasRole("admin")); // Case-insensitive
    }

    [Fact]
    public void HasRole_WithNonExistingRole_ReturnsFalse()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe", "User");

        // Act & Assert
        Assert.False(user.HasRole("Admin"));
        Assert.False(user.HasRole("Manager"));
    }

    [Fact]
    public void GetRoles_ReturnsListOfRoles()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe", "Admin, User, Manager");

        // Act
        var roles = user.GetRoles();

        // Assert
        Assert.Equal(3, roles.Count);
        Assert.Contains("Admin", roles);
        Assert.Contains("User", roles);
        Assert.Contains("Manager", roles);
    }

    [Fact]
    public void UpdateName_UpdatesFirstAndLastName()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");

        // Act
        user.UpdateName("Jane", "Smith");

        // Assert
        Assert.Equal("Jane", user.FirstName);
        Assert.Equal("Smith", user.LastName);
        Assert.Equal("Jane Smith", user.FullName);
        Assert.NotNull(user.UpdatedAt);
    }

    [Fact]
    public void UpdateEmail_UpdatesEmail()
    {
        // Arrange
        var user = User.Create("old@example.com", "hash", "John", "Doe");

        // Act
        user.UpdateEmail("new@example.com");

        // Assert
        Assert.Equal("new@example.com", user.Email);
        Assert.NotNull(user.UpdatedAt);
    }

    [Fact]
    public void UpdatePasswordHash_UpdatesPasswordHash()
    {
        // Arrange
        var user = User.Create("test@example.com", "oldHash", "John", "Doe");

        // Act
        user.UpdatePasswordHash("newHash");

        // Assert
        Assert.Equal("newHash", user.PasswordHash);
        Assert.NotNull(user.UpdatedAt);
    }

    [Fact]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");

        // Act
        user.Deactivate();

        // Assert
        Assert.False(user.IsActive);
        Assert.NotNull(user.UpdatedAt);
    }

    [Fact]
    public void Activate_SetsIsActiveToTrue()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        user.Deactivate();

        // Act
        user.Activate();

        // Assert
        Assert.True(user.IsActive);
    }

    [Fact]
    public void AddRefreshToken_AddsTokenToCollection()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        // Note: We need to save the user first to get an ID
        // For testing purposes, we'll create a token with userId 1
        var token = RefreshToken.CreateWithDefaultExpiry("tokenValue123", 1);

        // Act
        user.AddRefreshToken(token);

        // Assert
        Assert.Single(user.RefreshTokens);
        Assert.Equal("tokenValue123", user.RefreshTokens[0].Token);
    }

    [Fact]
    public void RevokeRefreshToken_RevokesSpecificToken()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        var token1 = RefreshToken.CreateWithDefaultExpiry("token1", 1);
        var token2 = RefreshToken.CreateWithDefaultExpiry("token2", 1);
        user.AddRefreshToken(token1);
        user.AddRefreshToken(token2);

        // Act
        user.RevokeRefreshToken("token1");

        // Assert
        Assert.True(user.RefreshTokens[0].IsRevoked);
        Assert.False(user.RefreshTokens[1].IsRevoked);
    }

    [Fact]
    public void RevokeAllRefreshTokens_RevokesAllActiveTokens()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        user.AddRefreshToken(RefreshToken.CreateWithDefaultExpiry("token1", 1));
        user.AddRefreshToken(RefreshToken.CreateWithDefaultExpiry("token2", 1));

        // Act
        user.RevokeAllRefreshTokens();

        // Assert
        Assert.All(user.RefreshTokens, t => Assert.True(t.IsRevoked));
    }

    [Fact]
    public void GetActiveRefreshToken_ReturnsActiveToken()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        var token = RefreshToken.CreateWithDefaultExpiry("activeToken", 1);
        user.AddRefreshToken(token);

        // Act
        var activeToken = user.GetActiveRefreshToken("activeToken");

        // Assert
        Assert.NotNull(activeToken);
        Assert.Equal("activeToken", activeToken.Token);
    }

    [Fact]
    public void GetActiveRefreshToken_ReturnsNull_ForRevokedToken()
    {
        // Arrange
        var user = User.Create("test@example.com", "hash", "John", "Doe");
        var token = RefreshToken.CreateWithDefaultExpiry("revokedToken", 1);
        user.AddRefreshToken(token);
        user.RevokeRefreshToken("revokedToken");

        // Act
        var activeToken = user.GetActiveRefreshToken("revokedToken");

        // Assert
        Assert.Null(activeToken);
    }
}
