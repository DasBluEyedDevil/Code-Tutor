using Microsoft.AspNetCore.Builder;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to my API!");

app.MapGet("/api/time", () => 
{
    return new { CurrentTime = DateTime.Now };
});

app.MapGet("/api/greet/{name}", (string name) => 
{
    return $"Hello, {name}! Welcome to the API.";
});

app.MapGet("/api/math/add", (int a, int b) => 
{
    return new { A = a, B = b, Sum = a + b };
});

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

Console.WriteLine("API endpoints created!");
Console.WriteLine("Endpoints available:");
Console.WriteLine("  GET /");
Console.WriteLine("  GET /api/time");
Console.WriteLine("  GET /api/greet/Bob");
Console.WriteLine("  GET /api/math/add?a=5&b=3");
Console.WriteLine("  GET /api/products");