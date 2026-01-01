---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// DEFINING a namespace
namespace MyApp.Models
{
    class Customer
    {
        public string Name;
        public string Email;
    }
    
    class Product
    {
        public string Name;
        public decimal Price;
    }
}

namespace MyApp.Services
{
    class EmailService
    {
        public void SendEmail(string to, string message)
        {
            Console.WriteLine("Sending email to: " + to);
        }
    }
}

// USING namespaces
using System;
using System.Collections.Generic;
using MyApp.Models;  // Now we can use Customer, Product directly!

namespace MyApp
{
    class Program
    {
        static void Main()
        {
            // Without 'using MyApp.Models', we'd need:
            // MyApp.Models.Customer c = new MyApp.Models.Customer();
            
            // With 'using MyApp.Models':
            Customer customer = new Customer();
            customer.Name = "John";
            
            List<Product> products = new List<Product>();
            // List comes from System.Collections.Generic (using statement)
        }
    }
}

// GLOBAL USINGS (.NET 6+ / C# 10+)
// In a GlobalUsings.cs file - applies to ALL files in project!
global using System;
global using System.Collections.Generic;
global using System.Linq;
// No need to repeat 'using System;' in every file!

// IMPLICIT USINGS (.NET 6+)
// In .csproj: <ImplicitUsings>enable</ImplicitUsings>
// Automatically includes: System, System.Collections.Generic,
// System.IO, System.Linq, System.Threading.Tasks, and more!
// Check obj/Debug/net8.0/GlobalUsings.g.cs to see what's included

// FILE-SCOPED NAMESPACES (C# 10+) - cleaner!
namespace MyApp.Models;  // Note the semicolon!

class Order  // No extra indentation needed!
{
    public int Id { get; set; }
}
```
