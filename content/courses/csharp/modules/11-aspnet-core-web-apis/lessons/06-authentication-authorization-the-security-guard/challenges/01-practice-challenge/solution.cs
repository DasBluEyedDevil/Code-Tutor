using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = "SuperSecretKey12345678901234567890"; // 32+ chars for HS256
var jwtIssuer = "MyApp";
var jwtAudience = "MyAppUsers";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", (LoginRequest request) =>
{
    if (request.Username == "admin" && request.Password == "password")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("UserId", "1")
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
        
        return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    return Results.Unauthorized();
});

record LoginRequest(string Username, string Password);

app.MapGet("/api/public", () => "Public data - anyone can see!");

app.MapGet("/api/protected", (ClaimsPrincipal user) =>
{
    var name = user.Identity?.Name;
    var userId = user.FindFirst("UserId")?.Value;
    return Results.Ok(new { message = $"Hello {name}!", userId });
}).RequireAuthorization();

app.MapGet("/api/admin", () => "Admin dashboard data!")
    .RequireAuthorization(policy => policy.RequireRole("Admin"));

Console.WriteLine("JWT Auth API configured!");
Console.WriteLine("POST /login → Get token");
Console.WriteLine("GET /api/public → No auth needed");
Console.WriteLine("GET /api/protected → Auth required");
Console.WriteLine("GET /api/admin → Admin role required");