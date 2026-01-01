using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.Twitter;
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
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
})
.AddTwitter(TwitterAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Twitter:ClientId"]
        ?? throw new InvalidOperationException("Twitter ClientId not configured");
    options.ClientSecret = builder.Configuration["Authentication:Twitter:ClientSecret"]
        ?? throw new InvalidOperationException("Twitter ClientSecret not configured");
    
    options.Scope.Add("users.read");
    options.Scope.Add("tweet.read");
    
    options.SaveTokens = true;
    
    options.ClaimActions.MapJsonKey("urn:twitter:username", "username");
    options.ClaimActions.MapJsonKey("urn:twitter:avatar", "profile_image_url");
    
    options.Events.OnCreatingTicket = async context =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();
        
        var username = context.Principal?.FindFirstValue("urn:twitter:username");
        logger.LogInformation("Twitter authentication for @{Username}", username);
        
        var identity = context.Principal?.Identity as ClaimsIdentity;
        identity?.AddClaim(new Claim("auth_provider", "Twitter"));
        
        await Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/auth/login/twitter", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    new[] { TwitterAuthenticationDefaults.AuthenticationScheme }));

app.Run();