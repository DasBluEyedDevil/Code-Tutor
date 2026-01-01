using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/forbidden";
    options.ExpireTimeSpan = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "ShopFlow",
        ValidAudience = "ShopFlowAPI",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
    
    options.AddPolicy("CanManageProducts", policy =>
        policy.RequireClaim("Permission", "Products.Manage"));
});

var app = builder.Build();

// Middleware in correct order
app.UseAuthentication();
app.UseAuthorization();

// Public endpoint
app.MapGet("/", () => "Welcome to ShopFlow!")
    .AllowAnonymous();

// Protected endpoint
app.MapGet("/dashboard", (HttpContext ctx) =>
    $"Dashboard for {ctx.User.Identity?.Name ?? "User"}")
    .RequireAuthorization();

// Admin-only endpoint
app.MapDelete("/products/{id}", (int id) =>
    Results.Ok($"Deleted product {id}"))
    .RequireAuthorization("AdminOnly");

app.Run();