---
type: "THEORY"
title: "Dependency Rule"
---

## The Golden Rule: Dependencies Point Inward

The most important rule in Clean Architecture is the **Dependency Rule**: source code dependencies can only point INWARD. Nothing in an inner circle can know anything about something in an outer circle.

**THE CONCENTRIC CIRCLES:**
```
┌─────────────────────────────────────────────┐
│  PRESENTATION (API, Blazor, Console)        │
│  ┌─────────────────────────────────────┐    │
│  │  INFRASTRUCTURE (EF Core, APIs)     │    │
│  │  ┌─────────────────────────────┐    │    │
│  │  │  APPLICATION (Use Cases)    │    │    │
│  │  │  ┌─────────────────────┐    │    │    │
│  │  │  │  DOMAIN (Entities)  │    │    │    │
│  │  │  └─────────────────────┘    │    │    │
│  │  └─────────────────────────────┘    │    │
│  └─────────────────────────────────────┘    │
└─────────────────────────────────────────────┘
```

**DEPENDENCY INVERSION IN ACTION:**

The problem: Application layer needs to save data to a database, but it can't reference Entity Framework (that would violate the dependency rule).

The solution: Application defines an INTERFACE, Infrastructure IMPLEMENTS it.

```csharp
// In Application layer (inner)
public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
}

// In Infrastructure layer (outer)
public class ProductRepository : IProductRepository
{
    private readonly ShopFlowDbContext _context;
    
    public async Task<Product> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);
        
    public async Task AddAsync(Product product)
        => await _context.Products.AddAsync(product);
}
```

**WHY THIS MATTERS:**

1. **Testability**: You can test Application layer with mock repositories - no database needed
2. **Flexibility**: Switch from SQL Server to PostgreSQL by changing only Infrastructure
3. **Maintainability**: Changes to external systems don't ripple into business logic
4. **Parallel Development**: Teams can work on different layers independently

**INTERFACES AT BOUNDARIES:**

Every boundary between layers should have interfaces:
- Application defines `IProductRepository`, Infrastructure implements it
- Application defines `IEmailService`, Infrastructure implements it
- Application defines `IPaymentGateway`, Infrastructure implements it

The inner layers define WHAT they need (interfaces), outer layers provide HOW (implementations).

**REGISTRATION IN DI CONTAINER:**

```csharp
// In Program.cs (Presentation layer)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmailService, SendGridEmailService>();
builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
```

The DI container wires everything together at runtime, but compile-time dependencies still point inward.

Think: 'The Domain is the sun - everything orbits around it, but it doesn't know or care about the planets. Each layer only knows about the layer directly inside it.'