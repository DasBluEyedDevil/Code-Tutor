---
type: "EXAMPLE"
title: "Generating JWTs in ShopFlow"
---

This example shows a complete JWT generation service for ShopFlow, including configuration, token creation, and the login endpoint that issues tokens.

```csharp
// ===== JWT SETTINGS =====
// ShopFlow.Application/Common/Settings/JwtSettings.cs

namespace ShopFlow.Application.Common.Settings;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    
    /// <summary>
    /// Secret key for signing tokens. Must be at least 256 bits (32 characters).
    /// Store in environment variables or secret manager in production!
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Token issuer - typically your application URL or name.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;
    
    /// <summary>
    /// Token audience - who the token is intended for.
    /// </summary>
    public string Audience { get; set; } = string.Empty;
    
    /// <summary>
    /// Access token lifetime in minutes. Keep short (15-60 minutes).
    /// </summary>
    public int AccessTokenExpirationMinutes { get; set; } = 15;
    
    /// <summary>
    /// Refresh token lifetime in days.
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 7;
}

// ===== JWT SERVICE INTERFACE =====
// ShopFlow.Application/Common/Interfaces/IJwtService.cs

using System.Security.Claims;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}

// ===== JWT SERVICE IMPLEMENTATION =====
// ShopFlow.Infrastructure/Services/JwtService.cs

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Application.Common.Settings;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;
    private readonly SymmetricSecurityKey _signingKey;
    
    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        
        // Validate key length (256 bits minimum for HS256)
        if (_settings.SecretKey.Length < 32)
        {
            throw new InvalidOperationException(
                "JWT secret key must be at least 32 characters (256 bits)");
        }
        
        _signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_settings.SecretKey));
    }
    
    public string GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            // Standard claims
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, 
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            
            // Custom claims
            new("name", user.FullName),
            new("email_verified", user.EmailConfirmed.ToString().ToLower()),
        };
        
        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var credentials = new SigningCredentials(
            _signingKey,
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        // Generate cryptographically random token
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    
    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = _signingKey,
                ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
            }, out _);
            
            return principal;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
    }
}

// ===== TOKEN ENDPOINT =====
// ShopFlow.Api/Endpoints/TokenEndpoints.cs

using Microsoft.AspNetCore.Identity;
using ShopFlow.Application.Common.Interfaces;
using ShopFlow.Domain.Entities;

namespace ShopFlow.Api.Endpoints;

public static class TokenEndpoints
{
    public static void MapTokenEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/token").WithTags("Token");
        
        group.MapPost("/", GenerateToken)
            .WithName("GenerateToken")
            .WithSummary("Authenticate and receive JWT tokens")
            .Produces<TokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .AllowAnonymous();
    }
    
    private static async Task<IResult> GenerateToken(
        TokenRequest request,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService,
        ILogger<Program> logger)
    {
        // Find user
        var user = await userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
        {
            logger.LogWarning("Token request for unknown email: {Email}", request.Email);
            return Results.Unauthorized();
        }
        
        // Verify password (without creating a cookie session)
        var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
        
        if (!passwordValid)
        {
            // Increment failed access count for lockout
            await userManager.AccessFailedAsync(user);
            
            logger.LogWarning("Invalid password for user: {UserId}", user.Id);
            return Results.Unauthorized();
        }
        
        // Check if locked out
        if (await userManager.IsLockedOutAsync(user))
        {
            logger.LogWarning("Token request for locked account: {UserId}", user.Id);
            return Results.Problem(
                statusCode: StatusCodes.Status423Locked,
                title: "Account Locked",
                detail: "Too many failed attempts. Try again later.");
        }
        
        // Check email confirmed
        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return Results.Problem(
                statusCode: StatusCodes.Status403Forbidden,
                title: "Email Not Confirmed",
                detail: "Please confirm your email before requesting tokens.");
        }
        
        // Reset access failed count on successful authentication
        await userManager.ResetAccessFailedCountAsync(user);
        
        // Get roles
        var roles = await userManager.GetRolesAsync(user);
        
        // Generate tokens
        var accessToken = jwtService.GenerateAccessToken(user, roles);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        // TODO: Store refresh token in database (covered in next lesson)
        
        logger.LogInformation("Tokens generated for user: {UserId}", user.Id);
        
        return Results.Ok(new TokenResponse(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: 900, // 15 minutes in seconds
            TokenType: "Bearer"
        ));
    }
}

public record TokenRequest(string Email, string Password);

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn,
    string TokenType
);
```
