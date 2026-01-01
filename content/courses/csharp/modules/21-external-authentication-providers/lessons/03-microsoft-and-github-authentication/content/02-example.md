---
type: "EXAMPLE"
title: "Adding Microsoft Authentication"
---

Microsoft authentication is commonly used in enterprise environments where organizations use Azure Active Directory. Here is how to configure it for ShopFlow.

```csharp
// ===== AZURE AD APP REGISTRATION =====
// 1. Go to https://portal.azure.com/
// 2. Navigate to Azure Active Directory > App registrations
// 3. Click 'New registration'
// 4. Configure:
//    - Name: 'ShopFlow'
//    - Supported account types: Choose based on your needs:
//      - 'Single tenant': Only users from your organization
//      - 'Multi-tenant': Users from any Azure AD organization
//      - 'Multi-tenant + personal': Azure AD + personal Microsoft accounts
//    - Redirect URI: Web > https://localhost:5001/signin-microsoft
// 5. Note the Application (client) ID
// 6. Go to 'Certificates & secrets' > 'New client secret'
// 7. Note the secret value (only shown once!)

// ===== NUGET PACKAGE =====
// dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount

// ===== PROGRAM.CS =====
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
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
.AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]
        ?? throw new InvalidOperationException("Microsoft ClientId not configured");
    options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]
        ?? throw new InvalidOperationException("Microsoft ClientSecret not configured");
    
    // For multi-tenant apps, this allows any Azure AD tenant
    // For single-tenant, use your specific tenant ID
    options.AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
    options.TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
    
    // Request specific scopes
    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.Scope.Add("openid");
    
    // Save tokens if you need to call Microsoft Graph API
    options.SaveTokens = true;
    
    // Map additional claims
    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "givenName");
    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "surname");
    options.ClaimActions.MapJsonKey("urn:microsoft:picture", "picture");
    
    options.Events.OnCreatingTicket = async context =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();
        
        var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
        var tenantId = context.Principal?.FindFirstValue(
            "http://schemas.microsoft.com/identity/claims/tenantid");
        
        logger.LogInformation(
            "Microsoft authentication for {Email} from tenant {TenantId}",
            email, tenantId ?? "personal");
        
        // For enterprise apps, you might validate tenant ID
        // to ensure only your organization's users can access
        
        await Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Login with Microsoft
app.MapGet("/auth/login/microsoft", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    new[] { MicrosoftAccountDefaults.AuthenticationScheme }));

app.Run();

// ===== APPSETTINGS.JSON (Development only - use secrets in production!) =====
// {
//   "Authentication": {
//     "Microsoft": {
//       "ClientId": "your-application-client-id",
//       "ClientSecret": "your-client-secret-value"
//     }
//   }
// }
```
