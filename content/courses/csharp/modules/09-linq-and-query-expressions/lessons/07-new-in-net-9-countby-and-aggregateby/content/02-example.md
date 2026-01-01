---
type: "EXAMPLE"
title: "Code Example"
---

.NET 9 introduces CountBy and AggregateBy - two LINQ methods that replace common GroupBy patterns with cleaner, more efficient code.

```csharp
using System;
using System.Linq;

var products = new[]
{
    new { Name = "Apple", Category = "Fruit", Price = 1.50m },
    new { Name = "Banana", Category = "Fruit", Price = 0.75m },
    new { Name = "Carrot", Category = "Vegetable", Price = 0.50m },
    new { Name = "Broccoli", Category = "Vegetable", Price = 1.25m },
    new { Name = "Orange", Category = "Fruit", Price = 1.00m }
};

// ===== COUNTBY: Count items by key =====
Console.WriteLine("=== CountBy ===");
var countByCategory = products.CountBy(p => p.Category);
foreach (var (category, count) in countByCategory)
    Console.WriteLine($"{category}: {count} items");
// Output: Fruit: 3 items, Vegetable: 2 items

// ===== AGGREGATEBY: Sum/aggregate values by key =====
Console.WriteLine("\n=== AggregateBy ===");
var totalByCategory = products.AggregateBy(
    keySelector: p => p.Category,
    seed: 0m,
    func: (total, product) => total + product.Price);

foreach (var (category, total) in totalByCategory)
    Console.WriteLine($"{category}: ${total}");
// Output: Fruit: $3.25, Vegetable: $1.75

// ===== COMPARE TO OLD WAY (verbose) =====
Console.WriteLine("\n=== Old GroupBy Way (for comparison) ===");
var oldWayCount = products
    .GroupBy(p => p.Category)
    .Select(g => new { Category = g.Key, Count = g.Count() });

var oldWaySum = products
    .GroupBy(p => p.Category)
    .Select(g => new { Category = g.Key, Total = g.Sum(p => p.Price) });

foreach (var item in oldWaySum)
    Console.WriteLine($"{item.Category}: ${item.Total}");

// ===== MORE EXAMPLES =====
// Count orders by status
var orders = new[] { "Pending", "Shipped", "Pending", "Delivered", "Shipped", "Shipped" };
var orderCounts = orders.CountBy(status => status);
Console.WriteLine("\n=== Order Status Counts ===");
foreach (var (status, count) in orderCounts)
    Console.WriteLine($"{status}: {count}");

// AggregateBy with string concatenation
var employees = new[]
{
    new { Name = "Alice", Department = "IT" },
    new { Name = "Bob", Department = "HR" },
    new { Name = "Carol", Department = "IT" },
    new { Name = "Dave", Department = "IT" }
};

var namesByDept = employees.AggregateBy(
    e => e.Department,
    seed: "",
    func: (names, emp) => names == "" ? emp.Name : names + ", " + emp.Name);

Console.WriteLine("\n=== Employees by Department ===");
foreach (var (dept, names) in namesByDept)
    Console.WriteLine($"{dept}: {names}");
```
