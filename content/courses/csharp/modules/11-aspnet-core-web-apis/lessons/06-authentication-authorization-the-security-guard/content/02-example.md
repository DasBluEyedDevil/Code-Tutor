---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== OPTION 1: MapIdentityApi (.NET 8/9 - EASIEST!) =====
// Built-in endpoints for /register, /login, /refresh, /confirmEmail, etc.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Identity with Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AuthDb"));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Map ALL Identity endpoints automatically!
app.MapIdentityApi<IdentityUser>();  // Creates /register, /login, /refresh, etc.!

app.UseAuthorization();

// Protected endpoint
app.MapGet("/api/profile", (ClaimsPrincipal user) =>
{
    return TypedResults.Ok(new { user.Identity?.Name });
}).RequireAuthorization();

// Public endpoint
app.MapGet("/api/public", () => "Anyone can access!");

app.Run();

class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

// ===== OPTION 2: Custom JWT (More Control) =====
/*
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = "YourSuperSecretKey32CharactersMin!";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MyApp",
            ValidAudience = "MyAppUsers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();  // MUST come BEFORE UseAuthorization!
app.UseAuthorization();

app.MapPost("/login", (LoginRequest req) => {
    if (req.Email == "admin@test.com" && req.Password == "password") {
        var token = new JwtSecurityToken(
            issuer: "MyApp", audience: "MyAppUsers",
            claims: new[] { new Claim(ClaimTypes.Name, req.Email) },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                SecurityAlgorithms.HmacSha256));
        return TypedResults.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    return TypedResults.Unauthorized();
});

app.MapGet("/protected", () => "Secret!").RequireAuthorization();
record LoginRequest(string Email, string Password);
*/

// MapIdentityApi endpoints created:
// POST /register - Create new user
// POST /login - Get access token (use ?useCookies=false for bearer tokens)
// POST /refresh - Refresh access token
// GET /confirmEmail - Confirm email address
// POST /forgotPassword - Initiate password reset
// POST /manage/2fa - Setup two-factor auth
```
