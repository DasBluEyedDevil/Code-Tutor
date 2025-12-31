# C# Full-Stack Development Course Design

**Date:** 2025-01-01
**Status:** Approved
**Target:** Junior Full-Stack .NET Developer
**Duration:** 65-75 hours (restructured from 29h fundamentals + ~45h new content)

---

## Executive Summary

Complete restructure and expansion of the C# course from "language fundamentals" to "job-ready full-stack .NET developer." The course builds a progressive e-commerce capstone (ShopFlow) using TDD throughout, covering ASP.NET Core 9, Blazor United, EF Core 9, and Azure deployment.

### Key Decisions Made

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Target Role | Full-Stack .NET Developer | Broader employability than backend-only |
| UI Technology | Blazor United (.NET 8+) | Modern standard, all render modes |
| Capstone Approach | Progressive (one app throughout) | Connects all concepts naturally |
| Capstone Domain | E-Commerce Store | Rich domain, portfolio-worthy |
| Database | SQL Server + PostgreSQL | Demonstrates EF Core abstraction |
| Testing | TDD-Integrated | Professional discipline from the start |
| Deployment | Cloud-Native Complete | Containers, CI/CD, Azure services |
| Authentication | Full Auth Story | Identity, JWT, OAuth, policies |
| Course Length | 60-80 hours | Bootcamp-level depth |
| Integration | Complete Restructure | Best learning experience |
| Content Format | Enhanced sections | New DEEP_DIVE, REAL_WORLD, ARCHITECTURE types |

---

## Technology Stack

Based on web research conducted 2025-01-01:

### .NET 9 (Released November 2024)
- ASP.NET Core 9 with built-in OpenAPI (replaces Swashbuckle)
- 93% memory reduction in Minimal APIs
- 15% faster startup with Dynamic PGO
- Native AOT support for SignalR
- MapStaticAssets for optimized static file delivery

**Sources:**
- [What's new in .NET 9 | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [What's new in ASP.NET Core 9 | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-9.0)

### Blazor United (.NET 8+)
- Four render modes: Static SSR, Interactive Server, Interactive WebAssembly, Interactive Auto
- Per-component render mode selection
- Circuit pooling for efficient SignalR connections
- .NET 9 adds `RendererInfo` for mode detection and SSR redirects

**Sources:**
- [ASP.NET Core Blazor render modes | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes)
- [Blazor United in 2025 | Meta Design Solutions](https://metadesignsolutions.com/blazor-united-in-2025-full-stack-net-with-wasm-server-and-hybrid-rendering/)

### EF Core 9
- Improved migration warnings for non-transactional operations
- SQL Server fill-factor support
- Improved temporal table migrations
- Breaking change: pending model changes throw exceptions
- Azure Cosmos DB provider rewrite

**Sources:**
- [What's New in EF Core 9 | Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/whatsnew)
- [Breaking changes in EF Core 9 | Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes)

### Azure Deployment (2025 Best Practices)
- Azure Container Apps for serverless containers
- Azure Key Vault for secrets management
- Application Insights for observability
- Azure Developer CLI (azd) for streamlined deployment
- Zone redundancy for reliability

**Sources:**
- [Deployment best practices | Microsoft Learn](https://learn.microsoft.com/en-us/azure/app-service/deploy-best-practices)
- [Azure Container Apps Guide 2025 | Kunal Das](https://kunaldaskd.medium.com/azure-container-apps-your-complete-2025-guide-to-serverless-container-deployment-de6ef2ef1f1a)

### xUnit Testing (2.9.3)
- Arrange-Act-Assert pattern
- `[Fact]` and `[Theory]` with `[InlineData]`
- Parallel execution by default
- Constructor injection for fixtures
- WebApplicationFactory for integration tests

**Sources:**
- [Unit testing C# with xUnit | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)
- [Best Practices for Unit Testing in .NET | CodeJack](https://codejack.com/2025/01/best-practices-for-unit-testing-in-net/)

---

## Enhanced Content Section Types

| Type | Purpose | Min Words |
|------|---------|-----------|
| ANALOGY | Relatable mental model for the concept | 150 |
| EXAMPLE | Working code with inline comments | 200 |
| THEORY | Technical explanation and syntax breakdown | 250 |
| WARNING | Common mistakes and how to avoid them | 150 |
| DEEP_DIVE | Internals for curious learners (optional) | 300 |
| REAL_WORLD | Production considerations and trade-offs | 200 |
| ARCHITECTURE | Design decisions and patterns | 250 |

---

## Module Structure

### Phase 1: Foundations (Modules 1-7, ~18 hours)

Restructured from existing content with e-commerce domain examples.

#### Module 1: Getting Started with C# (2 hours)
- Existing content preserved
- Add IDE setup for full-stack work
- .NET 9 / CLR introduction

#### Module 2: Variables, Types & Operations (3 hours)
- Rewrite examples: `decimal price`, `string productName`, `int quantity`
- Money handling with `decimal`
- Nullable reference types

#### Module 3: Control Flow & Logic (3 hours)
- Exercises filter products, validate cart rules, check inventory
- Pattern matching introduction

#### Module 4: Methods & Problem Decomposition (2.5 hours)
- Build reusable functions: `CalculateDiscount()`, `ValidateEmail()`
- Method overloading, optional parameters

#### Module 5: Object-Oriented Programming (5 hours)

| Lesson | Content |
|--------|---------|
| 5.1 | Classes and objects - `Product`, `Category` |
| 5.2 | Properties, fields, encapsulation |
| 5.3 | Constructors and initialization |
| 5.4 | Inheritance - `DigitalProduct : Product` |
| 5.5 | Interfaces - `IPaymentProcessor`, `IShippingCalculator` |
| 5.6 | Abstract classes and polymorphism |
| 5.7 | Records and value objects |

**NEW:** ARCHITECTURE section - "Why we model real-world concepts as classes"

#### Module 6: Collections & LINQ (4 hours)

| Lesson | Content |
|--------|---------|
| 6.1 | Arrays and Lists |
| 6.2 | Dictionaries and Sets |
| 6.3 | LINQ fundamentals - Where, Select, OrderBy |
| 6.4 | LINQ advanced - GroupBy, Join, Aggregate |
| 6.5 | Method syntax vs query syntax |

**NEW:** REAL_WORLD section - "LINQ in database queries vs in-memory"

#### Module 7: Async/Await & File I/O (3 hours)

| Lesson | Content |
|--------|---------|
| 7.1 | Synchronous vs asynchronous |
| 7.2 | async/await fundamentals |
| 7.3 | File reading and writing |
| 7.4 | JSON serialization with System.Text.Json |
| 7.5 | Configuration files and appsettings.json |

---

### Phase 2: Web APIs (Modules 8-9, ~9 hours)

#### Module 8: Introduction to Web APIs (4 hours)

| Lesson | Content | Capstone |
|--------|---------|----------|
| 8.1 | What is an API? HTTP fundamentals | ANALOGY: Restaurant/Kitchen |
| 8.2 | Creating .NET 9 Web API project | Create `ShopFlow.Api` |
| 8.3 | Minimal APIs vs Controller-based | Build `/health` both ways |
| 8.4 | Routing, path/query parameters | `GET /products/{id}` |
| 8.5 | Request/response bodies, DTOs | `CreateProductRequest` |
| 8.6 | Status codes and conventions | 200, 201, 400, 404, 500 |

#### Module 9: Building the ShopFlow API (5 hours)

| Lesson | Content | TDD Integration |
|--------|---------|-----------------|
| 9.1 | Project structure, solution organization | `ShopFlow.Api`, `ShopFlow.Core`, `ShopFlow.Tests` |
| 9.2 | Products API - full CRUD | First test-first feature |
| 9.3 | Categories API with relationships | Nested routes |
| 9.4 | Input validation with Data Annotations | `[Required]`, `[Range]` |
| 9.5 | Global exception handling middleware | Consistent error responses |
| 9.6 | OpenAPI documentation (.NET 9 built-in) | `Microsoft.AspNetCore.OpenApi` |

---

### Phase 3: Data Layer (Modules 10-11, ~9 hours)

#### Module 10: Database Fundamentals (3 hours)

| Lesson | Content |
|--------|---------|
| 10.1 | Relational database concepts |
| 10.2 | SQL basics for .NET developers |
| 10.3 | Setting up SQL Server LocalDB |
| 10.4 | Setting up PostgreSQL |

#### Module 11: Entity Framework Core 9 (6 hours)

| Lesson | Content | Capstone |
|--------|---------|----------|
| 11.1 | What is an ORM? EF Core intro | ANALOGY: Translator |
| 11.2 | DbContext and entity configuration | `ShopFlowDbContext` |
| 11.3 | Migrations: code-first evolution | `Add-Migration` |
| 11.4 | Relationships: 1:N, M:N | Category→Products |
| 11.5 | LINQ to Entities | `.Include()`, projections |
| 11.6 | Insert, update, delete | Change tracking |
| 11.7 | Transactions and concurrency | Optimistic concurrency |
| 11.8 | Switching providers | SQL Server ↔ PostgreSQL |

**NEW sections:**
- DEEP_DIVE: "How EF Core tracks entity changes"
- REAL_WORLD: "N+1 query problem and how to avoid it"
- WARNING: "EF9 breaking change - pending model changes throw"

---

### Phase 4: Blazor UI (Modules 12-13, ~11 hours)

#### Module 12: Blazor Fundamentals (5 hours)

| Lesson | Content |
|--------|---------|
| 12.1 | What is Blazor? Component-based UI |
| 12.2 | Render modes explained (SSR, Server, WASM, Auto) |
| 12.3 | Creating a Blazor Web App (.NET 9) |
| 12.4 | Components, parameters, rendering |
| 12.5 | Event handling and data binding |
| 12.6 | Component lifecycle |
| 12.7 | Layouts and navigation |

#### Module 13: Building the ShopFlow Storefront (6 hours)

| Lesson | Content | Render Mode |
|--------|---------|-------------|
| 13.1 | Product catalog page | Static SSR |
| 13.2 | Product detail with reviews | Static SSR + streaming |
| 13.3 | Shopping cart component | Interactive Server |
| 13.4 | Checkout flow | Interactive Server |
| 13.5 | Admin dashboard | Interactive Auto |
| 13.6 | Calling API from Blazor | HttpClient |
| 13.7 | Forms and validation | EditForm |
| 13.8 | State management patterns | Cascading values |

---

### Phase 5: Testing (Modules 14-16, ~13 hours)

#### Module 14: Testing Fundamentals (4 hours)

| Lesson | Content |
|--------|---------|
| 14.1 | Why testing matters |
| 14.2 | Types: unit, integration, E2E |
| 14.3 | xUnit fundamentals |
| 14.4 | Arrange-Act-Assert pattern |
| 14.5 | Test project setup |
| 14.6 | Running tests: CLI, IDE, CI |

#### Module 15: Test-Driven Development (5 hours)

| Lesson | Content | Capstone |
|--------|---------|----------|
| 15.1 | TDD cycle: Red-Green-Refactor | |
| 15.2 | First test-first feature | `CalculateCartTotal()` |
| 15.3 | Mocking with NSubstitute | Isolate services |
| 15.4 | Testing async code | |
| 15.5 | Testing edge cases/exceptions | |
| 15.6 | TDD mindset | Tests as specs |

#### Module 16: Integration & API Testing (4 hours)

| Lesson | Content |
|--------|---------|
| 16.1 | WebApplicationFactory |
| 16.2 | Testing CRUD endpoints |
| 16.3 | Test database strategies |
| 16.4 | Testing with authentication |
| 16.5 | Blazor component testing (bUnit) |
| 16.6 | Code coverage metrics |

**From Module 16 onward:** All capstone features built TDD-style.

---

### Phase 6: Architecture (Modules 17-19, ~11 hours)

#### Module 17: Dependency Injection Deep Dive (3 hours)

| Lesson | Content |
|--------|---------|
| 17.1 | What is dependency injection? |
| 17.2 | Service lifetimes |
| 17.3 | Registering and resolving |
| 17.4 | Constructor injection patterns |
| 17.5 | Keyed services (.NET 8+) |
| 17.6 | DI in middleware and Blazor |

#### Module 18: Clean Architecture (5 hours)

| Lesson | Content | Capstone |
|--------|---------|----------|
| 18.1 | Why architecture matters | |
| 18.2 | Layers: Domain, App, Infra, Presentation | |
| 18.3 | Domain layer: entities, value objects | `Order`, `Money` |
| 18.4 | Application layer: use cases | `CreateOrderHandler` |
| 18.5 | Infrastructure layer | `EfOrderRepository` |
| 18.6 | Refactoring to clean architecture | Multi-project |
| 18.7 | CQRS introduction | Optional pattern |

#### Module 19: Configuration & Logging (3 hours)

| Lesson | Content |
|--------|---------|
| 19.1 | Configuration providers |
| 19.2 | Options pattern: `IOptions<T>` |
| 19.3 | Environment-specific config |
| 19.4 | Logging: `ILogger<T>` |
| 19.5 | Structured logging with scopes |
| 19.6 | Serilog integration |

---

### Phase 7: Security (Modules 20-22, ~11 hours)

#### Module 20: Authentication Fundamentals (4 hours)

| Lesson | Content |
|--------|---------|
| 20.1 | Authentication vs Authorization |
| 20.2 | ASP.NET Core Identity setup |
| 20.3 | User registration and login |
| 20.4 | Cookie auth for Blazor |
| 20.5 | JWT tokens for APIs |
| 20.6 | Refresh tokens |

#### Module 21: External Authentication (3 hours)

| Lesson | Content |
|--------|---------|
| 21.1 | OAuth 2.0 / OIDC concepts |
| 21.2 | Google authentication |
| 21.3 | Microsoft authentication |
| 21.4 | GitHub authentication |
| 21.5 | External login callbacks |
| 21.6 | ShopFlow social login |

#### Module 22: Authorization Patterns (4 hours)

| Lesson | Content |
|--------|---------|
| 22.1 | Role-based authorization |
| 22.2 | Claims-based authorization |
| 22.3 | Policy-based authorization |
| 22.4 | Resource-based authorization |
| 22.5 | Authorization in Blazor |
| 22.6 | ShopFlow: Customer, Seller, Admin |

---

### Phase 8: DevOps (Modules 23-25, ~13 hours)

#### Module 23: Docker & Containerization (4 hours)

| Lesson | Content |
|--------|---------|
| 23.1 | What are containers? |
| 23.2 | Docker fundamentals |
| 23.3 | Dockerfile for .NET |
| 23.4 | docker-compose multi-container |
| 23.5 | Container best practices |
| 23.6 | ShopFlow containerized |

#### Module 24: CI/CD with GitHub Actions (4 hours)

| Lesson | Content |
|--------|---------|
| 24.1 | What is CI/CD? |
| 24.2 | GitHub Actions fundamentals |
| 24.3 | Build and test pipeline |
| 24.4 | Code quality gates |
| 24.5 | Docker image builds |
| 24.6 | Environment deployments |

#### Module 25: Azure Cloud Deployment (5 hours)

| Lesson | Content |
|--------|---------|
| 25.1 | Azure fundamentals |
| 25.2 | Azure Container Apps |
| 25.3 | Azure SQL / PostgreSQL |
| 25.4 | Azure Key Vault |
| 25.5 | Application Insights |
| 25.6 | Azure Developer CLI |
| 25.7 | Production checklist |

---

### Phase 9: Capstone Completion (Module 26, ~5 hours)

#### Module 26: ShopFlow Capstone Completion

| Lesson | Content |
|--------|---------|
| 26.1 | Feature: Inventory management |
| 26.2 | Feature: Order processing workflow |
| 26.3 | Feature: Payment integration (Stripe test) |
| 26.4 | Feature: Search with filtering/pagination |
| 26.5 | Performance optimization |
| 26.6 | Security hardening |
| 26.7 | Final production deployment |
| 26.8 | Course retrospective |

---

## ShopFlow Progressive Build Summary

| Module | Features Added |
|--------|----------------|
| 9 | Products API, Categories API |
| 11 | Database persistence, relationships |
| 13 | Product catalog UI, cart, checkout |
| 16 | Full test coverage |
| 18 | Clean architecture refactor |
| 22 | User auth, roles, permissions |
| 23-25 | Containerized, deployed to Azure |
| 26 | Inventory, orders, payments, polished |

**Final Capabilities:**
- Public storefront (SSR for SEO)
- Shopping cart and checkout (Interactive Server)
- User accounts with social login (Google, Microsoft, GitHub)
- Seller dashboard for product management
- Admin dashboard for order management
- RESTful API with JWT authentication
- SQL Server + PostgreSQL support
- 80%+ test coverage (TDD)
- Deployed to Azure Container Apps with CI/CD

---

## Implementation Approach

### Phase 1: Foundation Restructure (Modules 1-7)
- Audit existing 29 hours of content
- Map to new module structure
- Rewrite examples with e-commerce domain
- Add ARCHITECTURE and REAL_WORLD sections
- Update to .NET 9 syntax

### Phase 2: Web & Data Layer (Modules 8-11)
- Write entirely new content
- Research → write → create challenges per lesson
- Build ShopFlow API incrementally
- Validate all code compiles and runs

### Phase 3: Blazor UI (Modules 12-13)
- New content with render mode demonstrations
- ShopFlow storefront feature-by-feature
- bUnit test examples

### Phase 4: Testing Integration (Modules 14-16)
- Establish TDD discipline
- Retrofit tests for Modules 8-13
- All subsequent content test-first

### Phase 5: Architecture & Security (Modules 17-22)
- ShopFlow refactoring documented
- Clean architecture migration step-by-step
- Full auth implementation

### Phase 6: DevOps & Capstone (Modules 23-26)
- Real deployment artifacts
- Dockerfile, docker-compose, GitHub Actions
- Azure infrastructure (Bicep/azd)
- Final polish

---

## Quality Gates

Each lesson must pass before completion:

- [ ] All code examples compile and run on .NET 9
- [ ] Challenges have working, tested solutions
- [ ] Test cases validate correctly
- [ ] No TODOs, stubs, or placeholders anywhere
- [ ] Content sections meet minimum word counts
- [ ] ANALOGY section is relatable and accurate
- [ ] WARNING section covers real mistakes
- [ ] Code follows current .NET conventions

---

## Dependencies & Prerequisites

- .NET 9 SDK
- SQL Server LocalDB + PostgreSQL
- Docker Desktop
- Azure subscription (free tier sufficient)
- GitHub account
- Visual Studio 2022 or VS Code + C# Dev Kit

---

## Risk Mitigations

| Risk | Mitigation |
|------|------------|
| Scope creep | Strict module boundaries |
| Outdated info | Web research before each module |
| Code rot | All examples in ShopFlow repo |
| Incomplete content | Quality gate checklist per lesson |
| Technology changes | Pin to .NET 9 LTS, document versions |

---

## Metrics

| Metric | Target |
|--------|--------|
| Total hours | 65-75 |
| Total modules | 26 |
| Total lessons | ~150 |
| Avg lesson time | 30 min |
| Challenge per lesson | 1-2 |
| Test coverage (ShopFlow) | 80%+ |

---

## Next Steps

1. Create `ShopFlow` solution repository
2. Set up git worktree for implementation
3. Create detailed implementation plan per module
4. Begin Phase 1: Foundation Restructure

---

*Document generated through collaborative design session.*
*Ready for implementation approval.*
