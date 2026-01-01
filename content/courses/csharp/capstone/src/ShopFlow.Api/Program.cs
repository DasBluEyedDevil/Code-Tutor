using ShopFlow.Api.Endpoints;
using ShopFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI/Swagger services
builder.Services.AddOpenApi();

// Configure Infrastructure services (DbContext, repositories, handlers)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=shopflow.db";
builder.Services.AddInfrastructure(connectionString);

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

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithOpenApi();

// Map Product API endpoints
app.MapProductEndpoints();

app.Run();

// Make Program class accessible for integration tests
public partial class Program { }
