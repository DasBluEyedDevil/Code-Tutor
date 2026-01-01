---
type: "EXAMPLE"
title: "Login Endpoint with Cookie"
---

This example demonstrates a secure login endpoint that validates credentials, handles lockout, and issues an authentication cookie.

```csharp
// ===== LOGIN COMMAND =====
// ShopFlow.Application/Features/Auth/Commands/LoginCommand.cs

using MediatR;
using System.Security.Claims;

namespace ShopFlow.Application.Features.Auth.Commands;

public record LoginCommand(
    string Email,
    string Password,
    bool RememberMe
) : IRequest<LoginResult>;

public record LoginResult(
    bool Succeeded,
    string? UserId,
    string? DisplayName,
    IEnumerable<string> Roles,
    LoginFailureReason? FailureReason,
    string? Message
);

public enum LoginFailureReason
{
    InvalidCredentials,
    AccountLockedOut,
    EmailNotConfirmed,
    AccountDisabled
}

// ===== LOGIN HANDLER =====
// ShopFlow.Application/Features/Auth/Commands/LoginHandler.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Application.Features.Auth.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginHandler> _logger;
    
    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }
    
    public async Task<LoginResult> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
        {
            _logger.LogWarning(
                "Login attempt for non-existent email: {Email}",
                request.Email);
            
            // Security: Don't reveal that user doesn't exist!
            // Use same response as invalid password
            return FailedLogin(LoginFailureReason.InvalidCredentials);
        }
        
        // Check if account is disabled
        if (!user.IsActive)
        {
            _logger.LogWarning(
                "Login attempt for disabled account: {UserId}",
                user.Id);
            
            return FailedLogin(LoginFailureReason.AccountDisabled);
        }
        
        // Check if account is locked out
        if (await _userManager.IsLockedOutAsync(user))
        {
            _logger.LogWarning(
                "Login attempt for locked account: {UserId}",
                user.Id);
            
            var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
            return new LoginResult(
                Succeeded: false,
                UserId: null,
                DisplayName: null,
                Roles: [],
                FailureReason: LoginFailureReason.AccountLockedOut,
                Message: $"Account is locked. Try again after {lockoutEnd?.LocalDateTime}"
            );
        }
        
        // Check if email is confirmed
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogWarning(
                "Login attempt with unconfirmed email: {UserId}",
                user.Id);
            
            return FailedLogin(LoginFailureReason.EmailNotConfirmed);
        }
        
        // Attempt sign-in (this handles password verification and lockout)
        var result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            isPersistent: request.RememberMe,
            lockoutOnFailure: true);
        
        if (result.Succeeded)
        {
            // Update last login timestamp
            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            
            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            
            _logger.LogInformation(
                "User logged in successfully: {UserId} ({Email})",
                user.Id,
                user.Email);
            
            return new LoginResult(
                Succeeded: true,
                UserId: user.Id,
                DisplayName: user.FullName,
                Roles: roles,
                FailureReason: null,
                Message: "Login successful"
            );
        }
        
        if (result.IsLockedOut)
        {
            _logger.LogWarning(
                "Account locked after failed attempts: {UserId}",
                user.Id);
            
            return new LoginResult(
                Succeeded: false,
                UserId: null,
                DisplayName: null,
                Roles: [],
                FailureReason: LoginFailureReason.AccountLockedOut,
                Message: "Account locked due to multiple failed attempts. Try again later."
            );
        }
        
        if (result.RequiresTwoFactor)
        {
            // Handle 2FA - covered in advanced lesson
            _logger.LogInformation(
                "2FA required for user: {UserId}",
                user.Id);
            
            return new LoginResult(
                Succeeded: false,
                UserId: user.Id,
                DisplayName: null,
                Roles: [],
                FailureReason: null,
                Message: "Two-factor authentication required"
            );
        }
        
        // Password was incorrect
        _logger.LogWarning(
            "Failed login attempt for user: {UserId}",
            user.Id);
        
        return FailedLogin(LoginFailureReason.InvalidCredentials);
    }
    
    private static LoginResult FailedLogin(LoginFailureReason reason) =>
        new(
            Succeeded: false,
            UserId: null,
            DisplayName: null,
            Roles: [],
            FailureReason: reason,
            Message: reason switch
            {
                LoginFailureReason.InvalidCredentials => "Invalid email or password",
                LoginFailureReason.AccountLockedOut => "Account is temporarily locked",
                LoginFailureReason.EmailNotConfirmed => "Please confirm your email before logging in",
                LoginFailureReason.AccountDisabled => "This account has been disabled",
                _ => "Login failed"
            }
        );
}

// ===== API ENDPOINT =====
// Add to AuthEndpoints.cs

group.MapPost("/login", Login)
    .WithName("Login")
    .WithSummary("Authenticate user and create session")
    .Produces<LoginResponse>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status423Locked)  // Account locked
    .AllowAnonymous();

private static async Task<IResult> Login(
    [FromBody] LoginRequest request,
    [FromServices] ISender sender)
{
    var command = new LoginCommand(
        Email: request.Email,
        Password: request.Password,
        RememberMe: request.RememberMe
    );
    
    var result = await sender.Send(command);
    
    if (!result.Succeeded)
    {
        return result.FailureReason switch
        {
            LoginFailureReason.AccountLockedOut =>
                Results.Problem(
                    statusCode: StatusCodes.Status423Locked,
                    title: "Account Locked",
                    detail: result.Message),
            
            LoginFailureReason.EmailNotConfirmed =>
                Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    title: "Email Not Confirmed",
                    detail: result.Message),
            
            _ => Results.Unauthorized()
        };
    }
    
    return Results.Ok(new LoginResponse(
        UserId: result.UserId!,
        DisplayName: result.DisplayName!,
        Roles: result.Roles
    ));
}

public record LoginRequest(
    string Email,
    string Password,
    bool RememberMe = false
);

public record LoginResponse(
    string UserId,
    string DisplayName,
    IEnumerable<string> Roles
);
```
