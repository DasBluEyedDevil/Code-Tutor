using ShopFlow.Application.Auth.Commands;
using ShopFlow.Application.Auth.DTOs;
using ShopFlow.Application.Auth.Handlers;
using ShopFlow.Domain.Exceptions;

namespace ShopFlow.Api.Endpoints;

/// <summary>
/// Authentication API endpoints using minimal API pattern.
/// </summary>
public static class AuthEndpoints
{
    /// <summary>
    /// Maps all authentication-related endpoints.
    /// </summary>
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        group.MapPost("/register", Register)
            .WithName("Register")
            .WithDescription("Registers a new user account");

        group.MapPost("/login", Login)
            .WithName("Login")
            .WithDescription("Authenticates a user and returns JWT tokens");

        group.MapPost("/refresh", RefreshToken)
            .WithName("RefreshToken")
            .WithDescription("Refreshes an access token using a valid refresh token");

        group.MapPost("/logout", Logout)
            .WithName("Logout")
            .WithDescription("Logs out a user by revoking their refresh token");
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    private static async Task<IResult> Register(
        RegisterRequest request,
        AuthHandler authHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new RegisterCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName
            );

            var result = await authHandler.HandleAsync(command, cancellationToken);
            return Results.Created($"/api/users/{result.User.Id}", result);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(new { Message = ex.Message, Errors = ex.Errors });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Authenticates a user and returns JWT tokens.
    /// </summary>
    private static async Task<IResult> Login(
        LoginRequest request,
        AuthHandler authHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new LoginCommand(request.Email, request.Password);
            var result = await authHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(result);
        }
        catch (ValidationException ex)
        {
            return Results.BadRequest(new { Message = ex.Message, Errors = ex.Errors });
        }
    }

    /// <summary>
    /// Refreshes an access token using a valid refresh token.
    /// </summary>
    private static async Task<IResult> RefreshToken(
        RefreshTokenRequest request,
        AuthHandler authHandler,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new RefreshTokenCommand(request.RefreshToken);
            var result = await authHandler.HandleAsync(command, cancellationToken);
            return Results.Ok(result);
        }
        catch (ValidationException)
        {
            return Results.Unauthorized();
        }
    }

    /// <summary>
    /// Logs out a user by revoking their refresh token.
    /// </summary>
    private static async Task<IResult> Logout(
        LogoutRequest request,
        AuthHandler authHandler,
        CancellationToken cancellationToken = default)
    {
        var command = new LogoutCommand(request.RefreshToken);
        await authHandler.HandleAsync(command, cancellationToken);
        return Results.NoContent();
    }
}

/// <summary>
/// Request model for user registration.
/// </summary>
public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName
);

/// <summary>
/// Request model for user login.
/// </summary>
public record LoginRequest(
    string Email,
    string Password
);

/// <summary>
/// Request model for token refresh.
/// </summary>
public record RefreshTokenRequest(
    string RefreshToken
);

/// <summary>
/// Request model for logout.
/// </summary>
public record LogoutRequest(
    string RefreshToken
);
