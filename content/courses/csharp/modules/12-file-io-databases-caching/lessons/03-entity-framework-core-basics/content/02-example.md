---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ENTITY FRAMEWORK CORE SETUP

// 1. INSTALL PACKAGES (via NuGet or dotnet CLI):
// dotnet add package Microsoft.EntityFrameworkCore.Sqlite
// dotnet add package Microsoft.EntityFrameworkCore.Design

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// 2. DEFINE ENTITIES
class Product
{
    public int Id { get; set; }  // Primary Key (by convention)
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

// 3. CREATE DBCONTEXT
class AppDbContext : DbContext
{
    // DbSet = table in database
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // SQLite database (file-based, great for learning!)
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}

// 4. USING EF CORE
using var context = new AppDbContext();  // Modern using declaration

// ENSURE DATABASE EXISTS
context.Database.EnsureCreated();

// CREATE (INSERT)
var product = new Product 
{ 
    Name = "Laptop", 
    Price = 999.99m, 
    Stock = 10 
};
context.Products.Add(product);
context.SaveChanges();  // Executes SQL INSERT

Console.WriteLine("Product added with ID: " + product.Id);

// READ (SELECT)
var allProducts = context.Products.ToList();
var expensiveProducts = context.Products
    .Where(p => p.Price > 500)
    .ToList();

// UPDATE
var productToUpdate = context.Products.First();
productToUpdate.Price = 899.99m;
context.SaveChanges();  // Executes SQL UPDATE

// DELETE
var productToDelete = context.Products.Find(1);  // Find by primary key
if (productToDelete != null)
{
    context.Products.Remove(productToDelete);
    context.SaveChanges();  // Executes SQL DELETE
}
// context disposed at end of scope
```
