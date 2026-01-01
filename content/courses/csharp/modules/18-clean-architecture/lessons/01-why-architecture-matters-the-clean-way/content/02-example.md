---
type: "EXAMPLE"
title: "ShopFlow Project Structure"
---

This example shows how ShopFlow is organized following Clean Architecture principles.

```csharp
// ===== SHOPFLOW CLEAN ARCHITECTURE STRUCTURE =====
// Solution folder organization

/*
ShopFlow/
├── src/
│   ├── ShopFlow.Domain/              # Core business logic (innermost)
│   │   ├── Entities/
│   │   │   ├── Product.cs
│   │   │   ├── Order.cs
│   │   │   ├── Customer.cs
│   │   │   └── OrderItem.cs
│   │   ├── ValueObjects/
│   │   │   ├── Money.cs
│   │   │   ├── Address.cs
│   │   │   └── Email.cs
│   │   ├── Enums/
│   │   │   ├── OrderStatus.cs
│   │   │   └── PaymentMethod.cs
│   │   ├── Exceptions/
│   │   │   ├── DomainException.cs
│   │   │   └── InsufficientStockException.cs
│   │   └── ShopFlow.Domain.csproj     # NO dependencies on other projects!
│   │
│   ├── ShopFlow.Application/          # Use cases and interfaces
│   │   ├── Interfaces/
│   │   │   ├── IProductRepository.cs
│   │   │   ├── IOrderRepository.cs
│   │   │   ├── IEmailService.cs
│   │   │   └── IPaymentGateway.cs
│   │   ├── Services/
│   │   │   ├── OrderService.cs
│   │   │   ├── ProductService.cs
│   │   │   └── InventoryService.cs
│   │   ├── DTOs/
│   │   │   ├── CreateOrderRequest.cs
│   │   │   ├── OrderResponse.cs
│   │   │   └── ProductDto.cs
│   │   ├── Validators/
│   │   │   └── CreateOrderValidator.cs
│   │   └── ShopFlow.Application.csproj # References: ShopFlow.Domain
│   │
│   ├── ShopFlow.Infrastructure/        # External implementations
│   │   ├── Data/
│   │   │   ├── ShopFlowDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── ProductConfiguration.cs
│   │   │   │   └── OrderConfiguration.cs
│   │   │   └── Migrations/
│   │   ├── Repositories/
│   │   │   ├── ProductRepository.cs    # Implements IProductRepository
│   │   │   └── OrderRepository.cs      # Implements IOrderRepository
│   │   ├── Services/
│   │   │   ├── EmailService.cs         # Implements IEmailService
│   │   │   └── StripePaymentGateway.cs # Implements IPaymentGateway
│   │   └── ShopFlow.Infrastructure.csproj # References: ShopFlow.Application
│   │
│   └── ShopFlow.API/                   # Presentation layer
│       ├── Controllers/
│       │   ├── ProductsController.cs
│       │   └── OrdersController.cs
│       ├── Endpoints/                  # Minimal API endpoints
│       │   ├── ProductEndpoints.cs
│       │   └── OrderEndpoints.cs
│       ├── Middleware/
│       │   └── ExceptionHandlingMiddleware.cs
│       ├── Program.cs                  # DI configuration
│       └── ShopFlow.API.csproj         # References: ShopFlow.Infrastructure
│
└── tests/
    ├── ShopFlow.Domain.Tests/
    ├── ShopFlow.Application.Tests/
    └── ShopFlow.API.Tests/
*/

// ===== DEPENDENCY DIRECTION =====
// API → Infrastructure → Application → Domain
// Dependencies ALWAYS point inward!

// Domain: NO project references (standalone)
// Application: References Domain only
// Infrastructure: References Application (and transitively Domain)
// API: References Infrastructure (and transitively all others)
```
