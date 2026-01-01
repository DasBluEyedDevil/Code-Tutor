using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 999.99m },
    new Product { Id = 2, Name = "Mouse", Price = 29.99m },
    new Product { Id = 3, Name = "Keyboard", Price = 79.99m },
    new Product { Id = 4, Name = "Monitor", Price = 299.99m },
    new Product { Id = 5, Name = "Webcam", Price = 89.99m }
};

Console.WriteLine("=== SIMULATING FILE STORAGE ===");
foreach (var p in products)
{
    Console.WriteLine($"Saving to file: {p.Id},{p.Name},{p.Price}");
}

Console.WriteLine("\n=== SEARCHING FOR PRODUCT ID 3 ===");
Console.WriteLine("Reading entire file...");

int searchId = 3;
int checked = 0;
Product? found = null;

foreach (var p in products)
{
    checked++;
    Console.WriteLine($"Checking product {p.Id}...");
    if (p.Id == searchId)
    {
        found = p;
        break;
    }
}

Console.WriteLine($"\nChecked {checked} products to find ID {searchId}");
if (found != null)
    Console.WriteLine($"Found: {found.Name}");

Console.WriteLine("\n=== WHY FILE STORAGE IS BAD ===");
Console.WriteLine("❌ Problem 1: Slow - Must read entire file for every search!");
Console.WriteLine("❌ Problem 2: Not scalable - 1 million products = 1 million lines to scan!");
Console.WriteLine("❌ Problem 3: Corruption - Concurrent writes can corrupt the file!");
Console.WriteLine("❌ Problem 4: No relationships - Can't easily link products to orders!");
Console.WriteLine("❌ Problem 5: No validation - Can't enforce 'email must be unique'!");
Console.WriteLine("\n✅ SOLUTION: Databases solve ALL of these problems!");