# C# Course Expansion: Module Lessons & ShopFlow Implementation

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Expand 6 new modules (currently 1 lesson each) to 4-5 lessons each, and replace ShopFlow boilerplate with actual e-commerce implementation.

**Architecture:** Two parallel tracks:
1. **Content Track**: Add lessons to modules 18, 20-22, 24, 26 in `course.json`
2. **Code Track**: Build ShopFlow API with Products, Cart, Orders, Auth endpoints

**Tech Stack:** .NET 9, ASP.NET Core 9, EF Core 9, xUnit, PostgreSQL

---

## Part A: Module Lesson Expansion

### Task 1: Module 18 - Clean Architecture (Add 3 lessons)

**Current:** 1 lesson (lesson-18-01: Why Architecture Matters)
**Target:** 4 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-18-02 - The Four Layers**

Add to module-18.lessons array:
```json
{
  "id": "lesson-18-02",
  "title": "The Four Layers (Domain, Application, Infrastructure, Presentation)",
  "moduleId": "module-18",
  "order": 2,
  "estimatedMinutes": 35,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "The Layer Cake Model",
      "content": "Imagine a layer cake where each layer has a specific purpose:\n\n**Domain Layer (Bottom)** - The core flavor. Pure business logic with no frosting or decorations. It knows nothing about how it will be served.\n\n**Application Layer** - The filling between layers. Orchestrates domain operations, defines use cases like 'Place Order' or 'Add to Cart'.\n\n**Infrastructure Layer** - The cake stand and serving tools. Databases, external APIs, email services - all the 'how' of delivering the cake.\n\n**Presentation Layer (Top)** - The frosting and decorations. What users see - APIs, web pages, mobile apps.\n\nThe critical rule: **Inner layers never depend on outer layers.** The cake flavor doesn't care about the frosting."
    },
    {
      "type": "EXAMPLE",
      "title": "ShopFlow Layer Structure",
      "content": "Here's how ShopFlow's solution maps to Clean Architecture layers:",
      "code": "ShopFlow.sln\n├── src/\n│   ├── ShopFlow.Domain/           # DOMAIN LAYER\n│   │   ├── Entities/\n│   │   │   ├── Product.cs         # Core business entity\n│   │   │   ├── Order.cs\n│   │   │   └── Customer.cs\n│   │   ├── ValueObjects/\n│   │   │   ├── Money.cs           # Immutable value type\n│   │   │   └── Address.cs\n│   │   ├── Exceptions/\n│   │   │   └── DomainException.cs\n│   │   └── Interfaces/            # Abstractions only\n│   │       └── IProductRepository.cs\n│   │\n│   ├── ShopFlow.Application/       # APPLICATION LAYER\n│   │   ├── Products/\n│   │   │   ├── Commands/\n│   │   │   │   └── CreateProductCommand.cs\n│   │   │   └── Queries/\n│   │   │       └── GetProductQuery.cs\n│   │   ├── Services/\n│   │   │   └── OrderService.cs    # Orchestrates domain\n│   │   └── DTOs/\n│   │       └── ProductDto.cs\n│   │\n│   ├── ShopFlow.Infrastructure/    # INFRASTRUCTURE LAYER\n│   │   ├── Persistence/\n│   │   │   ├── AppDbContext.cs\n│   │   │   └── Repositories/\n│   │   │       └── ProductRepository.cs  # Implements IProductRepository\n│   │   └── ExternalServices/\n│   │       └── EmailService.cs\n│   │\n│   └── ShopFlow.Api/               # PRESENTATION LAYER\n│       ├── Controllers/\n│       │   └── ProductsController.cs\n│       └── Program.cs\n│\n└── tests/\n    ├── ShopFlow.Domain.Tests/\n    ├── ShopFlow.Application.Tests/\n    └── ShopFlow.Api.Tests/",
      "language": "text"
    },
    {
      "type": "THEORY",
      "title": "Dependency Rule",
      "content": "## The Golden Rule of Clean Architecture\n\nDependencies point **inward only**:\n\n```\nPresentation → Application → Domain\nInfrastructure → Application → Domain\n```\n\n**Domain Layer (innermost)**\n- ZERO dependencies on anything outside\n- Pure C# - no NuGet packages except BCL\n- Contains: Entities, Value Objects, Domain Services, Interfaces\n\n**Application Layer**\n- Depends on: Domain only\n- Contains: Use Cases, DTOs, Service Interfaces, Validation\n- Orchestrates domain objects to fulfill business operations\n\n**Infrastructure Layer**\n- Depends on: Application and Domain\n- Implements interfaces defined in Domain/Application\n- Contains: Database, File System, External APIs, Messaging\n\n**Presentation Layer**\n- Depends on: Application (and transitively Domain)\n- Contains: Controllers, Views, API Endpoints, Middleware\n- Handles HTTP, serialization, authentication\n\n## Why This Matters\n\nChanging your database from SQL Server to PostgreSQL affects **only** Infrastructure. Swapping REST for GraphQL affects **only** Presentation. The business logic in Domain and Application remains untouched."
    },
    {
      "type": "ARCHITECTURE",
      "title": "Project References in .NET",
      "content": "## Setting Up Layer Dependencies\n\n```xml\n<!-- ShopFlow.Domain.csproj - NO project references -->\n<Project Sdk=\"Microsoft.NET.Sdk\">\n  <PropertyGroup>\n    <TargetFramework>net9.0</TargetFramework>\n  </PropertyGroup>\n</Project>\n\n<!-- ShopFlow.Application.csproj -->\n<Project Sdk=\"Microsoft.NET.Sdk\">\n  <ItemGroup>\n    <ProjectReference Include=\"..\\ShopFlow.Domain\\ShopFlow.Domain.csproj\" />\n  </ItemGroup>\n</Project>\n\n<!-- ShopFlow.Infrastructure.csproj -->\n<Project Sdk=\"Microsoft.NET.Sdk\">\n  <ItemGroup>\n    <ProjectReference Include=\"..\\ShopFlow.Application\\ShopFlow.Application.csproj\" />\n    <PackageReference Include=\"Microsoft.EntityFrameworkCore\" Version=\"9.0.0\" />\n  </ItemGroup>\n</Project>\n\n<!-- ShopFlow.Api.csproj -->\n<Project Sdk=\"Microsoft.NET.Sdk.Web\">\n  <ItemGroup>\n    <ProjectReference Include=\"..\\ShopFlow.Application\\ShopFlow.Application.csproj\" />\n    <ProjectReference Include=\"..\\ShopFlow.Infrastructure\\ShopFlow.Infrastructure.csproj\" />\n  </ItemGroup>\n</Project>\n```\n\n**Notice:** Api references Infrastructure but Domain doesn't reference anything. This enforces the dependency rule at compile time."
    }
  ],
  "challenges": [
    {
      "type": "QUIZ",
      "id": "lesson-18-02-quiz-01",
      "question": "Which layer should contain the IProductRepository interface?",
      "options": [
        {"id": "a", "text": "Infrastructure", "isCorrect": false},
        {"id": "b", "text": "Domain", "isCorrect": true},
        {"id": "c", "text": "Presentation", "isCorrect": false},
        {"id": "d", "text": "Application", "isCorrect": false}
      ],
      "explanation": "Interfaces are defined in the Domain layer. Infrastructure provides the concrete implementation. This allows Domain to stay pure while Infrastructure can be swapped.",
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 2: Add lesson-18-03 - Domain Layer Deep Dive**

```json
{
  "id": "lesson-18-03",
  "title": "Domain Layer: Entities, Value Objects, and Domain Services",
  "moduleId": "module-18",
  "order": 3,
  "estimatedMinutes": 40,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Entities vs Value Objects",
      "content": "Think of the difference like people vs money:\n\n**Entities (like people)**: Two people with the same name are still different people. They have *identity*. Even if John Smith changes his hair color, he's still the same John Smith.\n\n**Value Objects (like money)**: Two $10 bills are interchangeable. They have no identity beyond their *value*. A $10 bill doesn't care which specific bill it is.\n\nIn ShopFlow:\n- `Product` is an Entity - Product #42 is a specific product\n- `Money` is a Value Object - $10.00 is just $10.00, no identity needed"
    },
    {
      "type": "EXAMPLE",
      "title": "Building ShopFlow's Domain Layer",
      "content": "Let's implement key domain concepts for ShopFlow:",
      "code": "// Entities/Product.cs - Entity with identity\nnamespace ShopFlow.Domain.Entities;\n\npublic class Product\n{\n    public int Id { get; private set; }  // Identity\n    public string Name { get; private set; } = string.Empty;\n    public string Description { get; private set; } = string.Empty;\n    public Money Price { get; private set; } = null!;\n    public int StockQuantity { get; private set; }\n    public int CategoryId { get; private set; }\n    public DateTime CreatedAt { get; private set; }\n    \n    private Product() { }  // EF Core needs this\n    \n    public static Product Create(string name, string description, Money price, int categoryId)\n    {\n        if (string.IsNullOrWhiteSpace(name))\n            throw new DomainException(\"Product name is required\");\n        if (price.Amount <= 0)\n            throw new DomainException(\"Price must be positive\");\n            \n        return new Product\n        {\n            Name = name,\n            Description = description,\n            Price = price,\n            CategoryId = categoryId,\n            StockQuantity = 0,\n            CreatedAt = DateTime.UtcNow\n        };\n    }\n    \n    public void UpdatePrice(Money newPrice)\n    {\n        if (newPrice.Amount <= 0)\n            throw new DomainException(\"Price must be positive\");\n        Price = newPrice;\n    }\n    \n    public void AddStock(int quantity)\n    {\n        if (quantity <= 0)\n            throw new DomainException(\"Quantity must be positive\");\n        StockQuantity += quantity;\n    }\n    \n    public void RemoveStock(int quantity)\n    {\n        if (quantity > StockQuantity)\n            throw new DomainException(\"Insufficient stock\");\n        StockQuantity -= quantity;\n    }\n}\n\n// ValueObjects/Money.cs - Value Object (immutable)\nnamespace ShopFlow.Domain.ValueObjects;\n\npublic sealed record Money(decimal Amount, string Currency = \"USD\")\n{\n    public static Money Zero => new(0);\n    \n    public Money Add(Money other)\n    {\n        if (Currency != other.Currency)\n            throw new DomainException(\"Cannot add different currencies\");\n        return new Money(Amount + other.Amount, Currency);\n    }\n    \n    public Money Multiply(int quantity) => new(Amount * quantity, Currency);\n    \n    public override string ToString() => $\"{Currency} {Amount:N2}\";\n}\n\n// Interfaces/IProductRepository.cs\nnamespace ShopFlow.Domain.Interfaces;\n\npublic interface IProductRepository\n{\n    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);\n    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);\n    Task AddAsync(Product product, CancellationToken ct = default);\n    void Update(Product product);\n    void Delete(Product product);\n}",
      "language": "csharp"
    },
    {
      "type": "WARNING",
      "title": "Domain Layer Anti-Patterns",
      "content": "## What NOT to Put in Domain\n\n**No Infrastructure Concerns**\n```csharp\n// WRONG - Domain shouldn't know about EF Core\npublic class Product\n{\n    [Column(\"product_name\")]  // EF Core attribute\n    public string Name { get; set; }\n    \n    public async Task SaveAsync(DbContext db)  // Infrastructure leak!\n    {\n        await db.SaveChangesAsync();\n    }\n}\n\n// CORRECT - Pure domain\npublic class Product\n{\n    public string Name { get; private set; }\n    // No knowledge of how it's persisted\n}\n```\n\n**No Anemic Models**\n```csharp\n// WRONG - No behavior, just data\npublic class Product\n{\n    public int Stock { get; set; }  // Anyone can set to -100!\n}\n\n// CORRECT - Behavior protects invariants\npublic class Product\n{\n    public int Stock { get; private set; }\n    public void RemoveStock(int qty)  // Enforces rules\n    {\n        if (qty > Stock) throw new DomainException(\"Insufficient stock\");\n        Stock -= qty;\n    }\n}\n```\n\n**No Application/UI Concerns**\n```csharp\n// WRONG - DTOs don't belong in Domain\npublic class ProductDto { }  // This goes in Application layer\n```"
    },
    {
      "type": "DEEP_DIVE",
      "title": "Domain Events",
      "content": "## Advanced: Raising Domain Events\n\nDomain events capture something significant that happened in the domain:\n\n```csharp\n// Events/ProductCreatedEvent.cs\npublic record ProductCreatedEvent(int ProductId, string Name, DateTime CreatedAt);\n\n// Entity with events\npublic class Product\n{\n    private readonly List<object> _domainEvents = new();\n    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();\n    \n    public static Product Create(string name, Money price, int categoryId)\n    {\n        var product = new Product { /* ... */ };\n        product._domainEvents.Add(new ProductCreatedEvent(\n            product.Id, name, DateTime.UtcNow));\n        return product;\n    }\n    \n    public void ClearDomainEvents() => _domainEvents.Clear();\n}\n```\n\nInfrastructure layer can listen for these events and trigger side effects (send emails, update search index, etc.) without polluting the domain."
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-18-03-challenge-01",
      "title": "Create a Value Object",
      "description": "Create an Address value object for ShopFlow.",
      "instructions": "Create an `Address` record that:\n1. Has Street, City, State, ZipCode, Country properties\n2. Validates that required fields are not empty\n3. Has a static method `Create` that throws DomainException for invalid data\n4. Overrides ToString to format as 'Street, City, State ZipCode, Country'",
      "starterCode": "namespace ShopFlow.Domain.ValueObjects;\n\npublic sealed record Address\n{\n    // TODO: Add properties\n    \n    // TODO: Add Create factory method\n    \n    // TODO: Override ToString\n}",
      "solution": "namespace ShopFlow.Domain.ValueObjects;\n\npublic sealed record Address\n{\n    public string Street { get; }\n    public string City { get; }\n    public string State { get; }\n    public string ZipCode { get; }\n    public string Country { get; }\n    \n    private Address(string street, string city, string state, string zipCode, string country)\n    {\n        Street = street;\n        City = city;\n        State = state;\n        ZipCode = zipCode;\n        Country = country;\n    }\n    \n    public static Address Create(string street, string city, string state, string zipCode, string country)\n    {\n        if (string.IsNullOrWhiteSpace(street))\n            throw new DomainException(\"Street is required\");\n        if (string.IsNullOrWhiteSpace(city))\n            throw new DomainException(\"City is required\");\n        if (string.IsNullOrWhiteSpace(zipCode))\n            throw new DomainException(\"Zip code is required\");\n        if (string.IsNullOrWhiteSpace(country))\n            throw new DomainException(\"Country is required\");\n            \n        return new Address(street, city, state ?? \"\", zipCode, country);\n    }\n    \n    public override string ToString() =>\n        $\"{Street}, {City}, {State} {ZipCode}, {Country}\";\n}",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Use a private constructor and public static Create factory method"},
        {"level": 2, "text": "Throw DomainException for validation failures"},
        {"level": 3, "text": "Use string interpolation for ToString: $\"{Street}, {City}...\""}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 3: Add lesson-18-04 - Application and Infrastructure Layers**

```json
{
  "id": "lesson-18-04",
  "title": "Application and Infrastructure Layers in Practice",
  "moduleId": "module-18",
  "order": 4,
  "estimatedMinutes": 45,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "The Conductor and the Orchestra",
      "content": "Think of a symphony orchestra:\n\n**Domain Layer** = The sheet music - pure, abstract, the essence of the composition.\n\n**Application Layer** = The conductor - interprets the music, coordinates the sections, ensures timing. Doesn't play any instruments but makes the performance happen.\n\n**Infrastructure Layer** = The instruments and stage - the actual tools that produce sound. Violins, trumpets, the concert hall's acoustics.\n\nThe conductor (Application) works with abstract concepts ('play louder here') and the orchestra (Infrastructure) makes it physically happen."
    },
    {
      "type": "EXAMPLE",
      "title": "ShopFlow Application Layer",
      "content": "The Application layer contains use cases, services, and DTOs:",
      "code": "// Application/Products/Commands/CreateProductCommand.cs\nnamespace ShopFlow.Application.Products.Commands;\n\npublic record CreateProductCommand(\n    string Name,\n    string Description,\n    decimal Price,\n    int CategoryId);\n\n// Application/Products/Handlers/CreateProductHandler.cs\npublic class CreateProductHandler\n{\n    private readonly IProductRepository _repository;\n    private readonly IUnitOfWork _unitOfWork;\n    \n    public CreateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork)\n    {\n        _repository = repository;\n        _unitOfWork = unitOfWork;\n    }\n    \n    public async Task<int> HandleAsync(CreateProductCommand command, CancellationToken ct)\n    {\n        // Create domain entity\n        var product = Product.Create(\n            command.Name,\n            command.Description,\n            new Money(command.Price),\n            command.CategoryId);\n        \n        // Persist through repository\n        await _repository.AddAsync(product, ct);\n        await _unitOfWork.SaveChangesAsync(ct);\n        \n        return product.Id;\n    }\n}\n\n// Application/Products/Queries/GetProductQuery.cs\npublic record GetProductQuery(int Id);\n\npublic class GetProductHandler\n{\n    private readonly IProductRepository _repository;\n    \n    public GetProductHandler(IProductRepository repository)\n    {\n        _repository = repository;\n    }\n    \n    public async Task<ProductDto?> HandleAsync(GetProductQuery query, CancellationToken ct)\n    {\n        var product = await _repository.GetByIdAsync(query.Id, ct);\n        if (product is null) return null;\n        \n        return new ProductDto(\n            product.Id,\n            product.Name,\n            product.Description,\n            product.Price.Amount,\n            product.StockQuantity);\n    }\n}\n\n// Application/Products/DTOs/ProductDto.cs\npublic record ProductDto(\n    int Id,\n    string Name,\n    string Description,\n    decimal Price,\n    int StockQuantity);",
      "language": "csharp"
    },
    {
      "type": "EXAMPLE",
      "title": "ShopFlow Infrastructure Layer",
      "content": "Infrastructure implements the interfaces defined in Domain/Application:",
      "code": "// Infrastructure/Persistence/AppDbContext.cs\nnamespace ShopFlow.Infrastructure.Persistence;\n\npublic class AppDbContext : DbContext, IUnitOfWork\n{\n    public DbSet<Product> Products => Set<Product>();\n    public DbSet<Category> Categories => Set<Category>();\n    \n    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }\n    \n    protected override void OnModelCreating(ModelBuilder modelBuilder)\n    {\n        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);\n    }\n    \n    // IUnitOfWork implementation\n    public async Task SaveChangesAsync(CancellationToken ct = default)\n    {\n        await base.SaveChangesAsync(ct);\n    }\n}\n\n// Infrastructure/Persistence/Configurations/ProductConfiguration.cs\npublic class ProductConfiguration : IEntityTypeConfiguration<Product>\n{\n    public void Configure(EntityTypeBuilder<Product> builder)\n    {\n        builder.HasKey(p => p.Id);\n        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();\n        builder.Property(p => p.Description).HasMaxLength(2000);\n        \n        // Value Object mapping\n        builder.OwnsOne(p => p.Price, priceBuilder =>\n        {\n            priceBuilder.Property(m => m.Amount).HasColumnName(\"Price\");\n            priceBuilder.Property(m => m.Currency).HasColumnName(\"Currency\").HasMaxLength(3);\n        });\n        \n        builder.HasOne<Category>()\n            .WithMany(c => c.Products)\n            .HasForeignKey(p => p.CategoryId);\n    }\n}\n\n// Infrastructure/Persistence/Repositories/ProductRepository.cs\npublic class ProductRepository : IProductRepository\n{\n    private readonly AppDbContext _context;\n    \n    public ProductRepository(AppDbContext context)\n    {\n        _context = context;\n    }\n    \n    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)\n    {\n        return await _context.Products.FindAsync(new object[] { id }, ct);\n    }\n    \n    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)\n    {\n        return await _context.Products.ToListAsync(ct);\n    }\n    \n    public async Task AddAsync(Product product, CancellationToken ct = default)\n    {\n        await _context.Products.AddAsync(product, ct);\n    }\n    \n    public void Update(Product product)\n    {\n        _context.Products.Update(product);\n    }\n    \n    public void Delete(Product product)\n    {\n        _context.Products.Remove(product);\n    }\n}",
      "language": "csharp"
    },
    {
      "type": "REAL_WORLD",
      "title": "Wiring It All Together",
      "content": "## Dependency Injection in Program.cs\n\nThe Presentation layer registers all services with the DI container:\n\n```csharp\n// Program.cs (ShopFlow.Api)\nvar builder = WebApplication.CreateBuilder(args);\n\n// Infrastructure services\nbuilder.Services.AddDbContext<AppDbContext>(options =>\n    options.UseNpgsql(builder.Configuration.GetConnectionString(\"DefaultConnection\")));\n\n// Register repositories\nbuilder.Services.AddScoped<IProductRepository, ProductRepository>();\nbuilder.Services.AddScoped<IUnitOfWork, AppDbContext>();\n\n// Register application handlers\nbuilder.Services.AddScoped<CreateProductHandler>();\nbuilder.Services.AddScoped<GetProductHandler>();\n\nvar app = builder.Build();\n```\n\n## Real-World Benefits\n\n**Testing**: Swap real database for in-memory or mock\n**Flexibility**: Change PostgreSQL to SQL Server in one place\n**Team Scalability**: Different teams own different layers\n**Maintenance**: Changes isolated to affected layer only"
    }
  ],
  "challenges": [
    {
      "type": "QUIZ",
      "id": "lesson-18-04-quiz-01",
      "question": "Where should the EF Core DbContext class be defined?",
      "options": [
        {"id": "a", "text": "Domain layer", "isCorrect": false},
        {"id": "b", "text": "Application layer", "isCorrect": false},
        {"id": "c", "text": "Infrastructure layer", "isCorrect": true},
        {"id": "d", "text": "Presentation layer", "isCorrect": false}
      ],
      "explanation": "DbContext is an infrastructure concern - it's the implementation detail of how we persist data. The Domain layer defines the interface (IProductRepository), and Infrastructure provides the EF Core implementation.",
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 4: Validate JSON**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
```
Expected: "Valid JSON"

**Step 5: Commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-18 Clean Architecture with 3 additional lessons"
```

---

### Task 2: Module 20 - Authentication Fundamentals (Add 4 lessons)

**Current:** 1 lesson (lesson-20-01: Authentication vs Authorization)
**Target:** 5 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-20-02 - ASP.NET Core Identity Setup**

```json
{
  "id": "lesson-20-02",
  "title": "Setting Up ASP.NET Core Identity",
  "moduleId": "module-20",
  "order": 2,
  "estimatedMinutes": 35,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Identity as Your User Management System",
      "content": "ASP.NET Core Identity is like a complete membership management system for a club:\n\n- **User records** - Name, email, password hash stored securely\n- **Sign-up process** - Account creation with validation\n- **Login desk** - Verify credentials, issue membership card (cookie/token)\n- **Lockout policy** - Too many failed attempts? Temporary ban\n- **Password reset** - Forgot password? Here's a secure reset link\n\nYou *could* build all this yourself, but why? Identity handles the security-critical parts correctly so you can focus on your app."
    },
    {
      "type": "EXAMPLE",
      "title": "Adding Identity to ShopFlow",
      "content": "Let's set up Identity in the ShopFlow API:",
      "code": "// 1. Install packages (ShopFlow.Infrastructure.csproj)\n// <PackageReference Include=\"Microsoft.AspNetCore.Identity.EntityFrameworkCore\" Version=\"9.0.0\" />\n\n// 2. Create ApplicationUser (Infrastructure/Identity/ApplicationUser.cs)\nusing Microsoft.AspNetCore.Identity;\n\nnamespace ShopFlow.Infrastructure.Identity;\n\npublic class ApplicationUser : IdentityUser\n{\n    public string FirstName { get; set; } = string.Empty;\n    public string LastName { get; set; } = string.Empty;\n    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;\n    \n    // Navigation to domain Customer entity\n    public int? CustomerId { get; set; }\n}\n\n// 3. Update DbContext\npublic class AppDbContext : IdentityDbContext<ApplicationUser>\n{\n    public DbSet<Product> Products => Set<Product>();\n    // ... other DbSets\n    \n    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }\n}\n\n// 4. Configure in Program.cs\nbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>\n{\n    // Password requirements\n    options.Password.RequiredLength = 8;\n    options.Password.RequireDigit = true;\n    options.Password.RequireLowercase = true;\n    options.Password.RequireUppercase = true;\n    options.Password.RequireNonAlphanumeric = false;\n    \n    // Lockout settings\n    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);\n    options.Lockout.MaxFailedAccessAttempts = 5;\n    \n    // User settings\n    options.User.RequireUniqueEmail = true;\n})\n.AddEntityFrameworkStores<AppDbContext>()\n.AddDefaultTokenProviders();\n\n// 5. Create migration\n// dotnet ef migrations add AddIdentity -p src/ShopFlow.Infrastructure -s src/ShopFlow.Api\n// dotnet ef database update -p src/ShopFlow.Infrastructure -s src/ShopFlow.Api",
      "language": "csharp"
    },
    {
      "type": "THEORY",
      "title": "What Identity Provides",
      "content": "## Identity Components\n\n**UserManager<TUser>**\n- Create, update, delete users\n- Find users by ID, email, or username\n- Validate and hash passwords\n- Manage user claims, roles, tokens\n\n**SignInManager<TUser>**\n- Password sign-in with lockout tracking\n- Two-factor authentication flow\n- External login handling (OAuth)\n- Remember me functionality\n\n**RoleManager<TRole>**\n- Create and manage roles\n- Assign users to roles\n\n## Database Tables Created\n\n| Table | Purpose |\n|-------|--------|\n| AspNetUsers | User accounts |\n| AspNetRoles | Role definitions |\n| AspNetUserRoles | User-to-role mapping |\n| AspNetUserClaims | User claims |\n| AspNetUserLogins | External login providers |\n| AspNetUserTokens | Password reset, 2FA tokens |"
    },
    {
      "type": "WARNING",
      "title": "Identity Security Considerations",
      "content": "## Common Mistakes\n\n**Storing Passwords in Plain Text**\nNever. Identity uses PBKDF2 by default. Never bypass this.\n\n**Weak Password Requirements**\n```csharp\n// Too weak for production!\noptions.Password.RequiredLength = 4;\noptions.Password.RequireDigit = false;\n```\n\n**No Lockout Policy**\nWithout lockout, attackers can brute-force passwords indefinitely.\n\n**Exposing User IDs in URLs**\n```csharp\n// WRONG: Sequential IDs reveal user count\n/api/users/42\n\n// BETTER: Use GUIDs or don't expose at all\n/api/users/me  // Infer from auth\n```\n\n**Not Validating Email**\nEnable email confirmation for production:\n```csharp\noptions.SignIn.RequireConfirmedEmail = true;\n```"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-20-02-challenge-01",
      "title": "Configure Strict Password Policy",
      "description": "Configure Identity with a strict password policy suitable for a financial application.",
      "instructions": "Configure IdentityOptions with:\n1. Minimum 12 character password\n2. Require digit, lowercase, uppercase, and special character\n3. Lockout after 3 failed attempts for 15 minutes\n4. Require unique email\n5. Require confirmed email to sign in",
      "starterCode": "builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>\n{\n    // TODO: Configure password requirements\n    \n    // TODO: Configure lockout\n    \n    // TODO: Configure user and sign-in requirements\n})",
      "solution": "builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>\n{\n    // Password requirements\n    options.Password.RequiredLength = 12;\n    options.Password.RequireDigit = true;\n    options.Password.RequireLowercase = true;\n    options.Password.RequireUppercase = true;\n    options.Password.RequireNonAlphanumeric = true;\n    options.Password.RequiredUniqueChars = 4;\n    \n    // Lockout settings\n    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);\n    options.Lockout.MaxFailedAccessAttempts = 3;\n    options.Lockout.AllowedForNewUsers = true;\n    \n    // User settings\n    options.User.RequireUniqueEmail = true;\n    \n    // Sign-in settings\n    options.SignIn.RequireConfirmedEmail = true;\n})",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Use options.Password.RequiredLength = 12"},
        {"level": 2, "text": "options.Lockout.MaxFailedAccessAttempts = 3 and DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15)"},
        {"level": 3, "text": "options.SignIn.RequireConfirmedEmail = true requires email verification"}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 2: Add lesson-20-03 - User Registration and Login**

```json
{
  "id": "lesson-20-03",
  "title": "Implementing Registration and Login Endpoints",
  "moduleId": "module-20",
  "order": 3,
  "estimatedMinutes": 40,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "The Registration and Login Flow",
      "content": "Think of joining a gym:\n\n**Registration:**\n1. Fill out application form (email, password, details)\n2. Staff validates your info (email format, password strong enough)\n3. You get entered into the system (user created)\n4. You receive a confirmation email (verify you own the email)\n5. You're now a member (account confirmed)\n\n**Login:**\n1. Show your ID at the front desk (provide credentials)\n2. Staff checks the system (validate password hash)\n3. If valid, you get a day pass (cookie or token)\n4. You can now access the gym (authenticated requests)\n5. Pass expires at end of day (session timeout)"
    },
    {
      "type": "EXAMPLE",
      "title": "Registration Endpoint",
      "content": "Here's a complete registration flow for the ShopFlow API:",
      "code": "// Application/Auth/Commands/RegisterCommand.cs\npublic record RegisterCommand(\n    string Email,\n    string Password,\n    string FirstName,\n    string LastName);\n\npublic record RegisterResult(bool Succeeded, string? UserId, IEnumerable<string> Errors);\n\n// Application/Auth/Handlers/RegisterHandler.cs\npublic class RegisterHandler\n{\n    private readonly UserManager<ApplicationUser> _userManager;\n    \n    public RegisterHandler(UserManager<ApplicationUser> userManager)\n    {\n        _userManager = userManager;\n    }\n    \n    public async Task<RegisterResult> HandleAsync(RegisterCommand command)\n    {\n        var user = new ApplicationUser\n        {\n            UserName = command.Email,\n            Email = command.Email,\n            FirstName = command.FirstName,\n            LastName = command.LastName\n        };\n        \n        var result = await _userManager.CreateAsync(user, command.Password);\n        \n        if (!result.Succeeded)\n        {\n            return new RegisterResult(false, null, result.Errors.Select(e => e.Description));\n        }\n        \n        // TODO: Send confirmation email\n        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);\n        // await _emailService.SendConfirmationEmailAsync(user.Email, token);\n        \n        return new RegisterResult(true, user.Id, Enumerable.Empty<string>());\n    }\n}\n\n// Api/Endpoints/AuthEndpoints.cs\npublic static class AuthEndpoints\n{\n    public static void MapAuthEndpoints(this WebApplication app)\n    {\n        var group = app.MapGroup(\"/api/auth\").WithTags(\"Authentication\");\n        \n        group.MapPost(\"/register\", async (\n            RegisterCommand command,\n            RegisterHandler handler) =>\n        {\n            var result = await handler.HandleAsync(command);\n            \n            if (!result.Succeeded)\n            {\n                return Results.BadRequest(new { Errors = result.Errors });\n            }\n            \n            return Results.Created($\"/api/users/{result.UserId}\", new { result.UserId });\n        })\n        .WithName(\"Register\")\n        .Produces(StatusCodes.Status201Created)\n        .Produces(StatusCodes.Status400BadRequest);\n    }\n}",
      "language": "csharp"
    },
    {
      "type": "EXAMPLE",
      "title": "Login Endpoint with Cookie",
      "content": "For web applications, cookie-based login:",
      "code": "// Application/Auth/Commands/LoginCommand.cs\npublic record LoginCommand(string Email, string Password, bool RememberMe = false);\n\npublic record LoginResult(bool Succeeded, bool RequiresTwoFactor, bool IsLockedOut);\n\n// Application/Auth/Handlers/LoginHandler.cs\npublic class LoginHandler\n{\n    private readonly SignInManager<ApplicationUser> _signInManager;\n    \n    public LoginHandler(SignInManager<ApplicationUser> signInManager)\n    {\n        _signInManager = signInManager;\n    }\n    \n    public async Task<LoginResult> HandleAsync(LoginCommand command)\n    {\n        var result = await _signInManager.PasswordSignInAsync(\n            command.Email,\n            command.Password,\n            isPersistent: command.RememberMe,\n            lockoutOnFailure: true);\n        \n        return new LoginResult(\n            result.Succeeded,\n            result.RequiresTwoFactor,\n            result.IsLockedOut);\n    }\n}\n\n// Api/Endpoints/AuthEndpoints.cs - Add login endpoint\ngroup.MapPost(\"/login\", async (\n    LoginCommand command,\n    LoginHandler handler) =>\n{\n    var result = await handler.HandleAsync(command);\n    \n    if (result.IsLockedOut)\n    {\n        return Results.Problem(\n            title: \"Account locked\",\n            detail: \"Too many failed attempts. Try again later.\",\n            statusCode: StatusCodes.Status423Locked);\n    }\n    \n    if (result.RequiresTwoFactor)\n    {\n        return Results.Ok(new { RequiresTwoFactor = true });\n    }\n    \n    if (!result.Succeeded)\n    {\n        return Results.Unauthorized();\n    }\n    \n    return Results.Ok(new { Message = \"Login successful\" });\n})\n.WithName(\"Login\")\n.Produces(StatusCodes.Status200OK)\n.Produces(StatusCodes.Status401Unauthorized)\n.Produces(StatusCodes.Status423Locked);",
      "language": "csharp"
    },
    {
      "type": "WARNING",
      "title": "Security Best Practices",
      "content": "## Critical Security Points\n\n**Don't Reveal User Existence**\n```csharp\n// WRONG: Reveals if email exists\nif (!userExists) return \"Email not found\";\nif (!passwordCorrect) return \"Wrong password\";\n\n// CORRECT: Generic message\nreturn \"Invalid email or password\";\n```\n\n**Rate Limiting**\nWithout rate limiting, attackers can try millions of passwords:\n```csharp\nbuilder.Services.AddRateLimiter(options =>\n{\n    options.AddFixedWindowLimiter(\"auth\", config =>\n    {\n        config.PermitLimit = 5;\n        config.Window = TimeSpan.FromMinutes(1);\n    });\n});\n\ngroup.MapPost(\"/login\", ...).RequireRateLimiting(\"auth\");\n```\n\n**HTTPS Only**\n```csharp\nbuilder.Services.ConfigureApplicationCookie(options =>\n{\n    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;\n    options.Cookie.SameSite = SameSiteMode.Strict;\n    options.Cookie.HttpOnly = true;\n});\n```"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-20-03-challenge-01",
      "title": "Implement Logout Endpoint",
      "description": "Create a logout endpoint that properly signs out the user.",
      "instructions": "Create a POST /api/auth/logout endpoint that:\n1. Signs out the current user using SignInManager\n2. Returns 200 OK with a success message\n3. Only works for authenticated users",
      "starterCode": "group.MapPost(\"/logout\", async (\n    // TODO: Add dependencies\n) =>\n{\n    // TODO: Sign out the user\n    // TODO: Return success response\n});",
      "solution": "group.MapPost(\"/logout\", async (\n    SignInManager<ApplicationUser> signInManager) =>\n{\n    await signInManager.SignOutAsync();\n    return Results.Ok(new { Message = \"Logged out successfully\" });\n})\n.RequireAuthorization()\n.WithName(\"Logout\")\n.Produces(StatusCodes.Status200OK)\n.Produces(StatusCodes.Status401Unauthorized);",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Inject SignInManager<ApplicationUser> as a parameter"},
        {"level": 2, "text": "Call await signInManager.SignOutAsync()"},
        {"level": 3, "text": "Add .RequireAuthorization() to ensure only logged-in users can logout"}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 3: Add lesson-20-04 - JWT Tokens for APIs**

```json
{
  "id": "lesson-20-04",
  "title": "JWT Tokens for API Authentication",
  "moduleId": "module-20",
  "order": 4,
  "estimatedMinutes": 45,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "JWT as a Boarding Pass",
      "content": "A JWT (JSON Web Token) is like an airline boarding pass:\n\n**Header** - The top section showing the airline and pass type\n- Algorithm used (HS256, RS256)\n- Token type (JWT)\n\n**Payload** - Your flight details and passenger info\n- Who you are (subject/user ID)\n- What you can do (roles, claims)\n- When it expires\n\n**Signature** - The barcode that proves it's legitimate\n- Cryptographic signature\n- Can't be forged without the secret key\n\nWhen you present this boarding pass (send the token), security can instantly verify it's valid by checking the signature, without calling the airline's database."
    },
    {
      "type": "EXAMPLE",
      "title": "Generating JWTs in ShopFlow",
      "content": "Here's how to issue JWT tokens for API authentication:",
      "code": "// Application/Auth/Services/JwtService.cs\npublic interface IJwtService\n{\n    string GenerateToken(ApplicationUser user, IList<string> roles);\n}\n\npublic class JwtService : IJwtService\n{\n    private readonly JwtSettings _settings;\n    \n    public JwtService(IOptions<JwtSettings> settings)\n    {\n        _settings = settings.Value;\n    }\n    \n    public string GenerateToken(ApplicationUser user, IList<string> roles)\n    {\n        var claims = new List<Claim>\n        {\n            new(ClaimTypes.NameIdentifier, user.Id),\n            new(ClaimTypes.Email, user.Email!),\n            new(ClaimTypes.Name, $\"{user.FirstName} {user.LastName}\"),\n            new(\"firstName\", user.FirstName),\n            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())\n        };\n        \n        // Add role claims\n        foreach (var role in roles)\n        {\n            claims.Add(new Claim(ClaimTypes.Role, role));\n        }\n        \n        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));\n        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);\n        \n        var token = new JwtSecurityToken(\n            issuer: _settings.Issuer,\n            audience: _settings.Audience,\n            claims: claims,\n            expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),\n            signingCredentials: creds);\n        \n        return new JwtSecurityTokenHandler().WriteToken(token);\n    }\n}\n\n// Application/Auth/Settings/JwtSettings.cs\npublic class JwtSettings\n{\n    public string SecretKey { get; set; } = string.Empty;\n    public string Issuer { get; set; } = string.Empty;\n    public string Audience { get; set; } = string.Empty;\n    public int ExpirationMinutes { get; set; } = 60;\n}\n\n// API endpoint that returns JWT\ngroup.MapPost(\"/token\", async (\n    LoginCommand command,\n    UserManager<ApplicationUser> userManager,\n    IJwtService jwtService) =>\n{\n    var user = await userManager.FindByEmailAsync(command.Email);\n    if (user is null || !await userManager.CheckPasswordAsync(user, command.Password))\n    {\n        return Results.Unauthorized();\n    }\n    \n    var roles = await userManager.GetRolesAsync(user);\n    var token = jwtService.GenerateToken(user, roles);\n    \n    return Results.Ok(new {\n        AccessToken = token,\n        ExpiresIn = 3600,\n        TokenType = \"Bearer\"\n    });\n})\n.WithName(\"GetToken\");",
      "language": "csharp"
    },
    {
      "type": "THEORY",
      "title": "Configuring JWT Authentication",
      "content": "## Setting Up JWT in Program.cs\n\n```csharp\n// appsettings.json\n{\n  \"Jwt\": {\n    \"SecretKey\": \"your-256-bit-secret-key-here-minimum-32-characters\",\n    \"Issuer\": \"ShopFlow.Api\",\n    \"Audience\": \"ShopFlow.Clients\",\n    \"ExpirationMinutes\": 60\n  }\n}\n\n// Program.cs\nbuilder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(\"Jwt\"));\n\nbuilder.Services.AddAuthentication(options =>\n{\n    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;\n    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;\n})\n.AddJwtBearer(options =>\n{\n    var jwtSettings = builder.Configuration.GetSection(\"Jwt\").Get<JwtSettings>()!;\n    \n    options.TokenValidationParameters = new TokenValidationParameters\n    {\n        ValidateIssuer = true,\n        ValidateAudience = true,\n        ValidateLifetime = true,\n        ValidateIssuerSigningKey = true,\n        ValidIssuer = jwtSettings.Issuer,\n        ValidAudience = jwtSettings.Audience,\n        IssuerSigningKey = new SymmetricSecurityKey(\n            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),\n        ClockSkew = TimeSpan.Zero  // No tolerance for expiration\n    };\n});\n\nbuilder.Services.AddAuthorization();\nbuilder.Services.AddScoped<IJwtService, JwtService>();\n```\n\n## Using the Token\n\nClients include the token in the Authorization header:\n```\nGET /api/products HTTP/1.1\nAuthorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\n```"
    },
    {
      "type": "WARNING",
      "title": "JWT Security Pitfalls",
      "content": "## Critical JWT Mistakes\n\n**Weak Secret Key**\n```csharp\n// WRONG: Too short, guessable\nSecretKey = \"secret\"\n\n// CORRECT: Long, random, from secure config\nSecretKey = Environment.GetEnvironmentVariable(\"JWT_SECRET_KEY\")\n```\n\n**Storing Sensitive Data in Payload**\n```csharp\n// WRONG: JWT payload is just Base64, anyone can read it\nnew Claim(\"creditCard\", \"4111-1111-1111-1111\")\n\n// CORRECT: Only include non-sensitive identifiers\nnew Claim(\"userId\", user.Id)\n```\n\n**Long Expiration Times**\n```csharp\n// WRONG: Token valid for 30 days\nexpires: DateTime.UtcNow.AddDays(30)\n\n// CORRECT: Short-lived access token + refresh token\nexpires: DateTime.UtcNow.AddMinutes(15)  // Access token\n```\n\n**Not Validating All Parameters**\n```csharp\n// WRONG: Skipping validation\nValidateIssuer = false,\nValidateAudience = false,\n\n// CORRECT: Validate everything\nValidateIssuer = true,\nValidateAudience = true,\nValidateLifetime = true,\nValidateIssuerSigningKey = true,\n```"
    }
  ],
  "challenges": [
    {
      "type": "QUIZ",
      "id": "lesson-20-04-quiz-01",
      "question": "Why should JWT access tokens have short expiration times?",
      "options": [
        {"id": "a", "text": "To reduce database storage requirements", "isCorrect": false},
        {"id": "b", "text": "Because JWTs cannot be revoked once issued", "isCorrect": true},
        {"id": "c", "text": "To save bandwidth", "isCorrect": false},
        {"id": "d", "text": "Because browsers have memory limits", "isCorrect": false}
      ],
      "explanation": "JWTs are self-contained and stateless. Once issued, they can't be invalidated until they expire. A stolen token remains valid until expiration. Short-lived tokens (15-60 minutes) limit the window of compromise. Use refresh tokens for longer sessions.",
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 4: Add lesson-20-05 - Refresh Tokens**

```json
{
  "id": "lesson-20-05",
  "title": "Refresh Tokens and Token Lifecycle",
  "moduleId": "module-20",
  "order": 5,
  "estimatedMinutes": 35,
  "difficulty": "advanced",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Access Token + Refresh Token Pattern",
      "content": "Think of a hotel key card system:\n\n**Access Token = Room Key Card**\n- Gets you into your room quickly\n- Works for a limited time (hours)\n- If stolen, the thief has access until it expires\n- No database check needed - just validates the card\n\n**Refresh Token = Hotel Registration Receipt**\n- Stored securely at the front desk (database)\n- Used to get a new room key when yours expires\n- Can be invalidated if you report theft\n- Longer validity (days/weeks)\n\nWhen your key card expires, you go to the front desk with your receipt to get a new one. They check if your reservation is still valid, verify your identity, and issue a fresh card."
    },
    {
      "type": "EXAMPLE",
      "title": "Implementing Refresh Tokens",
      "content": "Here's a complete refresh token implementation for ShopFlow:",
      "code": "// Infrastructure/Identity/RefreshToken.cs\npublic class RefreshToken\n{\n    public int Id { get; set; }\n    public string Token { get; set; } = string.Empty;\n    public string UserId { get; set; } = string.Empty;\n    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;\n    public DateTime ExpiresAt { get; set; }\n    public DateTime? RevokedAt { get; set; }\n    public string? ReplacedByToken { get; set; }\n    public string? ReasonRevoked { get; set; }\n    \n    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;\n    public bool IsRevoked => RevokedAt is not null;\n    public bool IsActive => !IsRevoked && !IsExpired;\n}\n\n// Application/Auth/Services/RefreshTokenService.cs\npublic class RefreshTokenService\n{\n    private readonly AppDbContext _context;\n    private readonly IJwtService _jwtService;\n    private readonly UserManager<ApplicationUser> _userManager;\n    \n    public async Task<TokenResponse> RefreshAsync(string refreshToken)\n    {\n        var storedToken = await _context.RefreshTokens\n            .Include(rt => rt.User)\n            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);\n        \n        if (storedToken is null)\n            throw new SecurityTokenException(\"Invalid refresh token\");\n        \n        if (!storedToken.IsActive)\n        {\n            // Token was revoked - possible token reuse attack\n            await RevokeDescendantTokensAsync(storedToken, \"Attempted reuse of revoked token\");\n            throw new SecurityTokenException(\"Token is no longer valid\");\n        }\n        \n        // Rotate: revoke old token, create new one\n        var newRefreshToken = GenerateRefreshToken();\n        storedToken.RevokedAt = DateTime.UtcNow;\n        storedToken.ReplacedByToken = newRefreshToken.Token;\n        storedToken.ReasonRevoked = \"Replaced by new token\";\n        \n        var user = await _userManager.FindByIdAsync(storedToken.UserId);\n        var roles = await _userManager.GetRolesAsync(user!);\n        var accessToken = _jwtService.GenerateToken(user!, roles);\n        \n        newRefreshToken.UserId = user!.Id;\n        _context.RefreshTokens.Add(newRefreshToken);\n        await _context.SaveChangesAsync();\n        \n        return new TokenResponse(accessToken, newRefreshToken.Token);\n    }\n    \n    private RefreshToken GenerateRefreshToken()\n    {\n        var randomBytes = new byte[64];\n        using var rng = RandomNumberGenerator.Create();\n        rng.GetBytes(randomBytes);\n        \n        return new RefreshToken\n        {\n            Token = Convert.ToBase64String(randomBytes),\n            ExpiresAt = DateTime.UtcNow.AddDays(7)\n        };\n    }\n    \n    private async Task RevokeDescendantTokensAsync(RefreshToken token, string reason)\n    {\n        // If this token was replaced by another, revoke that one too\n        if (!string.IsNullOrEmpty(token.ReplacedByToken))\n        {\n            var childToken = await _context.RefreshTokens\n                .FirstOrDefaultAsync(rt => rt.Token == token.ReplacedByToken);\n            \n            if (childToken?.IsActive == true)\n            {\n                childToken.RevokedAt = DateTime.UtcNow;\n                childToken.ReasonRevoked = reason;\n                await RevokeDescendantTokensAsync(childToken, reason);\n            }\n        }\n    }\n}",
      "language": "csharp"
    },
    {
      "type": "DEEP_DIVE",
      "title": "Token Rotation and Security",
      "content": "## Why Rotate Refresh Tokens?\n\n**Token Rotation** means every time a refresh token is used, it's invalidated and a new one is issued.\n\n**Benefits:**\n1. Stolen tokens can only be used once\n2. Reuse detection: if an old token is used, we know compromise occurred\n3. We can invalidate the entire chain when reuse is detected\n\n**The Attack Scenario:**\n```\n1. User logs in, gets AT1 + RT1\n2. Attacker steals RT1\n3. User refreshes: RT1 → AT2 + RT2 (RT1 marked as rotated)\n4. Attacker tries RT1: DENIED + all descendants revoked\n5. User's RT2 now invalid too (forced re-login)\n6. Security team alerted to token reuse\n```\n\n**Clean Up Old Tokens:**\n```csharp\npublic async Task CleanupExpiredTokensAsync()\n{\n    var cutoff = DateTime.UtcNow.AddDays(-30);\n    await _context.RefreshTokens\n        .Where(rt => rt.ExpiresAt < cutoff)\n        .ExecuteDeleteAsync();\n}\n```"
    },
    {
      "type": "WARNING",
      "title": "Refresh Token Storage",
      "content": "## Where to Store Refresh Tokens\n\n**Backend (recommended):**\n- Store in database with user association\n- HttpOnly cookie for web apps\n- Secure storage on mobile (Keychain/Keystore)\n\n**Never in:**\n- localStorage (XSS vulnerable)\n- sessionStorage (XSS vulnerable)\n- URL parameters (visible in logs)\n\n**HttpOnly Cookie Setup:**\n```csharp\nResponse.Cookies.Append(\"refreshToken\", refreshToken.Token, new CookieOptions\n{\n    HttpOnly = true,\n    Secure = true,\n    SameSite = SameSiteMode.Strict,\n    Expires = refreshToken.ExpiresAt\n});\n```"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-20-05-challenge-01",
      "title": "Implement Revoke Token Endpoint",
      "description": "Create an endpoint to revoke a user's refresh token (for logout or security).",
      "instructions": "Create a POST /api/auth/revoke endpoint that:\n1. Accepts a refresh token in the request body\n2. Finds the token in the database\n3. Marks it as revoked with reason 'User requested revocation'\n4. Returns 200 OK if successful, 400 if token not found",
      "starterCode": "group.MapPost(\"/revoke\", async (\n    RevokeRequest request,\n    AppDbContext context) =>\n{\n    // TODO: Find the token\n    // TODO: Mark as revoked\n    // TODO: Save and return\n});\n\npublic record RevokeRequest(string RefreshToken);",
      "solution": "group.MapPost(\"/revoke\", async (\n    RevokeRequest request,\n    AppDbContext context) =>\n{\n    var token = await context.RefreshTokens\n        .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);\n    \n    if (token is null)\n        return Results.BadRequest(new { Error = \"Token not found\" });\n    \n    if (token.IsRevoked)\n        return Results.Ok(new { Message = \"Token was already revoked\" });\n    \n    token.RevokedAt = DateTime.UtcNow;\n    token.ReasonRevoked = \"User requested revocation\";\n    await context.SaveChangesAsync();\n    \n    return Results.Ok(new { Message = \"Token revoked successfully\" });\n})\n.RequireAuthorization()\n.WithName(\"RevokeToken\");",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Use FirstOrDefaultAsync to find the token by its value"},
        {"level": 2, "text": "Set RevokedAt = DateTime.UtcNow and ReasonRevoked = 'User requested revocation'"},
        {"level": 3, "text": "Add .RequireAuthorization() so only authenticated users can revoke"}
      ],
      "difficulty": "advanced"
    }
  ]
}
```

**Step 5: Validate and commit**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-20 Authentication with 4 additional lessons"
```

---

### Task 3: Module 21 - External Authentication (Add 3 lessons)

**Current:** 1 lesson (lesson-21-01: OAuth 2.0 and OpenID Connect)
**Target:** 4 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-21-02 - Configuring Google Authentication**

```json
{
  "id": "lesson-21-02",
  "title": "Sign In with Google",
  "moduleId": "module-21",
  "order": 2,
  "estimatedMinutes": 30,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "OAuth as a Valet Key",
      "content": "When you give your car to a valet, you might use a valet key that:\n- Opens the door and starts the car\n- But can't open the trunk or glove compartment\n- Has limited capabilities by design\n\nOAuth works the same way. When a user clicks 'Sign in with Google':\n1. ShopFlow asks Google: 'Can this user prove who they are?'\n2. Google asks user: 'Do you want to let ShopFlow know your email and name?'\n3. User says yes, Google gives ShopFlow a 'valet key' (token)\n4. This key only reveals what the user consented to - not their Gmail password"
    },
    {
      "type": "EXAMPLE",
      "title": "Setting Up Google Authentication",
      "content": "Here's how to add Google sign-in to ShopFlow:",
      "code": "// 1. Create credentials at console.cloud.google.com\n// - Create OAuth 2.0 Client ID\n// - Set redirect URI: https://localhost:5001/signin-google\n\n// 2. Add package\n// dotnet add package Microsoft.AspNetCore.Authentication.Google\n\n// 3. Store credentials securely\n// appsettings.json (development) or user secrets / Azure Key Vault (production)\n{\n  \"Authentication\": {\n    \"Google\": {\n      \"ClientId\": \"your-client-id.apps.googleusercontent.com\",\n      \"ClientSecret\": \"your-client-secret\"\n    }\n  }\n}\n\n// 4. Configure in Program.cs\nbuilder.Services.AddAuthentication(options =>\n{\n    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;\n    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;\n})\n.AddCookie()\n.AddGoogle(options =>\n{\n    options.ClientId = builder.Configuration[\"Authentication:Google:ClientId\"]!;\n    options.ClientSecret = builder.Configuration[\"Authentication:Google:ClientSecret\"]!;\n    \n    // Request additional info\n    options.Scope.Add(\"profile\");\n    \n    // Map claims from Google's response\n    options.ClaimActions.MapJsonKey(\"picture\", \"picture\");\n    \n    // Handle events\n    options.Events.OnCreatingTicket = async context =>\n    {\n        // You can add custom claims or create user here\n        var email = context.Principal?.FindFirstValue(ClaimTypes.Email);\n        var name = context.Principal?.FindFirstValue(ClaimTypes.Name);\n        \n        // Optionally create local user account\n        // await EnsureUserExistsAsync(email, name);\n    };\n});\n\n// 5. Create login endpoint\napp.MapGet(\"/auth/google\", () => Results.Challenge(\n    new AuthenticationProperties { RedirectUri = \"/\" },\n    new[] { GoogleDefaults.AuthenticationScheme }));\n\napp.MapGet(\"/auth/google/callback\", async (HttpContext context) =>\n{\n    var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);\n    if (!result.Succeeded)\n        return Results.Redirect(\"/login?error=google-auth-failed\");\n    \n    return Results.Redirect(\"/\");\n});",
      "language": "csharp"
    },
    {
      "type": "THEORY",
      "title": "The OAuth Flow",
      "content": "## What Happens When User Clicks 'Sign in with Google'\n\n```\n1. User clicks 'Sign in with Google' on ShopFlow\n   → Browser redirects to: https://accounts.google.com/oauth/authorize\n   → URL includes: client_id, redirect_uri, scope, state\n\n2. Google shows consent screen\n   → 'ShopFlow wants to access your: Name, Email, Profile picture'\n   → User clicks 'Allow'\n\n3. Google redirects back to ShopFlow\n   → URL: https://shopflow.com/signin-google?code=AUTH_CODE&state=...\n   → The auth code is a one-time, short-lived code\n\n4. ShopFlow server exchanges code for tokens\n   → POST to https://oauth2.googleapis.com/token\n   → Includes: code, client_id, client_secret, redirect_uri\n   → Receives: access_token, id_token, refresh_token\n\n5. ShopFlow validates id_token and extracts user info\n   → Creates or finds local user\n   → Issues ShopFlow session cookie\n\n6. User is logged in!\n```\n\n**Security Note:** The client_secret is NEVER exposed to the browser. The exchange in step 4 happens server-to-server."
    },
    {
      "type": "WARNING",
      "title": "OAuth Security Considerations",
      "content": "## Security Best Practices\n\n**State Parameter (CSRF Protection)**\nAlways validate the state parameter matches what you sent:\n```csharp\noptions.CorrelationCookie.SameSite = SameSiteMode.Lax;\noptions.SaveTokens = true;  // ASP.NET Core handles state automatically\n```\n\n**PKCE (Proof Key for Code Exchange)**\nFor mobile/SPA apps, use PKCE to prevent code interception:\n```csharp\noptions.UsePkce = true;\n```\n\n**Never Trust Client-Side OAuth**\nAlways exchange the code on the server, never in JavaScript.\n\n**Check Email Verified**\n```csharp\noptions.Events.OnCreatingTicket = context =>\n{\n    var emailVerified = context.User.GetProperty(\"email_verified\").GetBoolean();\n    if (!emailVerified)\n    {\n        context.Fail(\"Email not verified with Google\");\n    }\n    return Task.CompletedTask;\n};\n```"
    }
  ],
  "challenges": [
    {
      "type": "QUIZ",
      "id": "lesson-21-02-quiz-01",
      "question": "Where should the OAuth client_secret be stored?",
      "options": [
        {"id": "a", "text": "In the frontend JavaScript code", "isCorrect": false},
        {"id": "b", "text": "In the URL as a query parameter", "isCorrect": false},
        {"id": "c", "text": "In server-side configuration (secrets/environment variables)", "isCorrect": true},
        {"id": "d", "text": "In localStorage for easy access", "isCorrect": false}
      ],
      "explanation": "The client_secret must NEVER be exposed to the client (browser). It should only exist on the server side, stored securely in environment variables, Azure Key Vault, or similar secure configuration. The code exchange that uses the secret happens server-to-server.",
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 2: Add lesson-21-03 - Microsoft and GitHub Authentication**

*(Similar structure for Microsoft Azure AD and GitHub OAuth providers)*

**Step 3: Add lesson-21-04 - Linking External Logins to Local Accounts**

*(Handling users who sign up with email then want to link Google, or vice versa)*

**Step 4: Validate and commit**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-21 External Auth with 3 additional lessons"
```

---

### Task 4: Module 22 - Authorization Patterns (Add 3 lessons)

**Current:** 1 lesson (lesson-22-01: Roles, Claims, and Policies)
**Target:** 4 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-22-02 - Implementing Roles in ShopFlow**

```json
{
  "id": "lesson-22-02",
  "title": "Role-Based Authorization in Practice",
  "moduleId": "module-22",
  "order": 2,
  "estimatedMinutes": 35,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Roles as Job Titles",
      "content": "Roles work like job titles in a company:\n\n- **Admin** = Manager with master key - can access everything\n- **Customer** = Regular employee - can access public areas and their own desk\n- **Seller** = Sales team member - can access sales floor and product inventory\n\nYou don't check 'is this person named John?' - you check 'does this person have the Manager role?' This makes it easy to add new managers later without changing the access control logic."
    },
    {
      "type": "EXAMPLE",
      "title": "Implementing Roles in ShopFlow",
      "content": "Complete role implementation for the ShopFlow e-commerce platform:",
      "code": "// 1. Define roles as constants\npublic static class ShopFlowRoles\n{\n    public const string Admin = \"Admin\";\n    public const string Seller = \"Seller\";\n    public const string Customer = \"Customer\";\n    \n    public static readonly string[] All = [Admin, Seller, Customer];\n}\n\n// 2. Seed roles on startup\npublic static class DbSeeder\n{\n    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)\n    {\n        foreach (var role in ShopFlowRoles.All)\n        {\n            if (!await roleManager.RoleExistsAsync(role))\n            {\n                await roleManager.CreateAsync(new IdentityRole(role));\n            }\n        }\n    }\n}\n\n// 3. Assign role during registration\npublic async Task<RegisterResult> HandleAsync(RegisterCommand command)\n{\n    var user = new ApplicationUser { ... };\n    var result = await _userManager.CreateAsync(user, command.Password);\n    \n    if (result.Succeeded)\n    {\n        // Default role for new users\n        await _userManager.AddToRoleAsync(user, ShopFlowRoles.Customer);\n    }\n    return ...;\n}\n\n// 4. Protect endpoints with roles\napp.MapGet(\"/api/admin/dashboard\", () => \"Admin Dashboard\")\n   .RequireAuthorization(policy => policy.RequireRole(ShopFlowRoles.Admin));\n\napp.MapGet(\"/api/products/manage\", () => \"Product Management\")\n   .RequireAuthorization(policy => policy.RequireRole(ShopFlowRoles.Admin, ShopFlowRoles.Seller));\n\n// 5. Check roles in code\npublic async Task<bool> CanManageProductsAsync(ClaimsPrincipal user)\n{\n    return user.IsInRole(ShopFlowRoles.Admin) || user.IsInRole(ShopFlowRoles.Seller);\n}\n\n// 6. Include roles in JWT\npublic string GenerateToken(ApplicationUser user, IList<string> roles)\n{\n    var claims = new List<Claim> { ... };\n    \n    foreach (var role in roles)\n    {\n        claims.Add(new Claim(ClaimTypes.Role, role));\n    }\n    \n    // JWT now contains: { \"role\": [\"Customer\", \"Seller\"] }\n}",
      "language": "csharp"
    },
    {
      "type": "WARNING",
      "title": "Role Anti-Patterns",
      "content": "## Common Role Mistakes\n\n**Too Many Granular Roles**\n```csharp\n// WRONG: Role explosion\nCanEditProducts, CanDeleteProducts, CanViewProducts, CanCreateProducts...\n\n// BETTER: Use claims for granular permissions, roles for groupings\nRole: \"ProductManager\" has claims: [Products.Create, Products.Edit, Products.Delete]\n```\n\n**Hardcoded Role Checks Everywhere**\n```csharp\n// WRONG: Scattered throughout codebase\nif (user.IsInRole(\"Admin\") || user.IsInRole(\"SuperAdmin\") || user.IsInRole(\"Manager\"))\n\n// BETTER: Centralized policy\n.RequireAuthorization(\"CanManageProducts\")\n```\n\n**Storing Roles in JWT Only**\nIf you store roles only in JWT without server validation, a user's role change won't take effect until their token expires.\n\n**Not Handling Role Hierarchy**\n```csharp\n// If Admin should have all Seller permissions, either:\n// 1. Explicitly assign both roles: user.AddToRoleAsync(\"Admin\"); user.AddToRoleAsync(\"Seller\");\n// 2. Or use a policy that checks: RequireRole(\"Admin\", \"Seller\")\n```"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-22-02-challenge-01",
      "title": "Create Admin Promotion Endpoint",
      "description": "Create an endpoint that allows admins to promote users to seller role.",
      "instructions": "Create a POST /api/admin/users/{userId}/promote-to-seller endpoint that:\n1. Only admins can access\n2. Finds the user by ID\n3. Adds the 'Seller' role to the user\n4. Returns 200 OK with the user's new roles\n5. Returns 404 if user not found",
      "starterCode": "app.MapPost(\"/api/admin/users/{userId}/promote-to-seller\", async (\n    string userId,\n    UserManager<ApplicationUser> userManager) =>\n{\n    // TODO: Implement\n})\n// TODO: Add authorization",
      "solution": "app.MapPost(\"/api/admin/users/{userId}/promote-to-seller\", async (\n    string userId,\n    UserManager<ApplicationUser> userManager) =>\n{\n    var user = await userManager.FindByIdAsync(userId);\n    if (user is null)\n        return Results.NotFound(new { Error = \"User not found\" });\n    \n    if (await userManager.IsInRoleAsync(user, ShopFlowRoles.Seller))\n        return Results.Ok(new { Message = \"User is already a seller\", Roles = await userManager.GetRolesAsync(user) });\n    \n    var result = await userManager.AddToRoleAsync(user, ShopFlowRoles.Seller);\n    if (!result.Succeeded)\n        return Results.BadRequest(new { Errors = result.Errors.Select(e => e.Description) });\n    \n    var roles = await userManager.GetRolesAsync(user);\n    return Results.Ok(new { Message = \"User promoted to seller\", Roles = roles });\n})\n.RequireAuthorization(policy => policy.RequireRole(ShopFlowRoles.Admin))\n.WithName(\"PromoteToSeller\");",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Use userManager.FindByIdAsync(userId) to find the user"},
        {"level": 2, "text": "Use userManager.AddToRoleAsync(user, ShopFlowRoles.Seller) to add the role"},
        {"level": 3, "text": "Add .RequireAuthorization(policy => policy.RequireRole(ShopFlowRoles.Admin))"}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 2: Add lesson-22-03 - Claims-Based Authorization**

**Step 3: Add lesson-22-04 - Resource-Based Authorization**

**Step 4: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-22 Authorization with 3 additional lessons"
```

---

### Task 5: Module 24 - CI/CD (Add 4 lessons)

**Current:** 1 lesson (lesson-24-01: What is CI/CD?)
**Target:** 5 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-24-02 - GitHub Actions Fundamentals**

```json
{
  "id": "lesson-24-02",
  "title": "GitHub Actions: Workflow Fundamentals",
  "moduleId": "module-24",
  "order": 2,
  "estimatedMinutes": 35,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Workflows as Assembly Lines",
      "content": "GitHub Actions workflows are like factory assembly lines:\n\n**Workflow** = The entire assembly line (a .yml file)\n**Job** = A station on the line (build station, test station, deploy station)\n**Step** = A specific task at a station (compile code, run tests)\n**Runner** = The worker performing the tasks (Ubuntu, Windows, or macOS machine)\n\nJust like assembly lines, jobs can run in parallel (multiple stations working at once) or sequentially (one station must finish before the next starts)."
    },
    {
      "type": "EXAMPLE",
      "title": "Anatomy of a GitHub Actions Workflow",
      "content": "Let's break down the ShopFlow CI workflow:",
      "code": "# .github/workflows/ci.yml\nname: ShopFlow CI  # Display name in GitHub UI\n\n# TRIGGERS: When does this workflow run?\non:\n  push:\n    branches: [main, develop]  # On push to these branches\n  pull_request:\n    branches: [main]           # On PRs targeting main\n  workflow_dispatch:            # Manual trigger button\n\n# JOBS: What work gets done?\njobs:\n  build:\n    name: Build and Test\n    runs-on: ubuntu-latest  # Runner OS\n    \n    # Service containers (databases, caches)\n    services:\n      postgres:\n        image: postgres:16\n        env:\n          POSTGRES_USER: shopflow\n          POSTGRES_PASSWORD: test_password\n          POSTGRES_DB: shopflow_test\n        ports:\n          - 5432:5432\n        options: >-\n          --health-cmd pg_isready\n          --health-interval 10s\n          --health-timeout 5s\n          --health-retries 5\n    \n    # STEPS: Sequential tasks\n    steps:\n      - name: Checkout code\n        uses: actions/checkout@v4  # Pre-built action\n      \n      - name: Setup .NET\n        uses: actions/setup-dotnet@v4\n        with:\n          dotnet-version: '9.0.x'\n      \n      - name: Restore dependencies\n        run: dotnet restore  # Shell command\n      \n      - name: Build\n        run: dotnet build --no-restore --configuration Release\n      \n      - name: Run unit tests\n        run: dotnet test tests/ShopFlow.Tests.Unit --no-build -c Release\n      \n      - name: Run integration tests\n        run: dotnet test tests/ShopFlow.Tests.Integration --no-build -c Release\n        env:\n          ConnectionStrings__DefaultConnection: \"Host=localhost;Database=shopflow_test;Username=shopflow;Password=test_password\"",
      "language": "yaml"
    },
    {
      "type": "THEORY",
      "title": "Key Workflow Concepts",
      "content": "## Triggers (on:)\n\n| Trigger | When it fires |\n|---------|---------------|\n| `push` | Code pushed to specified branches |\n| `pull_request` | PR opened, updated, or synchronized |\n| `schedule` | Cron schedule (e.g., nightly builds) |\n| `workflow_dispatch` | Manual trigger from GitHub UI |\n| `release` | Release published |\n\n## Runners\n\n| Runner | Use case |\n|--------|----------|\n| `ubuntu-latest` | Most .NET workloads (fastest) |\n| `windows-latest` | Windows-specific code |\n| `macos-latest` | iOS/macOS builds |\n| Self-hosted | Custom hardware, security requirements |\n\n## Job Dependencies\n\n```yaml\njobs:\n  build:\n    runs-on: ubuntu-latest\n    steps: [...]\n  \n  test:\n    needs: build  # Waits for build to complete\n    runs-on: ubuntu-latest\n    steps: [...]\n  \n  deploy:\n    needs: [build, test]  # Waits for both\n    runs-on: ubuntu-latest\n    if: github.ref == 'refs/heads/main'  # Only on main\n    steps: [...]\n```"
    },
    {
      "type": "WARNING",
      "title": "Common Workflow Mistakes",
      "content": "## Avoid These Pitfalls\n\n**Secrets in Code**\n```yaml\n# WRONG: Hardcoded secrets\nenv:\n  DB_PASSWORD: \"actual_password\"\n\n# CORRECT: Use GitHub Secrets\nenv:\n  DB_PASSWORD: ${{ secrets.DB_PASSWORD }}\n```\n\n**Not Caching Dependencies**\n```yaml\n# Without cache: downloads packages every run (slow)\n# With cache: reuses from previous runs\n- uses: actions/cache@v4\n  with:\n    path: ~/.nuget/packages\n    key: nuget-${{ hashFiles('**/*.csproj') }}\n```\n\n**Running Tests That Modify Shared State**\nIntegration tests should use isolated databases or clean up after themselves.\n\n**Ignoring Failing Builds**\nDon't merge PRs with failing checks. Enforce branch protection:\n`Settings → Branches → Require status checks to pass`"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-24-02-challenge-01",
      "title": "Add Code Coverage to Workflow",
      "description": "Add code coverage reporting to the CI workflow.",
      "instructions": "Modify the test step to:\n1. Generate code coverage in Cobertura format\n2. Upload the coverage report as an artifact\n3. Use a minimum threshold of 80%",
      "starterCode": "- name: Run unit tests\n  run: dotnet test tests/ShopFlow.Tests.Unit --no-build -c Release",
      "solution": "- name: Run unit tests with coverage\n  run: |\n    dotnet test tests/ShopFlow.Tests.Unit \\\n      --no-build -c Release \\\n      --collect:\"XPlat Code Coverage\" \\\n      --results-directory ./coverage\n\n- name: Upload coverage report\n  uses: actions/upload-artifact@v4\n  with:\n    name: coverage-report\n    path: coverage/**/coverage.cobertura.xml\n\n- name: Check coverage threshold\n  uses: irongut/CodeCoverageSummary@v1.3.0\n  with:\n    filename: coverage/**/coverage.cobertura.xml\n    fail_below_min: true\n    thresholds: '80 90'",
      "language": "yaml",
      "hints": [
        {"level": 1, "text": "Add --collect:\"XPlat Code Coverage\" to the test command"},
        {"level": 2, "text": "Use actions/upload-artifact@v4 to save the coverage report"},
        {"level": 3, "text": "Use a coverage action like irongut/CodeCoverageSummary to check thresholds"}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 2: Add lesson-24-03 - Building and Publishing Docker Images**

**Step 3: Add lesson-24-04 - Environment-Based Deployments**

**Step 4: Add lesson-24-05 - Secrets and Security in CI/CD**

**Step 5: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-24 CI/CD with 4 additional lessons"
```

---

### Task 6: Module 26 - Capstone (Add 4 lessons)

**Current:** 1 lesson (lesson-26-01: From Development to Production)
**Target:** 5 lessons

**Files:**
- Modify: `content/courses/csharp/course.json`

**Step 1: Add lesson-26-02 - ShopFlow Feature: Product Catalog**

```json
{
  "id": "lesson-26-02",
  "title": "Building the Product Catalog API",
  "moduleId": "module-26",
  "order": 2,
  "estimatedMinutes": 60,
  "difficulty": "advanced",
  "contentSections": [
    {
      "type": "REAL_WORLD",
      "title": "Your First Complete Feature",
      "content": "Now we bring together everything you've learned to build real features for ShopFlow. In this lesson, you'll implement the Product Catalog API—complete with:\n\n- Domain entities with business rules\n- Application layer with commands and queries\n- EF Core persistence with proper configuration\n- Minimal API endpoints with validation\n- Unit and integration tests\n\nThis is how professional .NET developers work: layer by layer, test by test, building maintainable software."
    },
    {
      "type": "EXAMPLE",
      "title": "Product Catalog Implementation",
      "content": "Complete TDD implementation of the product catalog:",
      "code": "// STEP 1: Write failing test first (TDD Red)\n// tests/ShopFlow.Tests.Unit/Products/CreateProductTests.cs\n\n[Fact]\npublic async Task Handle_ValidCommand_CreatesProduct()\n{\n    // Arrange\n    var repo = new Mock<IProductRepository>();\n    var uow = new Mock<IUnitOfWork>();\n    var handler = new CreateProductHandler(repo.Object, uow.Object);\n    \n    var command = new CreateProductCommand(\n        Name: \"Wireless Mouse\",\n        Description: \"Ergonomic wireless mouse\",\n        Price: 29.99m,\n        CategoryId: 1);\n    \n    // Act\n    var result = await handler.HandleAsync(command, CancellationToken.None);\n    \n    // Assert\n    Assert.True(result > 0);\n    repo.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);\n    uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);\n}\n\n// STEP 2: Make it pass (TDD Green)\n// src/ShopFlow.Application/Products/Handlers/CreateProductHandler.cs\n\npublic class CreateProductHandler\n{\n    private readonly IProductRepository _repository;\n    private readonly IUnitOfWork _unitOfWork;\n    \n    public CreateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork)\n    {\n        _repository = repository;\n        _unitOfWork = unitOfWork;\n    }\n    \n    public async Task<int> HandleAsync(CreateProductCommand command, CancellationToken ct)\n    {\n        var product = Product.Create(\n            command.Name,\n            command.Description,\n            new Money(command.Price),\n            command.CategoryId);\n        \n        await _repository.AddAsync(product, ct);\n        await _unitOfWork.SaveChangesAsync(ct);\n        \n        return product.Id;\n    }\n}\n\n// STEP 3: Add API endpoint\n// src/ShopFlow.Api/Endpoints/ProductEndpoints.cs\n\npublic static class ProductEndpoints\n{\n    public static void MapProductEndpoints(this WebApplication app)\n    {\n        var group = app.MapGroup(\"/api/products\").WithTags(\"Products\");\n        \n        group.MapPost(\"/\", async (\n            CreateProductRequest request,\n            CreateProductHandler handler,\n            CancellationToken ct) =>\n        {\n            var command = new CreateProductCommand(\n                request.Name,\n                request.Description,\n                request.Price,\n                request.CategoryId);\n            \n            var productId = await handler.HandleAsync(command, ct);\n            \n            return Results.Created($\"/api/products/{productId}\", new { Id = productId });\n        })\n        .WithName(\"CreateProduct\")\n        .RequireAuthorization(policy => policy.RequireRole(\"Admin\", \"Seller\"));\n        \n        group.MapGet(\"/{id:int}\", async (\n            int id,\n            GetProductHandler handler,\n            CancellationToken ct) =>\n        {\n            var product = await handler.HandleAsync(new GetProductQuery(id), ct);\n            return product is null ? Results.NotFound() : Results.Ok(product);\n        })\n        .WithName(\"GetProduct\");\n        \n        group.MapGet(\"/\", async (\n            [AsParameters] ProductFilterRequest filter,\n            GetProductsHandler handler,\n            CancellationToken ct) =>\n        {\n            var products = await handler.HandleAsync(new GetProductsQuery(\n                filter.CategoryId,\n                filter.MinPrice,\n                filter.MaxPrice,\n                filter.Page,\n                filter.PageSize), ct);\n            \n            return Results.Ok(products);\n        })\n        .WithName(\"GetProducts\");\n    }\n}\n\npublic record CreateProductRequest(string Name, string Description, decimal Price, int CategoryId);\npublic record ProductFilterRequest(int? CategoryId, decimal? MinPrice, decimal? MaxPrice, int Page = 1, int PageSize = 20);",
      "language": "csharp"
    },
    {
      "type": "DEEP_DIVE",
      "title": "Pagination and Filtering",
      "content": "## Implementing Efficient Pagination\n\n```csharp\n// Application/Products/Queries/GetProductsQuery.cs\npublic record GetProductsQuery(\n    int? CategoryId,\n    decimal? MinPrice,\n    decimal? MaxPrice,\n    int Page = 1,\n    int PageSize = 20);\n\npublic record PagedResult<T>(\n    IReadOnlyList<T> Items,\n    int Page,\n    int PageSize,\n    int TotalCount,\n    int TotalPages);\n\n// Handler with efficient database querying\npublic class GetProductsHandler\n{\n    private readonly AppDbContext _context;\n    \n    public async Task<PagedResult<ProductDto>> HandleAsync(GetProductsQuery query, CancellationToken ct)\n    {\n        var queryable = _context.Products.AsNoTracking();\n        \n        // Apply filters (database-side)\n        if (query.CategoryId.HasValue)\n            queryable = queryable.Where(p => p.CategoryId == query.CategoryId);\n        if (query.MinPrice.HasValue)\n            queryable = queryable.Where(p => p.Price.Amount >= query.MinPrice);\n        if (query.MaxPrice.HasValue)\n            queryable = queryable.Where(p => p.Price.Amount <= query.MaxPrice);\n        \n        // Get total count (single COUNT query)\n        var totalCount = await queryable.CountAsync(ct);\n        \n        // Get page (OFFSET/FETCH query)\n        var items = await queryable\n            .OrderBy(p => p.Name)\n            .Skip((query.Page - 1) * query.PageSize)\n            .Take(query.PageSize)\n            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price.Amount, p.StockQuantity))\n            .ToListAsync(ct);\n        \n        return new PagedResult<ProductDto>(\n            items,\n            query.Page,\n            query.PageSize,\n            totalCount,\n            (int)Math.Ceiling(totalCount / (double)query.PageSize));\n    }\n}\n```\n\n**Performance Tips:**\n- Use `AsNoTracking()` for read-only queries\n- Project to DTOs with `Select()` to avoid loading full entities\n- Apply filters before pagination (WHERE before OFFSET)\n- Consider keyset pagination for very large datasets"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-26-02-challenge-01",
      "title": "Implement Product Search",
      "description": "Add full-text search capability to the product listing.",
      "instructions": "Extend the GetProductsQuery to include a `SearchTerm` parameter that:\n1. Searches in both Name and Description fields\n2. Is case-insensitive\n3. Filters before pagination\n4. Uses EF Core's Contains for basic search",
      "starterCode": "public record GetProductsQuery(\n    int? CategoryId,\n    decimal? MinPrice,\n    decimal? MaxPrice,\n    string? SearchTerm,  // Add this\n    int Page = 1,\n    int PageSize = 20);\n\n// Update the handler to use SearchTerm",
      "solution": "// In GetProductsHandler.HandleAsync:\nif (!string.IsNullOrWhiteSpace(query.SearchTerm))\n{\n    var term = query.SearchTerm.ToLower();\n    queryable = queryable.Where(p => \n        p.Name.ToLower().Contains(term) || \n        p.Description.ToLower().Contains(term));\n}",
      "language": "csharp",
      "hints": [
        {"level": 1, "text": "Add null/empty check with !string.IsNullOrWhiteSpace()"},
        {"level": 2, "text": "Use ToLower() on both the search term and properties for case-insensitive search"},
        {"level": 3, "text": "Use Contains() for partial matching in the Where clause"}
      ],
      "difficulty": "advanced"
    }
  ]
}
```

**Step 2: Add lesson-26-03 - ShopFlow Feature: Shopping Cart**

**Step 3: Add lesson-26-04 - ShopFlow Feature: Order Processing**

**Step 4: Add lesson-26-05 - Final Deployment and Course Completion**

**Step 5: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-26 Capstone with 4 additional lessons"
```

---

## Part B: ShopFlow Implementation

### Task 7: Restructure ShopFlow Solution for Clean Architecture

**Files:**
- Create: `src/ShopFlow.Domain/ShopFlow.Domain.csproj`
- Create: `src/ShopFlow.Application/ShopFlow.Application.csproj`
- Create: `src/ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj`
- Modify: `src/ShopFlow.Api/ShopFlow.Api.csproj`
- Modify: `ShopFlow.sln`

**Step 1: Create Domain project**

```bash
cd content/courses/csharp/capstone
mkdir -p src/ShopFlow.Domain/Entities src/ShopFlow.Domain/ValueObjects src/ShopFlow.Domain/Interfaces src/ShopFlow.Domain/Exceptions
dotnet new classlib -n ShopFlow.Domain -o src/ShopFlow.Domain -f net9.0
```

**Step 2: Create Domain entities**

Create `src/ShopFlow.Domain/Entities/Product.cs`:
```csharp
namespace ShopFlow.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Product() { }

    public static Product Create(string name, string description, Money price, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");
        if (price.Amount <= 0)
            throw new DomainException("Price must be positive");

        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            StockQuantity = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateDetails(string name, string description, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");
        if (price.Amount <= 0)
            throw new DomainException("Price must be positive");

        Name = name;
        Description = description;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be positive");
        if (quantity > StockQuantity)
            throw new DomainException("Insufficient stock");
        StockQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsInStock => StockQuantity > 0;
}
```

Create `src/ShopFlow.Domain/ValueObjects/Money.cs`:
```csharp
namespace ShopFlow.Domain.ValueObjects;

public sealed record Money(decimal Amount, string Currency = "USD")
{
    public static Money Zero => new(0);

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException("Cannot add different currencies");
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException("Cannot subtract different currencies");
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(int quantity) => new(Amount * quantity, Currency);

    public Money Multiply(decimal factor) => new(Amount * factor, Currency);

    public override string ToString() => $"{Currency} {Amount:N2}";
}
```

**Step 3: Create Application project**

```bash
mkdir -p src/ShopFlow.Application/Products/Commands src/ShopFlow.Application/Products/Queries src/ShopFlow.Application/Products/DTOs src/ShopFlow.Application/Common
dotnet new classlib -n ShopFlow.Application -o src/ShopFlow.Application -f net9.0
cd src/ShopFlow.Application
dotnet add reference ../ShopFlow.Domain/ShopFlow.Domain.csproj
```

**Step 4: Create Infrastructure project**

```bash
mkdir -p src/ShopFlow.Infrastructure/Persistence/Configurations src/ShopFlow.Infrastructure/Persistence/Repositories src/ShopFlow.Infrastructure/Identity
dotnet new classlib -n ShopFlow.Infrastructure -o src/ShopFlow.Infrastructure -f net9.0
cd src/ShopFlow.Infrastructure
dotnet add reference ../ShopFlow.Application/ShopFlow.Application.csproj
dotnet add package Microsoft.EntityFrameworkCore -v 9.0.0
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL -v 9.0.0
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 9.0.0
```

**Step 5: Update API project references**

```bash
cd src/ShopFlow.Api
dotnet add reference ../ShopFlow.Application/ShopFlow.Application.csproj
dotnet add reference ../ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj
```

**Step 6: Update solution file**

```bash
cd ../..
dotnet sln add src/ShopFlow.Domain/ShopFlow.Domain.csproj
dotnet sln add src/ShopFlow.Application/ShopFlow.Application.csproj
dotnet sln add src/ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj
```

**Step 7: Build and verify**

```bash
dotnet build
```
Expected: Build succeeded with 0 errors.

**Step 8: Commit**

```bash
git add .
git commit -m "refactor(capstone): restructure ShopFlow to Clean Architecture"
```

---

### Task 8: Implement Product CRUD API

**Files:**
- Create: `src/ShopFlow.Application/Products/Commands/CreateProductCommand.cs`
- Create: `src/ShopFlow.Application/Products/Handlers/CreateProductHandler.cs`
- Create: `src/ShopFlow.Infrastructure/Persistence/AppDbContext.cs`
- Create: `src/ShopFlow.Infrastructure/Persistence/Repositories/ProductRepository.cs`
- Modify: `src/ShopFlow.Api/Program.cs`
- Create: `src/ShopFlow.Api/Endpoints/ProductEndpoints.cs`
- Create: `tests/ShopFlow.Tests.Unit/Products/CreateProductTests.cs`

**Step 1: Write failing test**

```csharp
// tests/ShopFlow.Tests.Unit/Products/CreateProductTests.cs
using Moq;
using ShopFlow.Application.Products.Commands;
using ShopFlow.Application.Products.Handlers;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Interfaces;
using Xunit;

namespace ShopFlow.Tests.Unit.Products;

public class CreateProductTests
{
    [Fact]
    public async Task Handle_ValidCommand_CreatesProduct()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var uow = new Mock<IUnitOfWork>();
        repo.Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Callback<Product, CancellationToken>((p, _) =>
            {
                // Simulate ID assignment
                typeof(Product).GetProperty("Id")!.SetValue(p, 1);
            });

        var handler = new CreateProductHandler(repo.Object, uow.Object);

        var command = new CreateProductCommand(
            Name: "Wireless Mouse",
            Description: "Ergonomic wireless mouse",
            Price: 29.99m,
            CategoryId: 1);

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
        repo.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyName_ThrowsDomainException()
    {
        // Arrange
        var repo = new Mock<IProductRepository>();
        var uow = new Mock<IUnitOfWork>();
        var handler = new CreateProductHandler(repo.Object, uow.Object);

        var command = new CreateProductCommand(
            Name: "",
            Description: "Description",
            Price: 29.99m,
            CategoryId: 1);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() =>
            handler.HandleAsync(command, CancellationToken.None));
    }
}
```

**Step 2: Run test to verify it fails**

```bash
dotnet test tests/ShopFlow.Tests.Unit --filter "CreateProductTests"
```
Expected: FAIL (classes don't exist yet)

**Step 3: Implement to make tests pass**

Create all necessary files as shown in the code examples above.

**Step 4: Run tests again**

```bash
dotnet test tests/ShopFlow.Tests.Unit --filter "CreateProductTests"
```
Expected: PASS

**Step 5: Create integration test**

```csharp
// tests/ShopFlow.Tests.Integration/ProductEndpointTests.cs
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ShopFlow.Tests.Integration;

public class ProductEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsEmptyList_WhenNoProducts()
    {
        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var products = await response.Content.ReadFromJsonAsync<PagedResult<ProductDto>>();
        Assert.NotNull(products);
        Assert.Empty(products.Items);
    }
}
```

**Step 6: Run integration tests**

```bash
dotnet test tests/ShopFlow.Tests.Integration
```
Expected: PASS

**Step 7: Commit**

```bash
git add .
git commit -m "feat(capstone): implement Product CRUD API with tests"
```

---

### Task 9: Implement Shopping Cart API

*(Similar TDD pattern for Cart endpoints)*

### Task 10: Implement Order Processing API

*(Similar TDD pattern for Order endpoints)*

### Task 11: Implement Authentication Endpoints

*(JWT token generation, login, register, refresh tokens)*

### Task 12: Add Database Migrations

```bash
cd content/courses/csharp/capstone
dotnet ef migrations add InitialCreate -p src/ShopFlow.Infrastructure -s src/ShopFlow.Api
```

### Task 13: Final Integration and Smoke Tests

```bash
dotnet build
dotnet test
```

---

## Implementation Tracking

| Task | Description | Status |
|------|-------------|--------|
| **Part A: Module Expansion** | | |
| Task 1 | Module 18 - Clean Architecture (+3 lessons) | ⬜ |
| Task 2 | Module 20 - Authentication (+4 lessons) | ⬜ |
| Task 3 | Module 21 - External Auth (+3 lessons) | ⬜ |
| Task 4 | Module 22 - Authorization (+3 lessons) | ⬜ |
| Task 5 | Module 24 - CI/CD (+4 lessons) | ⬜ |
| Task 6 | Module 26 - Capstone (+4 lessons) | ⬜ |
| **Part B: ShopFlow Implementation** | | |
| Task 7 | Restructure to Clean Architecture | ⬜ |
| Task 8 | Implement Product CRUD API | ⬜ |
| Task 9 | Implement Shopping Cart API | ⬜ |
| Task 10 | Implement Order Processing | ⬜ |
| Task 11 | Implement Authentication | ⬜ |
| Task 12 | Add Database Migrations | ⬜ |
| Task 13 | Final Integration Tests | ⬜ |

---

## Quality Checklist

Before marking a task complete:

- [ ] All code compiles on .NET 9
- [ ] Unit tests written and passing
- [ ] Integration tests where applicable
- [ ] No hardcoded secrets or credentials
- [ ] JSON validates: `node -e "JSON.parse(...)"`
- [ ] Content sections have 200+ words where required
- [ ] Challenges have complete solutions
- [ ] Git commit with conventional message

---

*Plan created: 2025-01-01*
*Ready for execution with superpowers:subagent-driven-development*
