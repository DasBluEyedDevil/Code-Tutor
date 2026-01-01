using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

// Assume ApplicationUser is already defined
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

// TODO: Create LogoutCommand (IRequest<LogoutResult>)
// It needs the current user's ID

// TODO: Create LogoutResult record
// - Succeeded (bool)
// - Message (string)

// TODO: Create LogoutHandler (IRequestHandler<LogoutCommand, LogoutResult>)
// - Inject SignInManager<ApplicationUser> and ILogger
// - Call SignOutAsync()
// - Log the logout event
// - Return success result

// API Endpoint setup
var builder = WebApplication.CreateBuilder(args);

// Assume Identity is configured...
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// TODO: Map POST /api/auth/logout endpoint
// - Require authorization
// - Get user ID from HttpContext.User
// - Send LogoutCommand
// - Return appropriate response

app.Run();