---
type: "ANALOGY"
title: "Validation in Production E-commerce Systems"
---

## Real-World Validation Patterns

In production e-commerce systems like ShopFlow, validation logic is critical for preventing bad data from corrupting your database, ensuring security, and providing clear feedback to users. Here's how professional developers approach validation.

**Defense in Depth**
Never trust user input—even from your own frontend. Production systems validate at multiple layers:
1. **Client-side** (JavaScript): Fast feedback, but easily bypassed—never rely on it alone
2. **API Controller**: Data Annotations and model binding catch obvious errors
3. **Service Layer**: Business rules like 'cart cannot exceed $10,000' or 'user must be verified to purchase alcohol'
4. **Database**: Constraints ensure data integrity even if code has bugs

**Early Returns (Guard Clauses)**
Professional code validates preconditions first and returns early, avoiding deep nesting:

```csharp
public decimal CalculateDiscount(Order order)
{
    if (order is null) throw new ArgumentNullException(nameof(order));
    if (order.Items.Count == 0) return 0m;
    if (order.Total < 50m) return 0m;
    
    // Main logic only runs if all preconditions pass
    return order.Total * GetDiscountRate(order.Customer);
}
```

**Fail Fast Principle**
Detect and report errors as early as possible. A validation error caught at the API layer is cheaper to handle than a database constraint violation, which is cheaper than a production bug discovered by customers.

**Pattern Matching for Status Handling**
C#'s pattern matching makes complex conditional logic readable:

```csharp
string message = orderStatus switch
{
    "Pending" => "Your order is being processed.",
    "Shipped" => "Your order is on its way!",
    "Delivered" => "Enjoy your purchase!",
    _ => "Status unknown—contact support."
};
```

**In ShopFlow**, we implement multi-layer validation using Data Annotations on DTOs, FluentValidation for complex rules, and domain validation in entity methods. This ensures invalid data never reaches the database.