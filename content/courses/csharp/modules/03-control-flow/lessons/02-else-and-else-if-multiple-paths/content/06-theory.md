---
type: "THEORY"
title: "Control Flow Patterns in Clean Code"
---

## Designing Readable Conditional Logic

Control flow is where code complexity often explodes. These patterns keep ShopFlow's business logic maintainable as it grows.

**Avoid Deep Nesting**
Deeply nested if-statements are hard to read and test. Compare:

```csharp
// BAD: Deep nesting obscures the happy path
if (user != null)
{
    if (user.IsActive)
    {
        if (cart.Items.Count > 0)
        {
            if (cart.Total >= 25m)
            {
                ApplyFreeShipping(cart);
            }
        }
    }
}

// GOOD: Guard clauses with early returns
if (user is null) return;
if (!user.IsActive) return;
if (cart.Items.Count == 0) return;
if (cart.Total < 25m) return;

ApplyFreeShipping(cart);
```

**Replace Conditionals with Polymorphism**
When you find yourself writing switch statements on object types, consider using polymorphism instead:

```csharp
// Instead of: if (product is DigitalProduct) ... else if (product is PhysicalProduct) ...
// Use: product.CalculateShipping() where each type implements its own logic
```

**Boolean Method Extraction**
Complex conditions should be extracted to well-named methods:

```csharp
// BAD: What does this even check?
if (order.Total > 100m && order.Customer.MemberSince < DateTime.Today.AddYears(-1) && !order.HasPromoCode)

// GOOD: The condition explains itself
if (IsEligibleForLoyaltyDiscount(order))
```

**Null-Coalescing and Null-Conditional Operators**
Modern C# provides elegant null handling:

```csharp
// Null-coalescing: provide default value
string customerName = order.Customer?.Name ?? "Guest";

// Null-conditional: safely navigate nullable references  
int? itemCount = cart?.Items?.Count;
```

**In ShopFlow**, we enforce a maximum nesting depth of 2 in code reviews, extract complex conditions to descriptive methods, and use pattern matching for type-based logic. These practices keep our codebase readable as the team grows. This discipline pays dividends in reduced debugging time and easier onboarding for new developers.