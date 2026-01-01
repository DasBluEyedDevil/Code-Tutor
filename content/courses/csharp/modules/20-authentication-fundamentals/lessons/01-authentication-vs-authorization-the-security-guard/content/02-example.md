---
type: "EXAMPLE"
title: "Auth in ASP.NET Core"
---

This example shows the complete authentication and authorization setup in an ASP.NET Core application.

```csharp
// ===== COMPLETE AUTH SETUP IN PROGRAM.CS =====

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== STEP 1: CONFIGURE AUTHENTICATION =====
// Define HOW users prove their identity

builder.Services.AddAuthentication(options =>
{
    // Default scheme for web app (cookies)
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // Challenge scheme for APIs (JWT)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Cookie authentication for web browsers
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;           // Prevent XSS access
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
    options.Cookie.SameSite = SameSiteMode.Strict;           // CSRF protection
})
// JWT authentication for APIs
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        ),
        ClockSkew = TimeSpan.Zero  // No tolerance for expired tokens
    };
});

// ===== STEP 2: CONFIGURE AUTHORIZATION =====
// Define WHAT authenticated users can do

builder.Services.AddAuthorization(options =>
{
    // Role-based policies
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("Admin"));
    
    options.AddPolicy("ManagerOrAdmin", policy => 
        policy.RequireRole("Manager", "Admin"));
    
    // Claim-based policies
    options.AddPolicy("CanEditProducts", policy => 
        policy.RequireClaim("Permission", "Products.Edit"));
    
    options.AddPolicy("PremiumUser", policy => 
        policy.RequireClaim("Subscription", "Premium", "Enterprise"));
    
    // Complex policy with multiple requirements
    options.AddPolicy("CanDeleteProducts", policy => policy
        .RequireRole("Admin")
        .RequireClaim("Permission", "Products.Delete")
        .RequireAuthenticatedUser());
});

var app = builder.Build();

// ===== STEP 3: ADD MIDDLEWARE IN CORRECT ORDER =====
// Order matters! Authentication before Authorization

app.UseRouting();

app.UseAuthentication();  // Step 1: Who are you?
app.UseAuthorization();   // Step 2: What can you do?

// ===== STEP 4: PROTECT YOUR ENDPOINTS =====

// Public - anyone can access
app.MapGet("/", () => "Welcome to ShopFlow!")
    .AllowAnonymous();

// Protected - must be authenticated
app.MapGet("/profile", (HttpContext ctx) => 
    $"Hello, {ctx.User.Identity?.Name}!")
    .RequireAuthorization();

// Role-restricted - must be Admin
app.MapDelete("/products/{id}", (int id) => 
    Results.Ok($"Deleted product {id}"))
    .RequireAuthorization("AdminOnly");

// Policy-restricted - must have specific claim
app.MapPut("/products/{id}", (int id) => 
    Results.Ok($"Updated product {id}"))
    .RequireAuthorization("CanEditProducts");

app.Run();
```
