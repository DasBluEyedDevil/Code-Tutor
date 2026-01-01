using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 }
};
int nextId = 2;

app.MapGet("/api/products", () => Results.Ok(products));

app.MapGet("/api/products/{id}", (int id) =>
{
    // Find and return 200 or 404
});

app.MapPost("/api/products", (Product product) =>
{
    // Validate Name
    // Validate Price
    // Validate Stock
    // If valid, add and return 201
});

app.MapPut("/api/products/{id}", (int id, Product updated) =>
{
    // Find product (404 if not found)
    // Validate input (400 if invalid)
    // Update and return 200
});

app.MapDelete("/api/products/{id}", (int id) =>
{
    // Find and delete, return 204 or 404
});

Console.WriteLine("Product API with proper status codes!");