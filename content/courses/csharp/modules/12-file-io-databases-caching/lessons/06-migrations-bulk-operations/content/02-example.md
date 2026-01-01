---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

// MIGRATIONS (Commands)
// dotnet ef migrations add InitialCreate
// dotnet ef database update
// dotnet ef migrations add AddPriceColumn
// dotnet ef database update

class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}

class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}

using var context = new AppDbContext();  // Modern using declaration

context.Database.EnsureCreated();

// Seed data
if (!context.Products.Any())
{
    context.Products.AddRange(
        new Product { Name = "Laptop", Price = 1000, Category = "Electronics" },
        new Product { Name = "Mouse", Price = 30, Category = "Electronics" },
        new Product { Name = "Desk", Price = 200, Category = "Furniture" }
    );
    context.SaveChanges();
}

// BULK UPDATE (EF Core 7+)
Console.WriteLine("=== BULK UPDATE ===");
int updated = context.Products
    .Where(p => p.Category == "Electronics")
    .ExecuteUpdate(setters => setters
        .SetProperty(p => p.Price, p => p.Price * 1.1m));

Console.WriteLine($"Updated {updated} products with single SQL!");
// Generated SQL: UPDATE Products SET Price = Price * 1.1 WHERE Category = 'Electronics'

// BULK DELETE (EF Core 7+)
Console.WriteLine("\n=== BULK DELETE ===");
int deleted = context.Products
    .Where(p => p.Price < 50)
    .ExecuteDelete();

Console.WriteLine($"Deleted {deleted} products");
// Generated SQL: DELETE FROM Products WHERE Price < 50

// TRADITIONAL UPDATE (for comparison - slower!)
var products = context.Products.Where(p => p.Category == "Furniture").ToList();
foreach (var p in products)
{
    p.Price *= 1.05m;  // 5% increase
}
context.SaveChanges();  // Separate UPDATE for each!
// context disposed at end of scope

// MIGRATION WORKFLOW:
// 1. Change your entity classes (add/remove properties)
// 2. Run: dotnet ef migrations add DescriptiveName
// 3. Review generated migration code
// 4. Run: dotnet ef database update
// 5. Database schema updated!

// MIGRATION COMMANDS:
// List migrations: dotnet ef migrations list
// Remove last:     dotnet ef migrations remove
// Rollback:        dotnet ef database update PreviousMigration
// Generate SQL:    dotnet ef migrations script
```
