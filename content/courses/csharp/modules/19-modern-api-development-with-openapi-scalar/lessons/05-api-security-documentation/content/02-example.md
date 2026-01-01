---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== API SECURITY DOCUMENTATION =====

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://myapi.com",
            ValidAudience = "https://myapi.com",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("YourSuperSecretKeyHere32Chars!!"))
        };
    });

builder.Services.AddAuthorization();

// Configure OpenAPI with security schemes
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Define JWT Bearer security scheme
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your JWT token. Example: eyJhbGciOiJIUzI1NiIs..."
            },
            ["ApiKey"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-API-Key",
                Description = "API key for server-to-server communication"
            }
        };

        // Apply Bearer auth globally (can be overridden per-endpoint)
        document.SecurityRequirements = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                }] = Array.Empty<string>()
            }
        };

        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();

// ===== PUBLIC ENDPOINTS (No Auth) =====

app.MapGet("/health", () => new { Status = "Healthy", Timestamp = DateTime.UtcNow })
    .WithName("HealthCheck")
    .WithTags("System")
    .WithDescription("Public health check endpoint - no authentication required")
    .AllowAnonymous();  // Explicitly public

app.MapPost("/auth/login", (LoginRequest request) =>
{
    // In real app: validate credentials, generate JWT
    return Results.Ok(new
    {
        Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
        ExpiresIn = 3600
    });
})
    .WithName("Login")
    .WithTags("Authentication")
    .WithDescription("Authenticate with username/password to receive JWT token")
    .AllowAnonymous();

// ===== PROTECTED ENDPOINTS =====

app.MapGet("/products", () =>
{
    return new[]
    {
        new Product(1, "Laptop", 999.99m),
        new Product(2, "Mouse", 29.99m)
    };
})
    .WithName("GetProducts")
    .WithTags("Products")
    .WithDescription("Returns all products. Requires Bearer token authentication.")
    .RequireAuthorization();  // Requires valid JWT

app.MapGet("/admin/users", () =>
{
    return new[] { new User(1, "admin@example.com", "Admin") };
})
    .WithName("GetUsers")
    .WithTags("Admin")
    .WithDescription("Admin only endpoint. Requires Bearer token with 'admin' role.")
    .RequireAuthorization("AdminPolicy");  // Requires specific policy

Console.WriteLine("Security configured:");
Console.WriteLine("  Public: /health, /auth/login");
Console.WriteLine("  Protected: /products (Bearer token)");
Console.WriteLine("  Admin: /admin/users (Bearer + admin role)");

app.Run();

// ===== MODELS =====

public record LoginRequest(string Username, string Password);
public record Product(int Id, string Name, decimal Price);
public record User(int Id, string Email, string Role);
```
