using Microsoft.AspNetCore.Builder;
using System;
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
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/api/products", (Product product) =>
{
    if (string.IsNullOrEmpty(product.Name))
        return Results.BadRequest("Product name is required!");
    
    if (product.Price <= 0)
        return Results.BadRequest("Price must be greater than 0!");
    
    if (product.Stock < 0)
        return Results.BadRequest("Stock cannot be negative!");
    
    product.Id = nextId++;
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", (int id, Product updated) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return Results.NotFound();
    
    if (string.IsNullOrEmpty(updated.Name))
        return Results.BadRequest("Product name is required!");
    
    if (updated.Price <= 0)
        return Results.BadRequest("Price must be greater than 0!");
    
    if (updated.Stock < 0)
        return Results.BadRequest("Stock cannot be negative!");
    
    product.Name = updated.Name;
    product.Price = updated.Price;
    product.Stock = updated.Stock;
    
    return Results.Ok(product);
});

app.MapDelete("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return Results.NotFound();
    
    products.Remove(product);
    return Results.NoContent();
});

Console.WriteLine("Product API with proper status codes!");
Console.WriteLine("Returns: 200 OK, 201 Created, 204 No Content, 400 Bad Request, 404 Not Found");