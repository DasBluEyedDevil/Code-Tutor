using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add JWT authentication
// builder.Services.AddAuthentication(...)

builder.Services.AddAuthorization();

var app = builder.Build();

// TODO: Add middleware (correct order!)
// app.UseAuthentication();
// app.UseAuthorization();

// Login endpoint
app.MapPost("/login", (LoginRequest request) =>
{
    // TODO: Validate and issue token
    return Results.Unauthorized();
});

record LoginRequest(string Username, string Password);

// Public endpoint
app.MapGet("/api/public", () => "Public data");

// Protected endpoint
app.MapGet("/api/protected", (ClaimsPrincipal user) =>
{
    // TODO: Return user info
    return Results.Ok();
}); // TODO: .RequireAuthorization()

// Admin endpoint
app.MapGet("/api/admin", () => "Admin only!");
// TODO: .RequireAuthorization(policy => policy.RequireRole("Admin"))

Console.WriteLine("Auth endpoints defined!");