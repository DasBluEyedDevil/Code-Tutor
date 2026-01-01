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
    new Product { Name = "Shirt", Price = 25, Category = "Clothing", Stock = 50 },
    new Product { Name = "Novel", Price = 15, Category = "Books", Stock = 0 },
    new Product { Name = "Headphones", Price = 79, Category = "Electronics", Stock = 20 },
    new Product { Name = "Jeans", Price = 45, Category = "Clothing", Stock = 30 },
    new Product { Name = "Tablet", Price = 299, Category = "Electronics", Stock = 0 }
};

var affordable = products.Where(p => p.Price < 50);
Console.WriteLine("Products under $50:");
foreach (var p in affordable)
{
    Console.WriteLine("- " + p.Name + ": $" + p.Price);
}

var availableElectronics = products.Where(p => p.Category == "Electronics" && p.Stock > 0);
Console.WriteLine("\nElectronics in stock:");
foreach (var p in availableElectronics)
{
    Console.WriteLine("- " + p.Name + " (" + p.Stock + " units)");
}

var booksOrExpensive = products.Where(p => p.Category == "Books" || p.Price > 200);
Console.WriteLine("\nBooks OR price > $200:");
foreach (var p in booksOrExpensive)
{
    Console.WriteLine("- " + p.Name);
}

var outOfStock = products.Where(p => p.Stock == 0);
Console.WriteLine("\nOut of stock:");
foreach (var p in outOfStock)
{
    Console.WriteLine("- " + p.Name);
}