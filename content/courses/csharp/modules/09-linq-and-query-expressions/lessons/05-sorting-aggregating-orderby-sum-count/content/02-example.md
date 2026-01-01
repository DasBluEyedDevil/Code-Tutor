---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

List<int> numbers = new List<int> { 5, 2, 8, 1, 9, 3, 7 };

// SORTING
var ascending = numbers.OrderBy(n => n);
Console.WriteLine("Ascending: " + string.Join(", ", ascending));

var descending = numbers.OrderByDescending(n => n);
Console.WriteLine("Descending: " + string.Join(", ", descending));

// AGGREGATING
int count = numbers.Count();
int sum = numbers.Sum();
double average = numbers.Average();
int min = numbers.Min();
int max = numbers.Max();

Console.WriteLine("Count: " + count);
Console.WriteLine("Sum: " + sum);
Console.WriteLine("Average: " + average);
Console.WriteLine("Min: " + min + ", Max: " + max);

// WORKING WITH OBJECTS
class Product
{
    public string Name;
    public decimal Price;
    public string Category;
}

List<Product> products = new List<Product>
{
    new Product { Name = "Laptop", Price = 999, Category = "Electronics" },
    new Product { Name = "Mouse", Price = 25, Category = "Electronics" },
    new Product { Name = "Desk", Price = 299, Category = "Furniture" },
    new Product { Name = "Chair", Price = 199, Category = "Furniture" }
};

// Sort by price
var byPrice = products.OrderBy(p => p.Price);
foreach (var p in byPrice)
{
    Console.WriteLine(p.Name + ": $" + p.Price);
}

// Sort by name, then by price (multi-level)
var sorted = products
    .OrderBy(p => p.Category)
    .ThenBy(p => p.Price);

foreach (var p in sorted)
{
    Console.WriteLine(p.Category + " - " + p.Name + ": $" + p.Price);
}

// Aggregate with selector
int productCount = products.Count();
decimal totalValue = products.Sum(p => p.Price);
decimal avgPrice = products.Average(p => p.Price);
decimal cheapest = products.Min(p => p.Price);
decimal mostExpensive = products.Max(p => p.Price);

Console.WriteLine("Total products: " + productCount);
Console.WriteLine("Total value: $" + totalValue);
Console.WriteLine("Average price: $" + avgPrice);
Console.WriteLine("Price range: $" + cheapest + " - $" + mostExpensive);
```
