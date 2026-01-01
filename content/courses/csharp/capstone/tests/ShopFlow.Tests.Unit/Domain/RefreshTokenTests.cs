using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Domain;

public class RefreshTokenTests
{
    [Fact]
    public void Create_WithValidData_ReturnsToken()
    {
        // Arrange
        var expiresAt = DateTime.UtcNow.AddDays(7);

        // Act
        var token = RefreshToken.Create("tokenValue123", expiresAt, 1);

        // Assert
        Assert.Equal("tokenValue123", token.Token);
        Assert.Equal(expiresAt, token.ExpiresAt);
        Assert.Equal(1, token.UserId);
        Assert.Null(token.RevokedAt);
        Assert.True(token.IsActive);
        Assert.False(token.IsExpired);
        Assert.False(token.IsRevoked);
    }

    [Fact]
    public void Create_WithEmptyToken_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            RefreshToken.Create("", DateTime.UtcNow.AddDays(7), 1));
    }

    [Fact]
    public void Create_WithPastExpirationDate_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            RefreshToken.Create("token", DateTime.UtcNow.AddMinutes(-1), 1));
    }

    [Fact]
    public void Create_WithInvalidUserId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            RefreshToken.Create("token", DateTime.UtcNow.AddDays(7), 0));

        Assert.Throws<ArgumentException>(() =>
            RefreshToken.Create("token", DateTime.UtcNow.AddDays(7), -1));
    }

    [Fact]
    public void CreateWithDefaultExpiry_CreatesTokenWith7DayExpiry()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddDays(7);

        // Act
        var token = RefreshToken.CreateWithDefaultExpiry("tokenValue", 1);
        var afterCreation = DateTime.UtcNow.AddDays(7);

        // Assert
        Assert.True(token.ExpiresAt >= beforeCreation.AddSeconds(-1));
        Assert.True(token.ExpiresAt <= afterCreation.AddSeconds(1));
    }

    [Fact]
    public void Revoke_SetsRevokedAt()
    {
        // Arrange
        var token = RefreshToken.CreateWithDefaultExpiry("token", 1);

        // Act
        token.Revoke();

        // Assert
        Assert.NotNull(token.RevokedAt);
        Assert.True(token.IsRevoked);
        Assert.False(token.IsActive);
    }

    [Fact]
    public void Revoke_CalledMultipleTimes_KeepsOriginalRevokedAt()
    {
        // Arrange
        var token = RefreshToken.CreateWithDefaultExpiry("token", 1);

        // Act
        token.Revoke();
        var firstRevokedAt = token.RevokedAt;
        Thread.Sleep(10); // Small delay to ensure time would change
        token.Revoke();

        // Assert
        Assert.Equal(firstRevokedAt, token.RevokedAt);
    }

    [Fact]
    public void IsActive_ReturnsFalse_WhenRevoked()
    {
        // Arrange
        var token = RefreshToken.CreateWithDefaultExpiry("token", 1);

        // Act
        token.Revoke();

        // Assert
        Assert.False(token.IsActive);
        Assert.False(token.CanBeUsedForRefresh());
    }

    [Fact]
    public void CanBeUsedForRefresh_ReturnsTrue_WhenActiveAndNotExpired()
    {
        // Arrange
        var token = RefreshToken.CreateWithDefaultExpiry("token", 1);

        // Assert
        Assert.True(token.CanBeUsedForRefresh());
    }

    [Fact]
    public void CanBeUsedForRefresh_ReturnsFalse_WhenRevoked()
    {
        // Arrange
        var token = RefreshToken.CreateWithDefaultExpiry("token", 1);
        token.Revoke();

        // Assert
        Assert.False(token.CanBeUsedForRefresh());
    }
}
