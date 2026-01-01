---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

class AppDbContext : DbContext
{
    // DbSet = Table
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}

// USING DbContext - Modern using declaration (C# 8+)
using var context = new AppDbContext();  // Disposed at end of scope

context.Database.EnsureCreated();

// CHANGE TRACKING
var product = new Product { Name = "Laptop", Price = 999.99m };
context.Products.Add(product);  // Tracked as 'Added'

Console.WriteLine("State: " + context.Entry(product).State);  // Added

context.SaveChanges();  // Persist to database
Console.WriteLine("State: " + context.Entry(product).State);  // Unchanged

// QUERYING DbSet
var allProducts = context.Products.ToList();  // SELECT *
var expensive = context.Products
    .Where(p => p.Price > 500)
    .OrderBy(p => p.Name)
    .ToList();  // SELECT ... WHERE ... ORDER BY

// MODIFYING TRACKED ENTITY
var firstProduct = context.Products.First();
firstProduct.Price = 899.99m;  // EF tracks this change!

Console.WriteLine("Modified state: " + context.Entry(firstProduct).State);
context.SaveChanges();  // UPDATE

// REMOVING
var toRemove = context.Products.Find(1);  // Find by primary key
if (toRemove != null)
{
    context.Products.Remove(toRemove);  // Marked as Deleted
    context.SaveChanges();  // DELETE
}

// DbContext TRACKING STATUS
Console.WriteLine("Total tracked: " + context.ChangeTracker.Entries().Count());

// context is DISPOSED at end of scope (connection closed)
// 'using var' is the modern C# 8+ way - cleaner, same behavior!
```
