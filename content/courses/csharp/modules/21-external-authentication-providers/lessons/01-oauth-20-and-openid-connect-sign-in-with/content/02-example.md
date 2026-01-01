---
type: "EXAMPLE"
title: "Adding Google Authentication"
---

This example shows how to configure Google authentication in your ASP.NET Core application. You will need to create credentials in the Google Cloud Console first.

```csharp
// ===== PROGRAM.CS - GOOGLE AUTHENTICATION SETUP =====

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Step 1: Configure Authentication with Google
builder.Services.AddAuthentication(options =>
{
    // Use cookies as the default scheme for web apps
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // Use Google when authentication is challenged
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    // Load credentials from configuration (never hardcode!)
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    
    // Request additional scopes beyond basic profile
    options.Scope.Add("email");
    options.Scope.Add("profile");
    
    // Map claims from Google to your application
    options.ClaimActions.MapJsonKey("picture", "picture");
    options.ClaimActions.MapJsonKey("locale", "locale");
    
    // Save tokens if you need to call Google APIs later
    options.SaveTokens = true;
    
    // Handle events during authentication
    options.Events.OnCreatingTicket = async context =>
    {
        // Access user info from Google
        var email = context.Principal?.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
        var name = context.Principal?.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
        
        // Log the successful authentication
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("User {Email} ({Name}) authenticated via Google", email, name);
        
        // You could also:
        // - Create/update user in your database
        // - Add custom claims based on your business logic
        // - Check if user is allowed to access your app
        
        await Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Login endpoint - redirects to Google
app.MapGet("/login", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    new[] { GoogleDefaults.AuthenticationScheme }
));

// Logout endpoint
app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

// Protected endpoint showing user info
app.MapGet("/profile", (HttpContext ctx) =>
{
    var user = ctx.User;
    return Results.Ok(new
    {
        Name = user.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value,
        Email = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
        Picture = user.FindFirst("picture")?.Value,
        IsAuthenticated = user.Identity?.IsAuthenticated ?? false
    });
}).RequireAuthorization();

app.MapGet("/", (HttpContext ctx) =>
{
    if (ctx.User.Identity?.IsAuthenticated == true)
    {
        return Results.Ok($"Welcome, {ctx.User.Identity.Name}! Visit /profile for more info.");
    }
    return Results.Ok("Welcome! Visit /login to sign in with Google.");
});

app.Run();

// ===== APPSETTINGS.JSON =====
// {
//   "Authentication": {
//     "Google": {
//       "ClientId": "your-client-id.apps.googleusercontent.com",
//       "ClientSecret": "your-client-secret"
//     }
//   }
// }
//
// For production, use User Secrets or Azure Key Vault:
// dotnet user-secrets set "Authentication:Google:ClientId" "your-client-id"
// dotnet user-secrets set "Authentication:Google:ClientSecret" "your-client-secret"
```
