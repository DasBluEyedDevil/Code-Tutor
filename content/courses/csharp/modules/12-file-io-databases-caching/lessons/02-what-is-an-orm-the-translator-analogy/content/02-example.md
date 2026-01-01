---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ORM CONCEPT - Entity Framework Core

// 1. DEFINE YOUR CLASSES (entities)
class Customer
{
    public int Id { get; set; }  // Primary key
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}

class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }  // Foreign key
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    
    // Navigation property (relationship!)
    public Customer Customer { get; set; }
}

// 2. WHAT ORM DOES BEHIND THE SCENES
// You write:
var youngCustomers = dbContext.Customers
    .Where(c => c.Age < 30)
    .ToList();

// ORM translates to SQL:
// SELECT * FROM Customers WHERE Age < 30

// You write:
var customer = new Customer 
{ 
    Name = "John", 
    Email = "john@example.com", 
    Age = 25 
};
dbContext.Customers.Add(customer);
dbContext.SaveChanges();

// ORM translates to SQL:
// INSERT INTO Customers (Name, Email, Age) 
// VALUES ('John', 'john@example.com', 25)

// You write:
var customer = dbContext.Customers.Find(1);
customer.Email = "newemail@example.com";
dbContext.SaveChanges();

// ORM translates to SQL:
// UPDATE Customers 
// SET Email = 'newemail@example.com' 
// WHERE Id = 1

// BENEFITS OF ORM:
// ✅ Type safety - compile-time errors, not runtime!
// ✅ LINQ queries - familiar C# syntax
// ✅ Auto-mapping - no manual reader["column"] code
// ✅ Relationships - navigate Customer.Orders easily
// ✅ Change tracking - EF knows what changed!
// ✅ Database agnostic - switch SQL Server to PostgreSQL easily
```
