---
type: "EXAMPLE"
title: "Adding GitHub Authentication"
---

GitHub authentication is popular for developer-focused applications. GitHub uses OAuth 2.0 but not OpenID Connect, so the claims structure is slightly different.

```csharp
// ===== GITHUB OAUTH APP SETUP =====
// 1. Go to https://github.com/settings/developers
// 2. Click 'OAuth Apps' > 'New OAuth App'
// 3. Configure:
//    - Application name: 'ShopFlow'
//    - Homepage URL: https://shopflow.example.com
//    - Authorization callback URL: https://localhost:5001/signin-github
// 4. Click 'Register application'
// 5. Note the Client ID
// 6. Click 'Generate a new client secret'
// 7. Note the secret (only shown once!)

// ===== NUGET PACKAGE =====
// dotnet add package AspNet.Security.OAuth.GitHub
// Note: This is a community package as there's no official Microsoft package

// ===== PROGRAM.CS WITH GITHUB =====
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.GitHub;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.Cookie.Name = "ShopFlow.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
})
.AddGitHub(GitHubAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"]
        ?? throw new InvalidOperationException("GitHub ClientId not configured");
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"]
        ?? throw new InvalidOperationException("GitHub ClientSecret not configured");
    
    // GitHub-specific scopes
    options.Scope.Add("user:email");  // Access user's email (even if private)
    options.Scope.Add("read:user");   // Access user profile info
    
    // Optional: Access user's repositories (if needed for your app)
    // options.Scope.Add("repo");
    
    options.SaveTokens = true;
    
    // Map GitHub-specific claims
    options.ClaimActions.MapJsonKey("urn:github:login", "login");  // GitHub username
    options.ClaimActions.MapJsonKey("urn:github:url", "html_url"); // GitHub profile URL
    options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");
    options.ClaimActions.MapJsonKey("urn:github:company", "company");
    options.ClaimActions.MapJsonKey("urn:github:bio", "bio");
    
    options.Events.OnCreatingTicket = async context =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();
        
        var githubUsername = context.Principal?.FindFirstValue("urn:github:login");
        var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
        
        logger.LogInformation(
            "GitHub authentication for @{Username} ({Email})",
            githubUsername, email ?? "no email");
        
        // Note: GitHub email might be null if user has private email
        // You may need to fetch it via the API using the access token
        if (string.IsNullOrEmpty(email) && context.AccessToken != null)
        {
            // Fetch primary email from GitHub API
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.AccessToken}");
            client.DefaultRequestHeaders.Add("User-Agent", "ShopFlow");
            
            try
            {
                var response = await client.GetAsync("https://api.github.com/user/emails");
                if (response.IsSuccessStatusCode)
                {
                    var emails = await response.Content
                        .ReadFromJsonAsync<List<GitHubEmail>>();
                    var primaryEmail = emails?.FirstOrDefault(e => e.Primary && e.Verified);
                    
                    if (primaryEmail != null)
                    {
                        // Add email claim manually
                        var identity = context.Principal?.Identity as ClaimsIdentity;
                        identity?.AddClaim(new Claim(ClaimTypes.Email, primaryEmail.Email));
                        
                        logger.LogInformation(
                            "Retrieved primary email {Email} from GitHub API",
                            primaryEmail.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to fetch email from GitHub API");
            }
        }
    };
});

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Login with GitHub
app.MapGet("/auth/login/github", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    new[] { GitHubAuthenticationDefaults.AuthenticationScheme }));

app.Run();

// Helper class for deserializing GitHub email response
public record GitHubEmail(
    string Email,
    bool Primary,
    bool Verified,
    string? Visibility);
```
