using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Name;
    public decimal Price;
    public string Category;
    public int Stock;
}

List<Product> products = new List<Product>
{
    new Product { Name = "Laptop", Price = 999, Category = "Electronics", Stock = 5 },
    new Product { Name = "Mouse", Price = 25, Category = "Electronics", Stock = 50 },
    new Product { Name = "Keyboard", Price = 75, Category = "Electronics", Stock = 30 },
    new Product { Name = "Monitor", Price = 299, Category = "Electronics", Stock = 0 },
    new Product { Name = "Webcam", Price = 89, Category = "Electronics", Stock = 15 }
};

var productNames = products.Select(p => p.Name);
Console.WriteLine("Product names: " + string.Join(", ", productNames));

var discountedPrices = products.Select(p => p.Price * 0.9m);
Console.WriteLine("\nDiscounted prices: $" + string.Join(", $", discountedPrices));

var summaries = products.Select(p => new
{
    Name = p.Name,
    Category = p.Category,
    TotalValue = p.Price * p.Stock,
    InStock = p.Stock > 0
});

Console.WriteLine("\nProduct summaries:");
foreach (var s in summaries)
{
    Console.WriteLine(s.Name + " (" + s.Category + ") - Value: $" + s.TotalValue + " - In stock: " + s.InStock);
}

var affordableNames = products
    .Where(p => p.Price < 100)
    .Select(p => p.Name);

Console.WriteLine("\nAffordable products: " + string.Join(", ", affordableNames));