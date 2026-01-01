---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== .NET 9 BUILT-IN OPENAPI SUPPORT =====
// No Swashbuckle needed!

using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services - that's it!
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose OpenAPI document at /openapi/v1.json
app.MapOpenApi();

// ===== WELL-DOCUMENTED ENDPOINTS =====

// Simple GET with metadata
app.MapGet("/products", () => 
{
    return new[]
    {
        new Product(1, "Laptop", 999.99m),
        new Product(2, "Mouse", 29.99m)
    };
})
.WithName("GetProducts")
.WithDescription("Returns all available products in the catalog")
.WithTags("Products")
.Produces<Product[]>(StatusCodes.Status200OK);

// GET with path parameter
app.MapGet("/products/{id}", (int id) =>
{
    if (id <= 0) return Results.BadRequest("Invalid ID");
    return Results.Ok(new Product(id, "Sample Product", 49.99m));
})
.WithName("GetProductById")
.WithDescription("Returns a specific product by its unique identifier")
.WithTags("Products")
.Produces<Product>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

// POST with request body
app.MapPost("/products", (CreateProductRequest request) =>
{
    var product = new Product(Random.Shared.Next(1000), request.Name, request.Price);
    return Results.Created($"/products/{product.Id}", product);
})
.WithName("CreateProduct")
.WithDescription("Creates a new product in the catalog")
.WithTags("Products")
.Accepts<CreateProductRequest>("application/json")
.Produces<Product>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

// PUT with path parameter and body
app.MapPut("/products/{id}", (int id, UpdateProductRequest request) =>
{
    return Results.Ok(new Product(id, request.Name, request.Price));
})
.WithName("UpdateProduct")
.WithDescription("Updates an existing product")
.WithTags("Products")
.Accepts<UpdateProductRequest>("application/json")
.Produces<Product>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// DELETE endpoint
app.MapDelete("/products/{id}", (int id) =>
{
    return Results.NoContent();
})
.WithName("DeleteProduct")
.WithDescription("Removes a product from the catalog")
.WithTags("Products")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.Run();

// ===== DATA MODELS =====

public record Product(int Id, string Name, decimal Price);

public record CreateProductRequest(string Name, decimal Price);

public record UpdateProductRequest(string Name, decimal Price);

// ===== OPENAPI OUTPUT (openapi/v1.json) =====
// {
//   "openapi": "3.0.1",
//   "info": { "title": "MyApi", "version": "1.0" },
//   "paths": {
//     "/products": {
//       "get": {
//         "operationId": "GetProducts",
//         "description": "Returns all available products...",
//         "tags": ["Products"],
//         "responses": { "200": { ... } }
//       }
//     }
//   }
// }
```
