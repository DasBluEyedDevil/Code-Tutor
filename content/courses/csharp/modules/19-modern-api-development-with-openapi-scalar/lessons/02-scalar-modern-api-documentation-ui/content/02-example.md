---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== SCALAR: MODERN API DOCUMENTATION =====
// Install: dotnet add package Scalar.AspNetCore

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// .NET 9 built-in OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose OpenAPI spec (required for Scalar)
app.MapOpenApi();

// Add Scalar UI - beautiful API documentation!
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("My Bookstore API")
        .WithTheme(ScalarTheme.Purple)  // Built-in themes!
        .WithDarkMode(true)              // Dark mode by default
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithPreferredScheme("Bearer");  // JWT auth hint
});

// ===== SAMPLE API ENDPOINTS =====

var products = new List<Product>
{
    new(1, "Laptop Pro", 1299.99m, "Electronics"),
    new(2, "Wireless Mouse", 49.99m, "Electronics"),
    new(3, "Desk Lamp", 39.99m, "Home Office")
};

app.MapGet("/products", () => products)
    .WithName("GetProducts")
    .WithDescription("Returns all available products")
    .WithTags("Products")
    .WithSummary("List all products")
    .Produces<List<Product>>(StatusCodes.Status200OK);

app.MapGet("/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null 
        ? Results.Ok(product) 
        : Results.NotFound();
})
    .WithName("GetProductById")
    .WithDescription("Returns a product by its unique identifier")
    .WithTags("Products")
    .WithSummary("Get product by ID")
    .Produces<Product>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.MapGet("/products/search", (string? category, decimal? minPrice) =>
{
    var results = products.AsEnumerable();
    
    if (!string.IsNullOrEmpty(category))
        results = results.Where(p => p.Category == category);
    if (minPrice.HasValue)
        results = results.Where(p => p.Price >= minPrice);
    
    return results.ToList();
})
    .WithName("SearchProducts")
    .WithDescription("Search products by category and/or minimum price")
    .WithTags("Products")
    .WithSummary("Search products")
    .Produces<List<Product>>(StatusCodes.Status200OK);

app.MapPost("/products", (CreateProductRequest request) =>
{
    var product = new Product(
        products.Max(p => p.Id) + 1,
        request.Name,
        request.Price,
        request.Category
    );
    products.Add(product);
    return Results.Created($"/products/{product.Id}", product);
})
    .WithName("CreateProduct")
    .WithDescription("Creates a new product in the catalog")
    .WithTags("Products")
    .WithSummary("Create new product")
    .Accepts<CreateProductRequest>("application/json")
    .Produces<Product>(StatusCodes.Status201Created);

Console.WriteLine("API Documentation available at: /scalar/v1");
app.Run();

// ===== MODELS =====

public record Product(int Id, string Name, decimal Price, string Category);

public record CreateProductRequest(string Name, decimal Price, string Category);

// ===== SCALAR THEMES =====
// ScalarTheme.Default   - Clean light theme
// ScalarTheme.Purple    - Purple accent
// ScalarTheme.Solarized - Solarized colors
// ScalarTheme.BluePlanet - Blue accent
// ScalarTheme.Saturn    - Dark with orange
// ScalarTheme.Kepler    - Minimal design
// ScalarTheme.Mars      - Red accent
// ScalarTheme.DeepSpace - Dark mode

// ===== CODE GENERATION TARGETS =====
// ScalarTarget.CSharp, .JavaScript, .Python, .Curl, .Go, .Ruby, etc.
// Users see ready-to-copy code in their preferred language!
```
