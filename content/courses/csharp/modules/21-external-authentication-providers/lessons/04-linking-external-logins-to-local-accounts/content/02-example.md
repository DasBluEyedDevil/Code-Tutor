---
type: "EXAMPLE"
title: "Linking Accounts Flow"
---

This example demonstrates two common scenarios: a user with password authentication adding Google login, and a user authenticated via Google adding password-based login.

```csharp
// ===== EXTERNAL LOGINS TABLE (Entity Framework) =====
public class ExternalLogin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Provider { get; set; } = default!;  // "Google", "Microsoft", "GitHub"
    public string ProviderKey { get; set; } = default!;  // User's ID at the provider
    public string? ProviderDisplayName { get; set; }  // Friendly name for UI
    public DateTime CreatedAt { get; set; }
    
    public User User { get; set; } = default!;
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string? PasswordHash { get; set; }  // Null if user only uses external login
    public string DisplayName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    
    public ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();
}

// ===== ACCOUNT LINKING ENDPOINTS =====
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public static class AccountLinkingEndpoints
{
    public static void MapAccountLinkingEndpoints(this WebApplication app)
    {
        // Get currently linked external logins
        app.MapGet("/api/account/external-logins", async (
            ClaimsPrincipal user,
            ShopFlowDbContext db) =>
        {
            var userId = int.Parse(user.FindFirstValue("user_id")!);
            
            var logins = await db.ExternalLogins
                .Where(el => el.UserId == userId)
                .Select(el => new
                {
                    el.Provider,
                    el.ProviderDisplayName,
                    LinkedAt = el.CreatedAt
                })
                .ToListAsync();
            
            return Results.Ok(logins);
        }).RequireAuthorization();
        
        // Initiate linking a new external provider
        app.MapGet("/api/account/link/{provider}", (
            string provider,
            HttpContext context) =>
        {
            // Validate provider is supported
            var validProviders = new[] { "Google", "Microsoft", "GitHub" };
            if (!validProviders.Contains(provider, StringComparer.OrdinalIgnoreCase))
            {
                return Results.BadRequest($"Unsupported provider: {provider}");
            }
            
            // Generate a linking token to track this specific linking attempt
            var linkingToken = Guid.NewGuid().ToString();
            context.Session.SetString("LinkingToken", linkingToken);
            context.Session.SetString("LinkingProvider", provider);
            
            var properties = new AuthenticationProperties
            {
                RedirectUri = $"/api/account/link-callback?token={linkingToken}",
                Items = { { "LoginProvider", provider } }
            };
            
            // Challenge with the external provider
            // User is already logged in, this just adds a new external login
            return Results.Challenge(properties, new[] { $"{provider}" });
        }).RequireAuthorization();
        
        // Callback after external authentication for linking
        app.MapGet("/api/account/link-callback", async (
            string token,
            HttpContext context,
            ShopFlowDbContext db,
            ILogger<Program> logger) =>
        {
            // Verify the linking token matches
            var storedToken = context.Session.GetString("LinkingToken");
            var provider = context.Session.GetString("LinkingProvider");
            
            if (storedToken != token || string.IsNullOrEmpty(provider))
            {
                logger.LogWarning("Invalid linking token or missing provider");
                return Results.BadRequest("Invalid linking request");
            }
            
            // Get the current user's ID (they must be authenticated)
            var currentUserId = int.Parse(
                context.User.FindFirstValue("user_id")
                ?? throw new InvalidOperationException("User not authenticated"));
            
            // Get the external authentication result
            var authenticateResult = await context.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded)
            {
                logger.LogWarning("External authentication failed during linking");
                return Results.Redirect("/account/settings?error=link_failed");
            }
            
            var externalPrincipal = authenticateResult.Principal!;
            var providerKey = externalPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var email = externalPrincipal.FindFirstValue(ClaimTypes.Email);
            var name = externalPrincipal.FindFirstValue(ClaimTypes.Name) ?? email;
            
            // Check if this external login is already linked to ANY account
            var existingLogin = await db.ExternalLogins
                .FirstOrDefaultAsync(el => 
                    el.Provider == provider && 
                    el.ProviderKey == providerKey);
            
            if (existingLogin != null)
            {
                if (existingLogin.UserId == currentUserId)
                {
                    // Already linked to this account
                    return Results.Redirect("/account/settings?info=already_linked");
                }
                else
                {
                    // Linked to a different account!
                    logger.LogWarning(
                        "Attempted to link {Provider}:{Key} to user {UserId}, " +
                        "but already linked to user {ExistingUserId}",
                        provider, providerKey, currentUserId, existingLogin.UserId);
                    
                    return Results.Redirect("/account/settings?error=already_linked_other");
                }
            }
            
            // Create the new external login link
            var newLogin = new ExternalLogin
            {
                UserId = currentUserId,
                Provider = provider,
                ProviderKey = providerKey,
                ProviderDisplayName = name,
                CreatedAt = DateTime.UtcNow
            };
            
            db.ExternalLogins.Add(newLogin);
            await db.SaveChangesAsync();
            
            logger.LogInformation(
                "User {UserId} linked {Provider} account ({Key})",
                currentUserId, provider, providerKey);
            
            // Clean up session
            context.Session.Remove("LinkingToken");
            context.Session.Remove("LinkingProvider");
            
            return Results.Redirect("/account/settings?success=provider_linked");
        }).RequireAuthorization();
        
        // Add password to an account that only has external login
        app.MapPost("/api/account/add-password", async (
            AddPasswordRequest request,
            ClaimsPrincipal user,
            ShopFlowDbContext db,
            IPasswordHasher passwordHasher) =>
        {
            var userId = int.Parse(user.FindFirstValue("user_id")!);
            
            var dbUser = await db.Users.FindAsync(userId);
            if (dbUser == null)
            {
                return Results.NotFound();
            }
            
            // Only allow adding password if user doesn't have one
            if (!string.IsNullOrEmpty(dbUser.PasswordHash))
            {
                return Results.BadRequest("Account already has a password. Use change-password instead.");
            }
            
            // Validate password meets requirements
            if (request.Password.Length < 8)
            {
                return Results.BadRequest("Password must be at least 8 characters.");
            }
            
            if (request.Password != request.ConfirmPassword)
            {
                return Results.BadRequest("Passwords do not match.");
            }
            
            // Hash and store password
            dbUser.PasswordHash = passwordHasher.HashPassword(request.Password);
            await db.SaveChangesAsync();
            
            return Results.Ok(new { Message = "Password added successfully" });
        }).RequireAuthorization();
    }
}

public record AddPasswordRequest(string Password, string ConfirmPassword);
```
