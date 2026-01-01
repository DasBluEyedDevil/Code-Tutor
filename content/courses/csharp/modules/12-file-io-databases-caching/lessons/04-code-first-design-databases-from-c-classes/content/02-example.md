---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// STEP 1: DEFINE ENTITIES (classes)
class Customer
{
    public int Id { get; set; }  // Primary key (convention)
    
    [Required]  // NOT NULL in database
    [MaxLength(100)]  // VARCHAR(100)
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]  // Validation attribute
    public string Email { get; set; } = string.Empty;
    
    public int Age { get; set; }
    
    // Navigation property - RELATIONSHIP!
    public List<Order> Orders { get; set; } = new();
}

class Order
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }  // Foreign key
    
    [Column(TypeName = "decimal(18,2)")]  // Precision for money
    public decimal Total { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    // Navigation property
    public Customer Customer { get; set; } = null!;
}

// STEP 2: DBCONTEXT
class StoreDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=store.db");
    }
    
    // STEP 3: Configure relationships (Fluent API)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Customer
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)  // Customer has many Orders
            .WithOne(o => o.Customer)  // Each Order has one Customer
            .HasForeignKey(o => o.CustomerId);
        
        // Seed data (initial data)
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 30 }
        );
    }
}

// STEP 4: CREATE DATABASE
using var context = new StoreDbContext();  // Modern using declaration
context.Database.EnsureDeleted();  // Delete if exists (learning only!)
context.Database.EnsureCreated();  // Create from classes!

Console.WriteLine("Database created from C# classes!");
Console.WriteLine("Tables: Customers, Orders");
Console.WriteLine("Relationship: Customer 1-to-Many Orders");
// context disposed at end of scope
```
