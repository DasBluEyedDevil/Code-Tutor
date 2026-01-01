---
type: "ARCHITECTURE"
title: "Domain-Driven Design Principles"
---

## Rich vs Anemic Domain Models

When building real-world applications like ShopFlow, how you model your domain entities matters significantly. There are two main approaches to consider.

**Anemic Domain Model (Avoid)**

An anemic domain model is essentially a data container with public setters and no business logic. While it seems simpler, it leads to scattered validation and broken invariants:

```csharp
// ANEMIC - Avoid this pattern!
public class Product
{
    public string Name { get; set; }  // Anyone can set invalid name
    public decimal Price { get; set; }  // Could be set to negative!
    public int Stock { get; set; }  // No protection against invalid values
}

// Business logic ends up scattered everywhere
product.Stock = product.Stock - quantity;  // What if Stock goes negative?
```

**Rich Domain Model (Prefer)**

A rich domain model encapsulates behavior alongside data. Private setters and behavior methods protect your business rules:

```csharp
// RICH - ShopFlow's recommended pattern
public class Product
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    public Product(string name, decimal price, int initialStock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required");
        if (price <= 0)
            throw new ArgumentException("Price must be positive");
        if (initialStock < 0)
            throw new ArgumentException("Stock cannot be negative");

        Name = name;
        Price = price;
        Stock = initialStock;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");
        if (Stock - quantity < 0)
            throw new InvalidOperationException("Insufficient stock");
        Stock -= quantity;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");
        Stock += quantity;
    }
}
```

## Encapsulation Guards Invariants

Encapsulation is not just about hiding data - it protects your business rules. In ShopFlow, we have invariants that must NEVER be violated:

- Stock can never go negative
- Order total must equal sum of line items
- Product price must be positive
- Customer email must be valid format

By making setters private and exposing behavior methods, the class itself enforces these rules. No external code can put the object into an invalid state.

## Entity vs Value Object

Domain-Driven Design distinguishes between two types of domain objects:

**Entities (Have Identity)**

Entities are defined by their unique identity, not their attributes. Two products with identical names and prices are still different products if they have different IDs. In ShopFlow:

- Product (identified by ProductId)
- Order (identified by OrderId)
- Customer (identified by CustomerId)
- ShoppingCart (identified by CartId)

**Value Objects (Defined by Values)**

Value objects have no identity - they are defined entirely by their attribute values. Two value objects with the same values are considered equal and interchangeable. In ShopFlow:

```csharp
// Value Object - immutable, equality by value
public record Money(decimal Amount, string Currency)
{
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot add different currencies");
        return new Money(Amount + other.Amount, Currency);
    }
}

public record Address(string Street, string City, string PostalCode, string Country);

public record OrderLineItem(Guid ProductId, string ProductName, int Quantity, Money UnitPrice)
{
    public Money Total => new Money(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}
```

Value objects are naturally implemented as C# records because records provide value-based equality by default. This makes comparing addresses or money amounts intuitive and correct.

## ShopFlow Domain Model Summary

In the ShopFlow e-commerce application, we apply these principles consistently:

- **Product**: Entity with private setters, behavior methods for stock management
- **Order**: Entity with line items, calculates totals, enforces order state transitions
- **Money**: Value object for currency-safe price calculations
- **Address**: Value object for shipping and billing addresses
- **Customer**: Entity with validation on email and profile updates

By following rich domain model principles, ShopFlow's code is more maintainable, bugs are caught early through validation, and business rules are documented in code rather than scattered across the application.