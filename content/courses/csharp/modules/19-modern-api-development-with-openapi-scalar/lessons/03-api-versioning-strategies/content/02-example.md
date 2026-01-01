---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== API VERSIONING IN .NET 9 =====
// Install: dotnet add package Asp.Versioning.Http

using Asp.Versioning;
using Asp.Versioning.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add API versioning services
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;  // Adds api-supported-versions header
    
    // Support multiple versioning schemes
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),           // /api/v1/
        new QueryStringApiVersionReader("version"), // ?version=1.0
        new HeaderApiVersionReader("X-API-Version") // X-API-Version: 1.0
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";  // v1, v2, etc.
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

// ===== VERSION 1 ENDPOINTS =====
var v1 = app.NewVersionedApi()
    .MapGroup("/api/v{version:apiVersion}/products")
    .HasApiVersion(new ApiVersion(1, 0));

v1.MapGet("/", () =>
{
    // V1: Simple product list
    return new[]
    {
        new ProductV1(1, "Laptop", 999.99m),
        new ProductV1(2, "Mouse", 29.99m)
    };
})
    .WithName("GetProductsV1")
    .WithTags("Products");

v1.MapGet("/{id}", (int id) =>
{
    return new ProductV1(id, "Sample Product", 49.99m);
})
    .WithName("GetProductByIdV1")
    .WithTags("Products");

// ===== VERSION 2 ENDPOINTS (Enhanced) =====
var v2 = app.NewVersionedApi()
    .MapGroup("/api/v{version:apiVersion}/products")
    .HasApiVersion(new ApiVersion(2, 0));

v2.MapGet("/", () =>
{
    // V2: Enhanced product with more fields
    return new[]
    {
        new ProductV2(1, "Laptop", 999.99m, "Electronics", 50, 4.5),
        new ProductV2(2, "Mouse", 29.99m, "Accessories", 200, 4.8)
    };
})
    .WithName("GetProductsV2")
    .WithTags("Products");

v2.MapGet("/{id}", (int id) =>
{
    return new ProductV2(id, "Sample Product", 49.99m, "General", 100, 4.0);
})
    .WithName("GetProductByIdV2")
    .WithTags("Products");

// Search only available in V2
v2.MapGet("/search", (string? category, decimal? minPrice) =>
{
    return new[] { new ProductV2(1, "Found Item", minPrice ?? 0, category ?? "All", 10, 4.0) };
})
    .WithName("SearchProductsV2")
    .WithTags("Products");

Console.WriteLine("API Versions:");
Console.WriteLine("  V1: /api/v1/products (basic)");
Console.WriteLine("  V2: /api/v2/products (enhanced + search)");
Console.WriteLine();
Console.WriteLine("Version can be specified via:");
Console.WriteLine("  URL: /api/v1/products");
Console.WriteLine("  Query: /api/products?version=1.0");
Console.WriteLine("  Header: X-API-Version: 1.0");

app.Run();

// ===== VERSION-SPECIFIC MODELS =====

// V1: Basic product
public record ProductV1(int Id, string Name, decimal Price);

// V2: Enhanced with category, stock, rating
public record ProductV2(
    int Id, 
    string Name, 
    decimal Price, 
    string Category, 
    int StockCount, 
    double Rating
);
```
