using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure Authentication
// - DefaultScheme = Cookies
// - DefaultChallengeScheme = Google

// TODO: Add Cookie authentication
// - LoginPath = "/login"
// - Cookie.HttpOnly = true
// - ExpireTimeSpan = 7 days

// TODO: Add Google authentication
// - ClientId from Configuration["Auth:Google:ClientId"]
// - ClientSecret from Configuration["Auth:Google:ClientSecret"]
// - Add scopes: email, profile
// - SaveTokens = true

var app = builder.Build();

// TODO: Add middleware

// TODO: GET /login - Challenge with Google, redirect to /profile

// TODO: GET /profile (protected) - Return user's Name and Email claims

app.Run();