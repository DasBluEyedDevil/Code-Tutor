---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// WITHOUT DATABASE - text file storage (bad!)
using System.IO;
using System.Collections.Generic;

class Customer
{
    public string Name;
    public string Email;
}

// Writing to text file
void SaveCustomer(Customer customer)
{
    File.AppendAllText("customers.txt", 
        $"{customer.Name},{customer.Email}\n");
}

// Reading from text file (slow!)
List<Customer> LoadCustomers()
{
    var customers = new List<Customer>();
    var lines = File.ReadAllLines("customers.txt");
    
    foreach (var line in lines)
    {
        var parts = line.Split(',');
        customers.Add(new Customer 
        { 
            Name = parts[0], 
            Email = parts[1] 
        });
    }
    return customers;
}

// Finding customer (read ENTIRE file!)
Customer? FindByEmail(string email)
{
    var customers = LoadCustomers();  // Read ALL data!
    return customers.FirstOrDefault(c => c.Email == email);
}

// WITH DATABASE (next lessons!) - pseudocode
/*
var customer = dbContext.Customers
    .Where(c => c.Email == "john@email.com")
    .FirstOrDefault();  // Database finds it instantly!

// Update
customer.Email = "newemail@example.com";
dbContext.SaveChanges();  // Transaction ensures safety!
*/

// Database features you get:
// - Indexes for fast searching
// - Transactions (all-or-nothing)
// - Constraints (email MUST be unique)
// - Relationships (Customer has many Orders)
// - Concurrent access (multiple users safely)
// - Query optimization (database is smart!)
```
