---
type: "THEORY"
title: "The Layers Explained"
---

## Understanding Each Layer

**DOMAIN LAYER (The Core)**
The Domain layer is the heart of your application. It contains:
- **Entities**: Core business objects with identity (Product, Order, Customer)
- **Value Objects**: Immutable objects without identity (Money, Address, Email)
- **Domain Events**: Things that happened in the domain (OrderPlaced, PaymentReceived)
- **Business Rules**: Core logic that never changes regardless of UI or database

The Domain layer has ZERO dependencies on other projects. It doesn't know about Entity Framework, ASP.NET Core, or any external framework. This makes it extremely testable and portable.

**APPLICATION LAYER (The Orchestrator)**
The Application layer coordinates the work. It contains:
- **Interfaces**: Contracts that Infrastructure must implement (IProductRepository, IEmailService)
- **Application Services**: Use cases that orchestrate domain objects (CreateOrderService, ProcessPaymentService)
- **DTOs**: Data transfer objects for moving data between layers
- **Validators**: Input validation using FluentValidation or similar

The Application layer depends ONLY on Domain. It defines interfaces but doesn't implement them - that's Infrastructure's job.

**INFRASTRUCTURE LAYER (The Adapter)**
The Infrastructure layer implements all external concerns:
- **Database Access**: Entity Framework Core DbContext, repositories
- **External Services**: Email providers, payment gateways, cloud storage
- **File System**: Reading/writing files
- **Third-party APIs**: Integration with external systems

Infrastructure implements the interfaces defined in Application. This is the Dependency Inversion Principle in action!

**PRESENTATION LAYER (The UI)**
The Presentation layer is what users interact with:
- **API Controllers/Endpoints**: HTTP request handling
- **Blazor Components**: Interactive web UI
- **Console Apps**: Command-line interfaces
- **Background Services**: Hosted services for background work

Presentation depends on Infrastructure for DI registration but communicates with Application layer services.