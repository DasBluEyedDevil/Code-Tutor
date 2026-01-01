using ShopFlow.Application.Auth.Commands;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Application.Auth.Services;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Exceptions;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Application.Auth.Handlers;

/// <summary>
/// Handles authentication-related commands.
/// </summary>
public class AuthHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public AuthHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    public async Task<AuthResultDto> HandleAsync(RegisterCommand command, CancellationToken cancellationToken = default)
    {
        // Validate password
        ValidatePassword(command.Password);

        // Check if email already exists
        if (await _userRepository.ExistsByEmailAsync(command.Email, cancellationToken: cancellationToken))
        {
            throw new ValidationException($"A user with the email '{command.Email}' already exists.");
        }

        // Hash the password
        var passwordHash = _passwordHasher.HashPassword(command.Password);

        // Create the user
        var user = User.Create(
            command.Email,
            passwordHash,
            command.FirstName,
            command.LastName
        );

        // Generate tokens
        var (accessToken, expiresAt) = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenString = _jwtTokenGenerator.GenerateRefreshToken();

        // Add user first to get ID
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Create and add refresh token
        var refreshToken = RefreshToken.CreateWithDefaultExpiry(refreshTokenString, user.Id);
        user.AddRefreshToken(refreshToken);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResultDto(
            accessToken,
            refreshTokenString,
            expiresAt,
            MapToUserDto(user)
        );
    }

    /// <summary>
    /// Logs in an existing user.
    /// </summary>
    public async Task<AuthResultDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        // Find user by email
        var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (user == null)
        {
            throw new ValidationException("Invalid email or password.");
        }

        // Check if user is active
        if (!user.IsActive)
        {
            throw new ValidationException("This account has been deactivated.");
        }

        // Verify password
        if (!_passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new ValidationException("Invalid email or password.");
        }

        // Generate tokens
        var (accessToken, expiresAt) = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenString = _jwtTokenGenerator.GenerateRefreshToken();

        // Create and add refresh token
        var refreshToken = RefreshToken.CreateWithDefaultExpiry(refreshTokenString, user.Id);
        user.AddRefreshToken(refreshToken);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResultDto(
            accessToken,
            refreshTokenString,
            expiresAt,
            MapToUserDto(user)
        );
    }

    /// <summary>
    /// Refreshes an access token using a valid refresh token.
    /// </summary>
    public async Task<TokenRefreshResultDto> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        // Find user by refresh token
        var user = await _userRepository.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken);
        if (user == null)
        {
            throw new ValidationException("Invalid or expired refresh token.");
        }

        // Check if user is active
        if (!user.IsActive)
        {
            throw new ValidationException("This account has been deactivated.");
        }

        // Get the active refresh token
        var existingToken = user.GetActiveRefreshToken(command.RefreshToken);
        if (existingToken == null || !existingToken.CanBeUsedForRefresh())
        {
            throw new ValidationException("Invalid or expired refresh token.");
        }

        // Revoke the old refresh token
        user.RevokeRefreshToken(command.RefreshToken);

        // Generate new tokens
        var (accessToken, expiresAt) = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenString = _jwtTokenGenerator.GenerateRefreshToken();

        // Create and add new refresh token
        var newRefreshToken = RefreshToken.CreateWithDefaultExpiry(refreshTokenString, user.Id);
        user.AddRefreshToken(newRefreshToken);

        // Clean up expired tokens
        user.RemoveExpiredRefreshTokens();

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokenRefreshResultDto(
            accessToken,
            refreshTokenString,
            expiresAt
        );
    }

    /// <summary>
    /// Logs out a user by revoking their refresh token.
    /// </summary>
    public async Task HandleAsync(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        // Find user by refresh token
        var user = await _userRepository.GetByRefreshTokenAsync(command.RefreshToken, cancellationToken);
        if (user == null)
        {
            // Token doesn't exist or already expired - consider logout successful
            return;
        }

        // Revoke the refresh token
        user.RevokeRefreshToken(command.RefreshToken);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ValidationException("Password is required.");

        if (password.Length < 8)
            throw new ValidationException("Password must be at least 8 characters long.");

        if (!password.Any(char.IsUpper))
            throw new ValidationException("Password must contain at least one uppercase letter.");

        if (!password.Any(char.IsLower))
            throw new ValidationException("Password must contain at least one lowercase letter.");

        if (!password.Any(char.IsDigit))
            throw new ValidationException("Password must contain at least one digit.");
    }

    private static UserDto MapToUserDto(User user) => new(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.FullName,
        user.GetRoles(),
        user.CreatedAt
    );
}
