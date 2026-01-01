using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Create sample data
var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 999.99m },
    // Add 4 more
};

// Simulate saving to file
Console.WriteLine("=== SIMULATING FILE STORAGE ===");
foreach (var p in products)
{
    Console.WriteLine($"Saving to file: {p.Id},{p.Name},{p.Price}");
}

// Simulate searching (inefficient!)
Console.WriteLine("\n=== SEARCHING FOR PRODUCT ID 3 ===");
Console.WriteLine("Reading entire file...");

int searchId = 3;
int checked = 0;
Product? found = null;

foreach (var p in products)
{
    checked++;
    if (p.Id == searchId)
    {
        found = p;
        break;
    }
}

Console.WriteLine($"Checked {checked} products");
if (found != null)
    Console.WriteLine($"Found: {found.Name}");

// Explain problems
Console.WriteLine("\n=== WHY THIS IS BAD ===");
Console.WriteLine("Problem 1: With 1 million products, searching reads ALL 1 million!");
Console.WriteLine("Problem 2: Two users writing at once = file corruption!");
Console.WriteLine("Problem 3: No relationships (can't link products to orders easily)");
Console.WriteLine("\nSOLUTION: Use a DATABASE!");