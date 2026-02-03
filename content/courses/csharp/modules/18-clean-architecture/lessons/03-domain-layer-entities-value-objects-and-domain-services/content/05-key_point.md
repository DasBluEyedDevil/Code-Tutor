---
type: "KEY_POINT"
title: "Domain Modeling Patterns"
---

## Key Takeaways

- **Entities have identity, Value Objects do not** -- a `Product` entity is identified by its ID. A `Money` value object is defined by its amount and currency. Two Money objects with the same values are interchangeable.

- **Domain Events decouple side effects** -- instead of `Order.PlaceOrder()` directly sending emails and updating inventory, it raises an `OrderPlaced` event. Other parts of the system react independently.

- **Keep the Domain layer framework-free** -- no EF Core attributes, no ASP.NET references, no NuGet dependencies. The Domain should be pure C# expressing business rules and nothing else.
