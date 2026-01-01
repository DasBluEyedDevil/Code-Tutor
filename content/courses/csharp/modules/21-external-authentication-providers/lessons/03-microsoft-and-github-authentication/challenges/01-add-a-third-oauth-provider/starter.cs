using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
// TODO: Add Twitter authentication using statement
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
// TODO: Add Twitter authentication
// Configure:
// - ClientId from Configuration["Authentication:Twitter:ClientId"]
// - ClientSecret from Configuration["Authentication:Twitter:ClientSecret"]
// - Add scopes: users.read, tweet.read
// - SaveTokens = true
// - Map claims: username -> urn:twitter:username
// - Map claims: profile_image_url -> urn:twitter:avatar
// - OnCreatingTicket: log the username and add provider claim

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// TODO: Add GET /auth/login/twitter endpoint

app.Run();