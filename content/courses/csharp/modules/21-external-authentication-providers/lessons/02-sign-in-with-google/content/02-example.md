---
type: "EXAMPLE"
title: "Setting Up Google Authentication"
---

Before writing any code, you need to configure your application in the Google Cloud Console. This creates the OAuth credentials that identify your application to Google.

```csharp
// ===== STEP 1: GOOGLE CLOUD CONSOLE SETUP =====
// 1. Go to https://console.cloud.google.com/
// 2. Create a new project or select existing one
// 3. Navigate to APIs & Services > Credentials
// 4. Click 'Create Credentials' > 'OAuth client ID'
// 5. Configure OAuth consent screen first (if prompted):
//    - Choose 'External' user type for public apps
//    - Add app name, user support email, developer contact
//    - Add scopes: .../auth/userinfo.email, .../auth/userinfo.profile, openid
// 6. Create OAuth client ID:
//    - Application type: 'Web application'
//    - Name: 'ShopFlow Web'
//    - Authorized redirect URIs: 
//      - Development: https://localhost:5001/signin-google
//      - Production: https://shopflow.example.com/signin-google
// 7. Copy the Client ID and Client Secret

// ===== STEP 2: INSTALL NUGET PACKAGES =====
// dotnet add package Microsoft.AspNetCore.Authentication.Google

// ===== STEP 3: CONFIGURE USER SECRETS (Development) =====
// dotnet user-secrets init
// dotnet user-secrets set "Authentication:Google:ClientId" "your-client-id.apps.googleusercontent.com"
// dotnet user-secrets set "Authentication:Google:ClientSecret" "your-client-secret"

// ===== STEP 4: PROGRAM.CS CONFIGURATION =====
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication with multiple schemes
builder.Services.AddAuthentication(options =>
{
    // Cookies store the authenticated session
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // Google handles the actual authentication
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.LogoutPath = "/auth/logout";
    options.AccessDeniedPath = "/auth/access-denied";
    options.Cookie.Name = "ShopFlow.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax; // Required for OAuth redirects
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    // Load credentials securely from configuration
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] 
        ?? throw new InvalidOperationException("Google ClientId not configured");
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] 
        ?? throw new InvalidOperationException("Google ClientSecret not configured");
    
    // Request specific scopes (permissions)
    options.Scope.Clear(); // Remove defaults if you want explicit control
    options.Scope.Add("openid");  // Required for OpenID Connect
    options.Scope.Add("email");   // User's email address
    options.Scope.Add("profile"); // User's name and picture
    
    // Map additional claims from Google's user info
    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
    options.ClaimActions.MapJsonKey("urn:google:picture", "picture");
    options.ClaimActions.MapJsonKey("urn:google:locale", "locale");
    options.ClaimActions.MapJsonKey("urn:google:email_verified", "email_verified");
    
    // Save tokens if you need to call Google APIs later
    options.SaveTokens = true;
    
    // Handle authentication events
    options.Events = new OAuthEvents
    {
        OnCreatingTicket = async context =>
        {
            // This fires after successful authentication
            var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
            var googleId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
            var emailVerified = context.Principal?.FindFirstValue("urn:google:email_verified");
            
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            logger.LogInformation(
                "Google authentication successful for {Email} (ID: {GoogleId}, Verified: {Verified})",
                email, googleId, emailVerified);
            
            // IMPORTANT: Check if email is verified!
            if (emailVerified != "true" && emailVerified != "True")
            {
                logger.LogWarning("User {Email} has unverified email from Google", email);
                // You might want to handle this differently
            }
            
            // Here you would typically:
            // 1. Look up user in your database by Google ID or email
            // 2. Create new user if first time
            // 3. Update last login timestamp
            // 4. Add custom claims based on your business logic
            
            await Task.CompletedTask;
        },
        
        OnRemoteFailure = context =>
        {
            // Handle authentication failures gracefully
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();
            
            logger.LogError(context.Failure, "Google authentication failed");
            
            context.Response.Redirect("/auth/login?error=google_failed");
            context.HandleResponse(); // Prevent default error handling
            
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Login endpoint - initiates Google OAuth flow
app.MapGet("/auth/login", (string? returnUrl) => 
{
    var properties = new AuthenticationProperties 
    { 
        RedirectUri = returnUrl ?? "/",
        // Add extra properties for security
        Items = { { ".xsrf", Guid.NewGuid().ToString() } }
    };
    return Results.Challenge(properties, new[] { GoogleDefaults.AuthenticationScheme });
});

// Logout endpoint
app.MapPost("/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

// User profile endpoint - returns authenticated user info
app.MapGet("/auth/me", (ClaimsPrincipal user) =>
{
    if (!user.Identity?.IsAuthenticated ?? true)
    {
        return Results.Unauthorized();
    }
    
    return Results.Ok(new
    {
        Id = user.FindFirstValue(ClaimTypes.NameIdentifier),
        Email = user.FindFirstValue(ClaimTypes.Email),
        Name = user.FindFirstValue(ClaimTypes.Name),
        GivenName = user.FindFirstValue(ClaimTypes.GivenName),
        FamilyName = user.FindFirstValue(ClaimTypes.Surname),
        Picture = user.FindFirstValue("urn:google:picture"),
        EmailVerified = user.FindFirstValue("urn:google:email_verified") == "true",
        Provider = "Google"
    });
}).RequireAuthorization();

app.Run();
```
