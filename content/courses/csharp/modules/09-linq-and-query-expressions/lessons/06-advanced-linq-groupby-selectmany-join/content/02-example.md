---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Sample data
var products = new[]
{
    new { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999m },
    new { Id = 2, Name = "Mouse", Category = "Electronics", Price = 29m },
    new { Id = 3, Name = "Desk", Category = "Furniture", Price = 299m },
    new { Id = 4, Name = "Chair", Category = "Furniture", Price = 199m },
    new { Id = 5, Name = "Monitor", Category = "Electronics", Price = 399m }
};

// ===== GROUPBY: Group products by category =====
var productsByCategory = products.GroupBy(p => p.Category);

Console.WriteLine("=== GroupBy: Products by Category ===");
foreach (var group in productsByCategory)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()} items):");
    foreach (var product in group)
    {
        Console.WriteLine($"  - {product.Name}: ${product.Price}");
    }
}

// GroupBy with aggregation
var categorySummary = products
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        Count = g.Count(),
        TotalValue = g.Sum(p => p.Price),
        AveragePrice = g.Average(p => p.Price)
    });

Console.WriteLine("\n=== GroupBy with Aggregation ===");
foreach (var summary in categorySummary)
{
    Console.WriteLine($"{summary.Category}: {summary.Count} items, " +
        $"Total: ${summary.TotalValue}, Avg: ${summary.AveragePrice:F2}");
}

// ===== SELECTMANY: Flatten nested collections =====
var departments = new[]
{
    new { Name = "IT", Employees = new[] { "Alice", "Bob" } },
    new { Name = "HR", Employees = new[] { "Carol" } },
    new { Name = "Sales", Employees = new[] { "Dave", "Eve", "Frank" } }
};

// Without SelectMany: nested IEnumerable<string[]>
var nested = departments.Select(d => d.Employees);

// With SelectMany: flat IEnumerable<string>
var allEmployees = departments.SelectMany(d => d.Employees);
Console.WriteLine("\n=== SelectMany: All Employees ===");
Console.WriteLine(string.Join(", ", allEmployees));

// SelectMany with projection
var employeeDetails = departments
    .SelectMany(
        d => d.Employees,
        (dept, emp) => new { Department = dept.Name, Employee = emp }
    );

Console.WriteLine("\n=== SelectMany with Projection ===");
foreach (var e in employeeDetails)
{
    Console.WriteLine($"{e.Employee} works in {e.Department}");
}

// ===== JOIN: Combine two data sources =====
var orders = new[]
{
    new { OrderId = 1, ProductId = 1, Quantity = 2 },
    new { OrderId = 2, ProductId = 3, Quantity = 1 },
    new { OrderId = 3, ProductId = 2, Quantity = 5 }
};

var orderDetails = orders.Join(
    products,                      // Inner collection
    order => order.ProductId,      // Outer key selector
    product => product.Id,         // Inner key selector
    (order, product) => new        // Result selector
    {
        order.OrderId,
        product.Name,
        order.Quantity,
        Total = product.Price * order.Quantity
    }
);

Console.WriteLine("\n=== Join: Order Details ===");
foreach (var detail in orderDetails)
{
    Console.WriteLine($"Order {detail.OrderId}: {detail.Quantity}x {detail.Name} = ${detail.Total}");
}
```
