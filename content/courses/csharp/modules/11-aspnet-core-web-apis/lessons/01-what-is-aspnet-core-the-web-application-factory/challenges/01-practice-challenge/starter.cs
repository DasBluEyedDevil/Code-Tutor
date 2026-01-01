using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint 1: Root
app.MapGet("/", () => /* return value */);

// Endpoint 2: Current time
app.MapGet("/api/time", () => 
{
    // Return DateTime.Now as anonymous object
});

// Endpoint 3: Greet with name parameter
app.MapGet("/api/greet/{name}", (string name) => 
{
    // Return greeting
});

// Endpoint 4: Add two numbers from query
app.MapGet("/api/math/add", (int a, int b) => 
{
    // Return sum
});

// Endpoint 5: Product list
app.MapGet("/api/products", () => 
{
    // Return array of products
});

Console.WriteLine("API endpoints created!");
Console.WriteLine("Endpoints available:");
Console.WriteLine("  GET /");
Console.WriteLine("  GET /api/time");
Console.WriteLine("  GET /api/greet/{name}");
Console.WriteLine("  GET /api/math/add?a=5&b=3");
Console.WriteLine("  GET /api/products");