using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShopFlow.Api.Endpoints;
using ShopFlow.Infrastructure;
using ShopFlow.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI/Swagger services
builder.Services.AddOpenApi();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
var secretKey = jwtSettings?.SecretKey ?? builder.Configuration["JwtSettings:SecretKey"] ?? "DefaultSecretKeyForDevelopmentOnly32Chars!";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer ?? "ShopFlow",
        ValidAudience = jwtSettings?.Audience ?? "ShopFlowUsers",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero // Remove default 5-minute tolerance
    };
});

builder.Services.AddAuthorization();

// Configure Infrastructure services (DbContext, repositories, handlers)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=shopflow.db";
builder.Services.AddInfrastructure(builder.Configuration, connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Initialize database in development (but not during testing)
    if (!app.Environment.EnvironmentName.Equals("Testing", StringComparison.OrdinalIgnoreCase))
    {
        DependencyInjection.InitializeDatabase(app.Services);
    }
}

app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithOpenApi();

// Map Auth API endpoints (no authentication required)
app.MapAuthEndpoints();

// Map Product API endpoints
app.MapProductEndpoints();

// Map Cart API endpoints (requires authentication)
app.MapCartEndpoints();

// Map Order API endpoints (requires authentication)
app.MapOrderEndpoints();

app.Run();

// Make Program class accessible for integration tests
public partial class Program { }
