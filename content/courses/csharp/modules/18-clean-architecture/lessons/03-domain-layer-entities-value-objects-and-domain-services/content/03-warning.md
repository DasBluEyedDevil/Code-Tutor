---
type: "WARNING"
title: "Domain Layer Anti-Patterns"
---

## Common Mistakes That Violate Clean Architecture

The Domain layer is the heart of your application, and corrupting it with infrastructure concerns is one of the most common architectural mistakes. Here are anti-patterns to avoid:

**ANTI-PATTERN 1: Infrastructure Dependencies in Domain**

```csharp
// BAD - Domain entity using Entity Framework!
public class Product
{
    [Key]  // ← EF Core attribute in Domain!
    public int Id { get; set; }
    
    [Required, MaxLength(200)]  // ← More EF/validation attributes
    public string Name { get; set; }
    
    // WORSE - Direct database access!
    private readonly DbContext _context;
    
    public void Save() => _context.SaveChanges();  // ← NEVER do this!
}
```

**Why it's bad**: Domain now depends on Microsoft.EntityFrameworkCore. You can't test Product without a database. You can't switch ORMs without rewriting Domain.

**ANTI-PATTERN 2: Anemic Domain Model**

```csharp
// BAD - No behavior, just a data container
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }  // ← Public setters everywhere
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

// All logic lives in services instead
public class ProductService
{
    public void UpdateStock(Product product, int quantity)
    {
        product.Stock -= quantity;  // ← Logic outside the entity
        if (product.Stock < 0)
            throw new Exception("Not enough stock");
    }
}
```

**Why it's bad**: The Product entity is just a dumb data bag. Business rules are scattered across services. Anyone can set Stock to -100 directly. No encapsulation.

**ANTI-PATTERN 3: Using DTOs in Domain**

```csharp
// BAD - Domain method accepting a DTO
public class Order
{
    public void AddItem(OrderItemDto dto)  // ← DTO in Domain!
    {
        _items.Add(new OrderItem(dto.ProductId, dto.Quantity));
    }
}
```

**Why it's bad**: DTOs belong in Application layer. Domain shouldn't know about data transfer concerns.

**ANTI-PATTERN 4: Calling External Services from Domain**

```csharp
// BAD - Domain calling external API
public class Order
{
    private readonly IEmailService _email;  // ← Infrastructure concern!
    
    public void Complete()
    {
        Status = OrderStatus.Completed;
        _email.SendOrderConfirmation(this);  // ← Side effect in entity!
    }
}
```

**Why it's bad**: Domain entities shouldn't have infrastructure dependencies. Use Domain Events instead - emit an event, let Application/Infrastructure handle the email.

**THE CORRECT PATTERN:**

```csharp
// GOOD - Rich domain model with encapsulation
public class Product
{
    public int Id { get; private set; }  // ← Private setters
    public string Name { get; private set; }
    public Money Price { get; private set; }  // ← Value Object
    public int StockQuantity { get; private set; }

    // Factory method for valid creation
    public static Product Create(string name, Money price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name required");
        // ... validation and creation
    }

    // Behavior with business rules
    public void RemoveStock(int quantity)
    {
        if (quantity > StockQuantity)
            throw new InsufficientStockException(...);
        StockQuantity -= quantity;
    }
}
```

**GOLDEN RULES FOR DOMAIN LAYER:**

1. NO references to Infrastructure packages (EF Core, HTTP clients, etc.)
2. NO public setters - use methods that enforce business rules
3. Entities have behavior, not just data
4. Value Objects are immutable
5. Validation happens inside the domain objects
6. Use Domain Events for side effects, not direct calls