// TODO: Add using statement for Asp.Versioning

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure API versioning
// - DefaultApiVersion: 1.0
// - AssumeDefaultVersionWhenUnspecified: true
// - ReportApiVersions: true
// - ApiVersionReader: Combine URL, Query, Header readers

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

// Sample data
var usersV1 = new[]
{
    new UserV1(1, "Alice", "alice@example.com"),
    new UserV1(2, "Bob", "bob@example.com")
};

var usersV2 = new[]
{
    new UserV2(1, "Alice", "alice@example.com", "Admin", DateTime.Parse("2024-01-15")),
    new UserV2(2, "Bob", "bob@example.com", "User", DateTime.Parse("2024-03-20"))
};

// TODO: Create V1 endpoint group at /api/v{version:apiVersion}/users
// - GET / - return usersV1
// - GET /{id} - return user by ID

// TODO: Create V2 endpoint group
// - GET / - return usersV2
// - GET /{id} - return user by ID (V2)
// - GET /admins - return only admin users (new in V2!)

// TODO: Print API version info
Console.WriteLine("Available API Versions:");
// Print how to access each version

app.Run();

// TODO: Define UserV1 record (Id, Name, Email)
// TODO: Define UserV2 record (Id, Name, Email, Role, CreatedAt)