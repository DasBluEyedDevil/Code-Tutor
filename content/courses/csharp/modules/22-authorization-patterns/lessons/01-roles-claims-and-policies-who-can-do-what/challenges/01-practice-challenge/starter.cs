using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", opt => opt.LoginPath = "/login");

// TODO: Configure authorization policies:
// - RequireAdmin: Admin role
// - CanViewProducts: Any authenticated user
// - CanEditProducts: Admin OR Manager
// - CanDeleteProducts: Admin AND has claim 'can_delete' = 'true'

var app = builder.Build();

// TODO: Add middleware

// TODO: GET /products - CanViewProducts

// TODO: PUT /products/{id} - CanEditProducts

// TODO: DELETE /products/{id} - CanDeleteProducts

// TODO: GET /admin/settings - RequireAdmin

app.Run();