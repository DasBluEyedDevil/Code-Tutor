---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// MINIMAL API in ASP.NET Core 9 (.NET 9)
// File: Program.cs

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI support (.NET 9)
builder.Services.AddOpenApi();

var app = builder.Build();

// Map OpenAPI endpoint
app.MapOpenApi();

// ENDPOINT 1: Simple GET request
app.MapGet("/", () => "Hello from ASP.NET Core 9!");

// ENDPOINT 2: GET with route parameter
app.MapGet("/hello/{name}", (string name) => 
{
    return $"Hello, {name}!";
});

// ENDPOINT 3: Returning JSON object
app.MapGet("/api/user", () => 
{
    return new { Id = 1, Name = "Alice", Email = "alice@example.com" };
});

// ENDPOINT 4: List of data
app.MapGet("/api/products", () => 
{
    var products = new[]
    {
        new { Id = 1, Name = "Laptop", Price = 999.99 },
        new { Id = 2, Name = "Mouse", Price = 29.99 },
        new { Id = 3, Name = "Keyboard", Price = 79.99 }
    };
    return products;
});

// ENDPOINT 5: Query parameters with TypedResults (.NET 7+)
// TypedResults provides compile-time type safety and auto OpenAPI metadata!
app.MapGet("/api/search", (string? query) => 
{
    if (string.IsNullOrEmpty(query))
        return TypedResults.BadRequest("Query parameter required!");
    
    return TypedResults.Ok($"Searching for: {query}");
});

app.Run();  // Start the web server!

// Access in browser:
// http://localhost:5000/
// http://localhost:5000/hello/Bob
// http://localhost:5000/api/user
// http://localhost:5000/api/products
// http://localhost:5000/api/search?query=laptop
// http://localhost:5000/openapi/v1.json (API docs!)
```
