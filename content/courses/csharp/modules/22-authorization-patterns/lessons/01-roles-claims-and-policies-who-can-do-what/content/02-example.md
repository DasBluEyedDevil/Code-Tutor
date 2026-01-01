---
type: "EXAMPLE"
title: "Role-Based and Policy-Based Authorization"
---

This example demonstrates different authorization strategies in ASP.NET Core, from simple role checks to flexible policy-based authorization.

```csharp
// ===== PROGRAM.CS - AUTHORIZATION CONFIGURATION =====

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", options => options.LoginPath = "/login");

// Configure Authorization Policies
builder.Services.AddAuthorization(options =>
{
    // Simple role-based policy
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireRole("Admin"));
    
    // Policy requiring specific claim
    options.AddPolicy("RequireVerifiedEmail", policy =>
        policy.RequireClaim("email_verified", "true"));
    
    // Policy with multiple requirements (AND logic)
    options.AddPolicy("CanManageProducts", policy =>
        policy.RequireRole("Admin", "Manager")  // Admin OR Manager
              .RequireClaim("department", "Products", "Inventory"));  // AND in Products/Inventory dept
    
    // Policy with custom assertion
    options.AddPolicy("CanEditProducts", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            
            // Admins can always edit
            if (user.IsInRole("Admin"))
                return true;
            
            // Managers can edit during business hours
            if (user.IsInRole("Manager"))
            {
                var hour = DateTime.Now.Hour;
                return hour >= 9 && hour < 17; // 9 AM to 5 PM
            }
            
            return false;
        }));
    
    // Age verification policy
    options.AddPolicy("Over18", policy =>
        policy.RequireAssertion(context =>
        {
            var birthDateClaim = context.User.FindFirst("birth_date")?.Value;
            if (DateTime.TryParse(birthDateClaim, out var birthDate))
            {
                var age = DateTime.Today.Year - birthDate.Year;
                if (birthDate > DateTime.Today.AddYears(-age)) age--;
                return age >= 18;
            }
            return false;
        }));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// ===== SIMPLE ROLE-BASED AUTHORIZATION =====

// Only users in Admin role can access
app.MapGet("/admin/dashboard", () => "Admin Dashboard")
    .RequireAuthorization("RequireAdmin");

// Multiple roles - user must be in at least one
app.MapGet("/management/reports", [Authorize(Roles = "Admin,Manager")] () => 
    "Management Reports");

// ===== POLICY-BASED AUTHORIZATION =====

app.MapGet("/products/manage", () => "Product Management")
    .RequireAuthorization("CanManageProducts");

app.MapPut("/products/{id}", (int id) => $"Updating product {id}")
    .RequireAuthorization("CanEditProducts");

// ===== CLAIM-BASED AUTHORIZATION =====

app.MapGet("/verified-only", () => "Verified Users Area")
    .RequireAuthorization("RequireVerifiedEmail");

app.MapGet("/adult-content", () => "Age-Restricted Content")
    .RequireAuthorization("Over18");

// ===== READING CLAIMS IN HANDLERS =====

app.MapGet("/my-claims", (ClaimsPrincipal user) =>
{
    var claims = user.Claims.Select(c => new { c.Type, c.Value });
    return Results.Ok(new
    {
        IsAuthenticated = user.Identity?.IsAuthenticated,
        Name = user.Identity?.Name,
        Roles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value),
        AllClaims = claims
    });
}).RequireAuthorization();

// Fake login for testing - assigns different roles
app.MapGet("/login/{role}", async (string role, HttpContext ctx) =>
{
    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, $"TestUser_{role}"),
        new(ClaimTypes.Email, $"{role.ToLower()}@shopflow.com"),
        new(ClaimTypes.Role, role),
        new("email_verified", "true"),
        new("department", "Products"),
        new("birth_date", "1990-01-15")
    };
    
    var identity = new ClaimsIdentity(claims, "Cookie");
    var principal = new ClaimsPrincipal(identity);
    
    await ctx.SignInAsync("Cookie", principal);
    return Results.Ok($"Logged in as {role}");
});

app.Run();
```
