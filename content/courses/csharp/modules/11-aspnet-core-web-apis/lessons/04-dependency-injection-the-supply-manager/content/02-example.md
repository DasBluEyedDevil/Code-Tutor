---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// STEP 1: Define interface (contract)
interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
}

// STEP 2: Implement interface
class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m },
        new Product { Id = 2, Name = "Mouse", Price = 29.99m }
    };
    
    public List<Product> GetAll() => _products;
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    public void Add(Product product) => _products.Add(product);
}

class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

// STEP 3: Register service with DI container
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var app = builder.Build();

// STEP 4: Inject into endpoint handlers
app.MapGet("/api/products", (IProductRepository repo) =>
{
    return repo.GetAll();  // DI provides the repo!
});

app.MapGet("/api/products/{id}", (int id, IProductRepository repo) =>
{
    var product = repo.GetById(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/api/products", (Product product, IProductRepository repo) =>
{
    repo.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.Run();

// DI automatically provides IProductRepository to ALL endpoints!
// Same instance shared across all requests (Singleton lifetime)
```
