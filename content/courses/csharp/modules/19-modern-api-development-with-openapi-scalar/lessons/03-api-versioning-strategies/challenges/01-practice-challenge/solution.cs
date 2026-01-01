using Asp.Versioning;
using Asp.Versioning.Builder;

var builder = WebApplication.CreateBuilder(args);

// Configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader("version"),
        new HeaderApiVersionReader("X-API-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

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
    new UserV2(2, "Bob", "bob@example.com", "User", DateTime.Parse("2024-03-20")),
    new UserV2(3, "Charlie", "charlie@example.com", "Admin", DateTime.Parse("2024-02-10"))
};

// V1 endpoint group
var v1 = app.NewVersionedApi()
    .MapGroup("/api/v{version:apiVersion}/users")
    .HasApiVersion(new ApiVersion(1, 0));

v1.MapGet("/", () => usersV1)
    .WithName("GetUsersV1")
    .WithTags("Users");

v1.MapGet("/{id}", (int id) =>
{
    var user = usersV1.FirstOrDefault(u => u.Id == id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
})
    .WithName("GetUserByIdV1")
    .WithTags("Users");

// V2 endpoint group (enhanced)
var v2 = app.NewVersionedApi()
    .MapGroup("/api/v{version:apiVersion}/users")
    .HasApiVersion(new ApiVersion(2, 0));

v2.MapGet("/", () => usersV2)
    .WithName("GetUsersV2")
    .WithTags("Users");

v2.MapGet("/{id}", (int id) =>
{
    var user = usersV2.FirstOrDefault(u => u.Id == id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
})
    .WithName("GetUserByIdV2")
    .WithTags("Users");

// NEW in V2: Get admins only
v2.MapGet("/admins", () =>
{
    return usersV2.Where(u => u.Role == "Admin").ToArray();
})
    .WithName("GetAdminUsersV2")
    .WithDescription("Returns only users with Admin role (V2 only)")
    .WithTags("Users");

// Print API version info
Console.WriteLine("=== Available API Versions ===");
Console.WriteLine();
Console.WriteLine("Version 1.0 (Basic):");
Console.WriteLine("  GET /api/v1/users");
Console.WriteLine("  GET /api/v1/users/{id}");
Console.WriteLine();
Console.WriteLine("Version 2.0 (Enhanced):");
Console.WriteLine("  GET /api/v2/users (includes Role, CreatedAt)");
Console.WriteLine("  GET /api/v2/users/{id}");
Console.WriteLine("  GET /api/v2/users/admins (NEW!)");
Console.WriteLine();
Console.WriteLine("Access via:");
Console.WriteLine("  URL: /api/v1/users or /api/v2/users");
Console.WriteLine("  Query: /api/users?version=1.0");
Console.WriteLine("  Header: X-API-Version: 1.0");

app.Run();

public record UserV1(int Id, string Name, string Email);

public record UserV2(int Id, string Name, string Email, string Role, DateTime CreatedAt);