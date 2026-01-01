---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== AOT-COMPATIBLE MINIMAL API =====
// Project file settings:
// <PublishAot>true</PublishAot>
// <InvariantGlobalization>true</InvariantGlobalization>

using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// Configure JSON with source generator (required for AOT!)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonContext.Default);
});

var app = builder.Build();

// ===== ENDPOINTS =====

// Simple GET
app.MapGet("/", () => "Hello, AOT World!");

// GET with typed response
app.MapGet("/health", () => new HealthStatus("Healthy", DateTime.UtcNow));

// GET with route parameter
app.MapGet("/products/{id}", (int id) =>
{
    var product = new Product(id, $"Product {id}", 19.99m);
    return Results.Ok(product);
});

// GET all products
app.MapGet("/products", () =>
{
    var products = new List<Product>
    {
        new(1, "Widget", 29.99m),
        new(2, "Gadget", 49.99m),
        new(3, "Gizmo", 39.99m)
    };
    return Results.Ok(products);
});

// POST with typed body
app.MapPost("/products", (Product product) =>
{
    // In real app, save to database
    Console.WriteLine($"Created: {product.Name}");
    return Results.Created($"/products/{product.Id}", product);
});

// PUT with route param and body
app.MapPut("/products/{id}", (int id, Product product) =>
{
    if (id != product.Id)
        return Results.BadRequest("ID mismatch");
    
    Console.WriteLine($"Updated: {product.Name}");
    return Results.Ok(product);
});

// DELETE
app.MapDelete("/products/{id}", (int id) =>
{
    Console.WriteLine($"Deleted product {id}");
    return Results.NoContent();
});

// ===== QUERY PARAMETERS =====
app.MapGet("/search", (string? query, int page = 1, int size = 10) =>
{
    var result = new SearchResult(query ?? "", page, size, Array.Empty<Product>());
    return Results.Ok(result);
});

app.Run();

// ===== MODELS =====
public record Product(int Id, string Name, decimal Price);
public record HealthStatus(string Status, DateTime CheckedAt);
public record SearchResult(string Query, int Page, int Size, Product[] Results);

// ===== JSON SOURCE GENERATOR (Required for AOT!) =====
[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
[JsonSerializable(typeof(Product[]))]
[JsonSerializable(typeof(HealthStatus))]
[JsonSerializable(typeof(SearchResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class AppJsonContext : JsonSerializerContext { }

// ===== OUTPUT =====
Console.WriteLine("Minimal API with AOT support!");
Console.WriteLine("Endpoints:");
Console.WriteLine("  GET  / - Hello world");
Console.WriteLine("  GET  /health - Health check");
Console.WriteLine("  GET  /products - List all");
Console.WriteLine("  GET  /products/{id} - Get one");
Console.WriteLine("  POST /products - Create");
Console.WriteLine("  PUT  /products/{id} - Update");
Console.WriteLine("  DELETE /products/{id} - Delete");
Console.WriteLine("\nPublish: dotnet publish -c Release -r linux-x64");
```
