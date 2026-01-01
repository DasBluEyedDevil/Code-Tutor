---
type: "EXAMPLE"
title: "Working with Claims"
---

This example demonstrates adding custom claims to users, reading claims in your application, and using claims within authorization policies.

```csharp
// ===== ADDING CUSTOM CLAIMS TO USERS =====
// Claims are added during registration, login, or by admins

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public async Task<AuthResult> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
        
        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
            return AuthResult.Failed(createResult.Errors);
        
        // Add custom claims during registration
        var claims = new List<Claim>
        {
            new Claim("full_name", $"{dto.FirstName} {dto.LastName}"),
            new Claim("registration_date", DateTime.UtcNow.ToString("O")),
            new Claim("account_tier", "free"),  // Premium, free, trial
            new Claim("email_verified", "false")
        };
        
        await _userManager.AddClaimsAsync(user, claims);
        
        return AuthResult.Success(user);
    }
    
    // Upgrade user to premium tier
    public async Task UpgradeToPremiumAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;
        
        // Remove old tier claim and add new one
        var oldClaims = await _userManager.GetClaimsAsync(user);
        var tierClaim = oldClaims.FirstOrDefault(c => c.Type == "account_tier");
        
        if (tierClaim != null)
        {
            await _userManager.RemoveClaimAsync(user, tierClaim);
        }
        
        await _userManager.AddClaimAsync(user, 
            new Claim("account_tier", "premium"));
        
        // Add premium-specific permissions
        await _userManager.AddClaimsAsync(user, new[]
        {
            new Claim("feature_access", "analytics"),
            new Claim("feature_access", "bulk_upload"),
            new Claim("feature_access", "priority_support"),
            new Claim("max_products", "1000")
        });
    }
    
    // Grant specific permission to a user
    public async Task GrantPermissionAsync(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;
        
        // Check if user already has this permission
        var claims = await _userManager.GetClaimsAsync(user);
        if (claims.Any(c => c.Type == "permission" && c.Value == permission))
            return; // Already has it
        
        await _userManager.AddClaimAsync(user,
            new Claim("permission", permission));
    }
}

// ===== READING CLAIMS IN YOUR APPLICATION =====
// Access claims through the ClaimsPrincipal (User property)

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public ActionResult<ProfileDto> GetProfile()
    {
        // User is the ClaimsPrincipal from the JWT
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var fullName = User.FindFirstValue("full_name");
        var accountTier = User.FindFirstValue("account_tier") ?? "free";
        
        // Get all feature access claims (there may be multiple)
        var features = User.Claims
            .Where(c => c.Type == "feature_access")
            .Select(c => c.Value)
            .ToList();
        
        // Get all permissions
        var permissions = User.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .ToList();
        
        // Check for specific claim
        var isEmailVerified = User.HasClaim("email_verified", "true");
        var isPremium = User.HasClaim("account_tier", "premium");
        
        return Ok(new ProfileDto
        {
            UserId = userId!,
            Email = email!,
            FullName = fullName ?? "Unknown",
            AccountTier = accountTier,
            Features = features,
            Permissions = permissions,
            IsEmailVerified = isEmailVerified,
            IsPremium = isPremium
        });
    }
}

// ===== INCLUDING CLAIMS IN JWT =====
// Claims must be added to the token for stateless authorization

public class JwtTokenService : ITokenService
{
    public async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        // Get all claims from the database
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            // Standard claims
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("full_name", $"{user.FirstName} {user.LastName}")
        };
        
        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        // Add custom claims from database
        // Be selective - don't bloat the token with unnecessary claims
        var relevantClaimTypes = new[] 
        { 
            "account_tier", "email_verified", "permission", 
            "feature_access", "max_products" 
        };
        
        foreach (var claim in userClaims)
        {
            if (relevantClaimTypes.Contains(claim.Type))
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }
        }
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// ===== CLAIMS IN POLICIES =====
// Configure policies that require specific claims

// In Program.cs:
builder.Services.AddAuthorization(options =>
{
    // Require email verification
    options.AddPolicy("RequireVerifiedEmail", policy =>
        policy.RequireClaim("email_verified", "true"));
    
    // Require premium subscription
    options.AddPolicy("RequirePremium", policy =>
        policy.RequireClaim("account_tier", "premium"));
    
    // Require specific feature access
    options.AddPolicy("CanAccessAnalytics", policy =>
        policy.RequireClaim("feature_access", "analytics"));
    
    // Combine multiple claim requirements
    options.AddPolicy("VerifiedPremiumUser", policy =>
        policy.RequireClaim("email_verified", "true")
              .RequireClaim("account_tier", "premium"));
    
    // Require any of multiple claim values
    options.AddPolicy("PaidUser", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => 
                c.Type == "account_tier" && 
                (c.Value == "premium" || c.Value == "enterprise"))));
});

// Applying policies to endpoints:
[HttpGet("analytics")]
[Authorize(Policy = "CanAccessAnalytics")]
public ActionResult<AnalyticsDto> GetAnalytics()
{
    // Only users with feature_access:analytics claim can access
    return Ok(_analyticsService.GetDashboard());
}

[HttpPost("bulk-upload")]
[Authorize(Policy = "VerifiedPremiumUser")]
public async Task<ActionResult> BulkUpload(BulkUploadDto dto)
{
    // Requires both verified email AND premium tier
    await _productService.BulkImportAsync(dto);
    return Ok();
}
```
