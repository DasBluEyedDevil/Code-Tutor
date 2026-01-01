using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add authentication and authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

// Configure OpenAPI with security schemes
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT token from /auth/login endpoint"
            },
            ["ApiKey"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-API-Key",
                Description = "API key for service accounts"
            }
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();

// ===== PUBLIC ENDPOINTS =====

app.MapGet("/health", () => new { Status = "Healthy", Time = DateTime.UtcNow })
    .WithName("HealthCheck")
    .WithTags("System")
    .WithDescription("Public health check - no authentication required")
    .Produces<object>(StatusCodes.Status200OK)
    .AllowAnonymous();

app.MapPost("/auth/login", (LoginRequest request) =>
{
    // Validate and return token (simplified)
    return Results.Ok(new { Token = "jwt.token.here", ExpiresIn = 3600 });
})
    .WithName("Login")
    .WithTags("Authentication")
    .WithDescription("Authenticate to receive JWT token")
    .Accepts<LoginRequest>("application/json")
    .Produces<object>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .AllowAnonymous();

// ===== PROTECTED ENDPOINTS =====

var orders = new List<Order>
{
    new(1, "user@example.com", 99.99m, "Pending"),
    new(2, "user@example.com", 149.99m, "Shipped")
};

app.MapGet("/orders", () => orders)
    .WithName("GetUserOrders")
    .WithTags("Orders")
    .WithDescription("Get current user's orders. Requires Bearer token.")
    .Produces<List<Order>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .RequireAuthorization();

app.MapPost("/orders", (CreateOrderRequest request) =>
{
    var order = new Order(orders.Count + 1, "user@example.com", request.Amount, "Pending");
    orders.Add(order);
    return Results.Created($"/orders/{order.Id}", order);
})
    .WithName("CreateOrder")
    .WithTags("Orders")
    .WithDescription("Create new order. Requires Bearer token.")
    .Accepts<CreateOrderRequest>("application/json")
    .Produces<Order>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status401Unauthorized)
    .RequireAuthorization();

// ===== ADMIN ENDPOINTS =====

app.MapGet("/admin/orders", () => orders)
    .WithName("GetAllOrders")
    .WithTags("Admin")
    .WithDescription("Get ALL orders. Admin role required.")
    .Produces<List<Order>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status403Forbidden)
    .RequireAuthorization("Admin");

app.MapDelete("/admin/orders/{id}", (int id) =>
{
    var order = orders.FirstOrDefault(o => o.Id == id);
    if (order is null) return Results.NotFound();
    orders.Remove(order);
    return Results.NoContent();
})
    .WithName("DeleteOrder")
    .WithTags("Admin")
    .WithDescription("Delete any order. Admin role required.")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status403Forbidden)
    .RequireAuthorization("Admin");

// Print security summary
Console.WriteLine("=== API Security Configuration ===");
Console.WriteLine();
Console.WriteLine("Security Schemes:");
Console.WriteLine("  - Bearer: JWT token in Authorization header");
Console.WriteLine("  - ApiKey: X-API-Key header");
Console.WriteLine();
Console.WriteLine("Endpoint Security:");
Console.WriteLine("  PUBLIC (no auth):");
Console.WriteLine("    GET  /health");
Console.WriteLine("    POST /auth/login");
Console.WriteLine();
Console.WriteLine("  PROTECTED (Bearer token):");
Console.WriteLine("    GET  /orders");
Console.WriteLine("    POST /orders");
Console.WriteLine();
Console.WriteLine("  ADMIN (Bearer + Admin role):");
Console.WriteLine("    GET    /admin/orders");
Console.WriteLine("    DELETE /admin/orders/{id}");

app.Run();

public record LoginRequest(string Username, string Password);
public record Order(int Id, string UserEmail, decimal Amount, string Status);
public record CreateOrderRequest(decimal Amount);