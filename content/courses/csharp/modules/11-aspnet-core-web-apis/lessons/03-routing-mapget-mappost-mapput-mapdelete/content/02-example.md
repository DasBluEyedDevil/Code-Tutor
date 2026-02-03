---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 999.99m },
    new Product { Id = 2, Name = "Mouse", Price = 29.99m }
};

int nextId = 3;

// GET - Read all
app.MapGet("/api/products", () => products);

// GET - Read one (TypedResults for type safety + OpenAPI docs)
app.MapGet("/api/products/{id}", Results<Ok<Product>, NotFound> (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null ? TypedResults.Ok(product) : TypedResults.NotFound();
});

// POST - Create new
app.MapPost("/api/products", (Product product) =>
{
    product.Id = nextId++;
    products.Add(product);
    return TypedResults.Created($"/api/products/{product.Id}", product);
});

// PUT - Update existing
app.MapPut("/api/products/{id}", Results<Ok<Product>, NotFound> (int id, Product updatedProduct) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return TypedResults.NotFound();

    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    return TypedResults.Ok(product);
});

// DELETE - Remove
app.MapDelete("/api/products/{id}", Results<NoContent, NotFound> (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return TypedResults.NotFound();

    products.Remove(product);
    return TypedResults.NoContent();
});

app.Run();
```
