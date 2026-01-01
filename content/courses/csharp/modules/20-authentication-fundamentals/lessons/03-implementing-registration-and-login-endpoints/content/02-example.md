---
type: "EXAMPLE"
title: "Registration Endpoint"
---

This example shows a complete registration endpoint using the CQRS pattern with MediatR, including validation, user creation, and email confirmation token generation.

```csharp
// ===== REGISTRATION COMMAND =====
// ShopFlow.Application/Features/Auth/Commands/RegisterCommand.cs

using MediatR;
using FluentValidation;

namespace ShopFlow.Application.Features.Auth.Commands;

public record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    DateTime DateOfBirth
) : IRequest<RegisterResult>;

public record RegisterResult(
    bool Succeeded,
    string? UserId,
    string? EmailConfirmationToken,
    IEnumerable<string> Errors
);

// ===== REGISTRATION VALIDATOR =====
// ShopFlow.Application/Features/Auth/Commands/RegisterCommandValidator.cs

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(256).WithMessage("Email must not exceed 256 characters");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(14).WithMessage("Password must be at least 14 characters")
            .Matches("[A-Z]").WithMessage("Password must contain uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain a digit")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain special character");
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(100).WithMessage("First name must not exceed 100 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .Must(BeAtLeast18YearsOld).WithMessage("You must be at least 18 years old");
    }
    
    private static bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        return age >= 18;
    }
}

// ===== REGISTRATION HANDLER =====
// ShopFlow.Application/Features/Auth/Commands/RegisterHandler.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Application.Features.Auth.Commands;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<RegisterHandler> _logger;
    
    public RegisterHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<RegisterHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<RegisterResult> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            _logger.LogWarning(
                "Registration attempted for existing email: {Email}",
                request.Email);
            
            // Security: Don't reveal that email exists!
            // Return generic error
            return new RegisterResult(
                Succeeded: false,
                UserId: null,
                EmailConfirmationToken: null,
                Errors: ["Registration failed. Please check your information."]
            );
        }
        
        // Create new user
        var user = new ApplicationUser
        {
            UserName = request.Email,  // Use email as username
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            CreatedAt = DateTime.UtcNow
        };
        
        // Create user with password (Identity handles hashing)
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            _logger.LogWarning(
                "User registration failed for {Email}: {Errors}",
                request.Email,
                string.Join(", ", result.Errors.Select(e => e.Description)));
            
            return new RegisterResult(
                Succeeded: false,
                UserId: null,
                EmailConfirmationToken: null,
                Errors: result.Errors.Select(e => e.Description)
            );
        }
        
        // Assign default role
        await _userManager.AddToRoleAsync(user, "Customer");
        
        // Generate email confirmation token
        var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        _logger.LogInformation(
            "User registered successfully: {UserId} ({Email})",
            user.Id,
            user.Email);
        
        return new RegisterResult(
            Succeeded: true,
            UserId: user.Id,
            EmailConfirmationToken: emailToken,
            Errors: []
        );
    }
}

// ===== API ENDPOINT =====
// ShopFlow.Api/Endpoints/AuthEndpoints.cs

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopFlow.Application.Features.Auth.Commands;

namespace ShopFlow.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/auth").WithTags("Authentication");
        
        group.MapPost("/register", Register)
            .WithName("Register")
            .WithSummary("Register a new user account")
            .Produces<RegisterResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .AllowAnonymous();
    }
    
    private static async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] ISender sender,
        [FromServices] IEmailService emailService,
        HttpContext httpContext)
    {
        var command = new RegisterCommand(
            Email: request.Email,
            Password: request.Password,
            ConfirmPassword: request.ConfirmPassword,
            FirstName: request.FirstName,
            LastName: request.LastName,
            DateOfBirth: request.DateOfBirth
        );
        
        var result = await sender.Send(command);
        
        if (!result.Succeeded)
        {
            return Results.BadRequest(new { Errors = result.Errors });
        }
        
        // Send confirmation email
        var confirmationLink = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}" +
            $"/api/auth/confirm-email?userId={result.UserId}&token={Uri.EscapeDataString(result.EmailConfirmationToken!)}";
        
        await emailService.SendEmailConfirmationAsync(
            request.Email,
            request.FirstName,
            confirmationLink);
        
        return Results.Created(
            $"/api/users/{result.UserId}",
            new RegisterResponse(
                Message: "Registration successful. Please check your email to confirm your account."
            ));
    }
}

public record RegisterRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    DateTime DateOfBirth
);

public record RegisterResponse(string Message);
```
