using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public record LogoutCommand(string UserId) : IRequest<LogoutResult>;

public record LogoutResult(bool Succeeded, string Message);

public class LogoutHandler : IRequestHandler<LogoutCommand, LogoutResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LogoutHandler> _logger;
    
    public LogoutHandler(
        SignInManager<ApplicationUser> signInManager,
        ILogger<LogoutHandler> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }
    
    public async Task<LogoutResult> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        
        _logger.LogInformation(
            "User logged out: {UserId} at {Timestamp}",
            request.UserId,
            DateTime.UtcNow);
        
        return new LogoutResult(
            Succeeded: true,
            Message: "You have been logged out successfully"
        );
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/auth/logout", async (
    HttpContext httpContext,
    ISender sender) =>
{
    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    
    if (string.IsNullOrEmpty(userId))
    {
        return Results.Unauthorized();
    }
    
    var result = await sender.Send(new LogoutCommand(userId));
    
    return Results.Ok(new { result.Message });
})
.RequireAuthorization()
.WithName("Logout")
.WithTags("Authentication");

app.Run();