---
type: "THEORY"
title: "Configuring JWT Authentication"
---

## Setting Up JWT Authentication in ASP.NET Core

Configuring JWT authentication requires careful attention to both the service registration and the validation parameters. Let us walk through a production-ready setup.

### Configuration in appsettings.json

```json
{
  "Jwt": {
    "SecretKey": "ThisKeyIsStoredInUserSecretsOrKeyVault_NotHere!",
    "Issuer": "https://shopflow.com",
    "Audience": "https://shopflow.com",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

**Important:** Never store the actual secret key in appsettings.json! Use User Secrets for development and Azure Key Vault or environment variables for production.

### Program.cs Configuration

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Bind settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(JwtSettings.SectionName));

// Register JWT service
builder.Services.AddSingleton<IJwtService, JwtService>();

// Configure authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration
        .GetSection(JwtSettings.SectionName)
        .Get<JwtSettings>()!;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Validate the issuer (who created the token)
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        
        // Validate the audience (who the token is for)
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        
        // Validate the token hasn't expired
        ValidateLifetime = true,
        
        // Validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        
        // No tolerance for expired tokens (default is 5 minutes!)
        ClockSkew = TimeSpan.Zero,
        
        // Ensure the token has an expiration claim
        RequireExpirationTime = true,
        
        // Map standard claim names
        NameClaimType = "name",
        RoleClaimType = ClaimTypes.Role
    };
    
    // Handle authentication events
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            logger.LogWarning(
                "Authentication failed: {Exception}",
                context.Exception.Message);
            
            return Task.CompletedTask;
        },
        
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            var userId = context.Principal?.FindFirst("sub")?.Value;
            logger.LogDebug("Token validated for user: {UserId}", userId);
            
            return Task.CompletedTask;
        },
        
        OnChallenge = context =>
        {
            // Customize the 401 response
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            
            var result = JsonSerializer.Serialize(new
            {
                error = "Unauthorized",
                message = "Valid JWT token required"
            });
            
            return context.Response.WriteAsync(result);
        }
    };
});
```

### TokenValidationParameters Explained

| Parameter | Purpose | Recommendation |
|-----------|---------|----------------|
| ValidateIssuer | Check the 'iss' claim matches expected issuer | Always true |
| ValidateAudience | Check the 'aud' claim matches expected audience | Always true |
| ValidateLifetime | Check the token hasn't expired | Always true |
| ValidateIssuerSigningKey | Verify the signature is valid | Always true |
| ClockSkew | Tolerance for expiration time differences | Set to Zero |
| RequireExpirationTime | Reject tokens without 'exp' claim | True |
| NameClaimType | Which claim to use for User.Identity.Name | Custom claim name |
| RoleClaimType | Which claim to use for role-based auth | ClaimTypes.Role |

### Protecting Endpoints

Once JWT authentication is configured, protect your endpoints:

```csharp
// Require authentication for all routes in a group
var api = app.MapGroup("/api")
    .RequireAuthorization();

// Or per-endpoint
app.MapGet("/api/profile", GetProfile)
    .RequireAuthorization();

// With specific policy
app.MapDelete("/api/products/{id}", DeleteProduct)
    .RequireAuthorization("AdminOnly");

// Allow anonymous access
app.MapPost("/api/token", GenerateToken)
    .AllowAnonymous();
```

The JWT is passed in the Authorization header: `Authorization: Bearer eyJhbGciOiJIUzI1NiIs...`