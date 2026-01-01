using NSubstitute;
using ShopFlow.Application.Auth.Commands;
using ShopFlow.Application.Auth.Handlers;
using ShopFlow.Application.Auth.Services;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Tests.Unit.Application;

public class AuthHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthHandler _handler;

    public AuthHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new AuthHandler(_userRepository, _passwordHasher, _jwtTokenGenerator, _unitOfWork);
    }

    /// <summary>
    /// Helper to simulate a persisted user with an ID (like EF Core would do).
    /// </summary>
    private static User CreatePersistedUser(string email, string passwordHash, string firstName, string lastName, int id = 1)
    {
        var user = User.Create(email, passwordHash, firstName, lastName);
        typeof(User).GetProperty("Id")!.SetValue(user, id);
        return user;
    }

    #region Register Tests

    [Fact]
    public async Task HandleAsync_Register_WithValidData_ReturnsAuthResult()
    {
        // Arrange
        var command = new RegisterCommand(
            Email: "test@example.com",
            Password: "Password123!",
            FirstName: "John",
            LastName: "Doe"
        );

        _userRepository.ExistsByEmailAsync(command.Email, null, Arg.Any<CancellationToken>())
            .Returns(false);
        _passwordHasher.HashPassword(command.Password)
            .Returns("hashedPassword");
        _jwtTokenGenerator.GenerateAccessToken(Arg.Any<User>())
            .Returns(("accessToken123", DateTime.UtcNow.AddMinutes(15)));
        _jwtTokenGenerator.GenerateRefreshToken()
            .Returns("refreshToken123");

        // Simulate EF Core setting the ID when the user is added
        _userRepository.When(x => x.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()))
            .Do(callInfo =>
            {
                var user = callInfo.Arg<User>();
                // Use reflection to set the ID, simulating what EF Core does
                typeof(User).GetProperty("Id")!.SetValue(user, 1);
            });

        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("accessToken123", result.AccessToken);
        Assert.Equal("refreshToken123", result.RefreshToken);
        Assert.Equal("test@example.com", result.User.Email);
        Assert.Equal("John", result.User.FirstName);
        Assert.Equal("Doe", result.User.LastName);

        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(2).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Register_WithExistingEmail_ThrowsValidationException()
    {
        // Arrange
        var command = new RegisterCommand(
            Email: "existing@example.com",
            Password: "Password123!",
            FirstName: "John",
            LastName: "Doe"
        );

        _userRepository.ExistsByEmailAsync(command.Email, null, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("existing@example.com", exception.Message);
        Assert.Contains("already exists", exception.Message);

        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("short")]
    [InlineData("nouppercase1")]
    [InlineData("NOLOWERCASE1")]
    [InlineData("NoDigitsHere")]
    public async Task HandleAsync_Register_WithWeakPassword_ThrowsValidationException(string weakPassword)
    {
        // Arrange
        var command = new RegisterCommand(
            Email: "test@example.com",
            Password: weakPassword,
            FirstName: "John",
            LastName: "Doe"
        );

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Register_WithEmptyPassword_ThrowsValidationException()
    {
        // Arrange
        var command = new RegisterCommand(
            Email: "test@example.com",
            Password: "",
            FirstName: "John",
            LastName: "Doe"
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Password is required", exception.Message);
    }

    #endregion

    #region Login Tests

    [Fact]
    public async Task HandleAsync_Login_WithValidCredentials_ReturnsAuthResult()
    {
        // Arrange
        var command = new LoginCommand(
            Email: "test@example.com",
            Password: "Password123!"
        );

        var user = CreatePersistedUser("test@example.com", "hashedPassword", "John", "Doe");

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.PasswordHash)
            .Returns(true);
        _jwtTokenGenerator.GenerateAccessToken(user)
            .Returns(("accessToken123", DateTime.UtcNow.AddMinutes(15)));
        _jwtTokenGenerator.GenerateRefreshToken()
            .Returns("refreshToken123");
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("accessToken123", result.AccessToken);
        Assert.Equal("refreshToken123", result.RefreshToken);
        Assert.Equal("test@example.com", result.User.Email);

        _userRepository.Received(1).Update(user);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Login_WithNonExistentEmail_ThrowsValidationException()
    {
        // Arrange
        var command = new LoginCommand(
            Email: "nonexistent@example.com",
            Password: "Password123!"
        );

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Invalid email or password", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_Login_WithWrongPassword_ThrowsValidationException()
    {
        // Arrange
        var command = new LoginCommand(
            Email: "test@example.com",
            Password: "WrongPassword123!"
        );

        var user = User.Create("test@example.com", "hashedPassword", "John", "Doe");

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);
        _passwordHasher.VerifyPassword(command.Password, user.PasswordHash)
            .Returns(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Invalid email or password", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_Login_WithDeactivatedUser_ThrowsValidationException()
    {
        // Arrange
        var command = new LoginCommand(
            Email: "test@example.com",
            Password: "Password123!"
        );

        var user = User.Create("test@example.com", "hashedPassword", "John", "Doe");
        user.Deactivate();

        _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("deactivated", exception.Message);
    }

    #endregion

    #region Refresh Token Tests

    [Fact]
    public async Task HandleAsync_RefreshToken_WithValidToken_ReturnsNewTokens()
    {
        // Arrange
        var command = new RefreshTokenCommand(RefreshToken: "validRefreshToken");

        var user = CreatePersistedUser("test@example.com", "hashedPassword", "John", "Doe");
        var refreshToken = RefreshToken.CreateWithDefaultExpiry("validRefreshToken", user.Id);
        user.AddRefreshToken(refreshToken);

        _userRepository.GetByRefreshTokenAsync(command.RefreshToken, Arg.Any<CancellationToken>())
            .Returns(user);
        _jwtTokenGenerator.GenerateAccessToken(user)
            .Returns(("newAccessToken", DateTime.UtcNow.AddMinutes(15)));
        _jwtTokenGenerator.GenerateRefreshToken()
            .Returns("newRefreshToken");
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newAccessToken", result.AccessToken);
        Assert.Equal("newRefreshToken", result.RefreshToken);

        _userRepository.Received(1).Update(user);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_RefreshToken_WithInvalidToken_ThrowsValidationException()
    {
        // Arrange
        var command = new RefreshTokenCommand(RefreshToken: "invalidToken");

        _userRepository.GetByRefreshTokenAsync(command.RefreshToken, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("Invalid or expired refresh token", exception.Message);
    }

    [Fact]
    public async Task HandleAsync_RefreshToken_WithDeactivatedUser_ThrowsValidationException()
    {
        // Arrange
        var command = new RefreshTokenCommand(RefreshToken: "validRefreshToken");

        var user = User.Create("test@example.com", "hashedPassword", "John", "Doe");
        user.AddRefreshToken(RefreshToken.CreateWithDefaultExpiry("validRefreshToken", 1));
        user.Deactivate();

        _userRepository.GetByRefreshTokenAsync(command.RefreshToken, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _handler.HandleAsync(command));

        Assert.Contains("deactivated", exception.Message);
    }

    #endregion

    #region Logout Tests

    [Fact]
    public async Task HandleAsync_Logout_WithValidToken_RevokesToken()
    {
        // Arrange
        var command = new LogoutCommand(RefreshToken: "validRefreshToken");

        var user = User.Create("test@example.com", "hashedPassword", "John", "Doe");
        user.AddRefreshToken(RefreshToken.CreateWithDefaultExpiry("validRefreshToken", 1));

        _userRepository.GetByRefreshTokenAsync(command.RefreshToken, Arg.Any<CancellationToken>())
            .Returns(user);
        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _userRepository.Received(1).Update(user);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Logout_WithInvalidToken_DoesNotThrow()
    {
        // Arrange
        var command = new LogoutCommand(RefreshToken: "invalidToken");

        _userRepository.GetByRefreshTokenAsync(command.RefreshToken, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act - Should not throw
        await _handler.HandleAsync(command);

        // Assert
        _userRepository.DidNotReceive().Update(Arg.Any<User>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    #endregion
}
