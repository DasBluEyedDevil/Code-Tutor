using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", opt => opt.LoginPath = "/login");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireRole("Admin"));
    
    options.AddPolicy("CanViewProducts", policy =>
        policy.RequireAuthenticatedUser());
    
    options.AddPolicy("CanEditProducts", policy =>
        policy.RequireRole("Admin", "Manager"));
    
    options.AddPolicy("CanDeleteProducts", policy =>
        policy.RequireRole("Admin")
              .RequireClaim("can_delete", "true"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/products", () => "Product list")
    .RequireAuthorization("CanViewProducts");

app.MapPut("/products/{id}", (int id) => $"Updated product {id}")
    .RequireAuthorization("CanEditProducts");

app.MapDelete("/products/{id}", (int id) => $"Deleted product {id}")
    .RequireAuthorization("CanDeleteProducts");

app.MapGet("/admin/settings", () => "Admin Settings")
    .RequireAuthorization("RequireAdmin");

app.Run();