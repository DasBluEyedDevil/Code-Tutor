# C# Course Audit Report - .NET 9 / C# 13 Compliance

**Audit Date**: 2025-12-29
**Auditor**: .NET Core Architect
**Target Framework**: .NET 9 / C# 13

---

## Executive Summary

The C# course contains **15 modules** covering fundamentals through advanced topics. While the course demonstrates many modern practices (top-level statements, Minimal APIs, Blazor), it requires updates for full .NET 9 / C# 13 compliance.

| Category | Findings |
|----------|----------|
| ✅ Compliant | Top-level statements, Primary constructors, Minimal APIs, Blazor, DI |
| 🔴 Anti-patterns | Newtonsoft.Json (5 instances), using with braces (8+ instances) |
| 🔴 Missing | params IEnumerable<T>, CountBy/AggregateBy, System.Text.Json |

---

## Phase 1: .NET 9 Freshness Check

### Syntax Validation Results

#### ✅ Modern Practices Found

| Feature | Status | Evidence |
|---------|--------|----------|
| Top-level statements | ✅ | All examples use minimal Program.cs |
| Primary constructors | ✅ | Module 7: `class Car(string make, int year)` |
| Nullable reference types | ✅ | Module 2: `string?`, `??`, `?.` operators |
| Collection expressions | ✅ | Module 5: `int[] nums = [1, 2, 3]` |
| Pattern matching | ✅ | Used in control flow examples |

#### 🔴 Anti-Patterns Found

##### 1. Newtonsoft.Json Usage (CRITICAL)

**Location**: Module 8, JSON/NuGet lesson
**Lines**: 4388, 4418, 4436

```csharp
// CURRENT (Anti-pattern)
using Newtonsoft.Json;
string json = JsonConvert.SerializeObject(books, Formatting.Indented);
var loaded = JsonConvert.DeserializeObject<List<Book>>(json);
```

**Required Fix**:
```csharp
// MODERN (.NET 9)
using System.Text.Json;
string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
var loaded = JsonSerializer.Deserialize<List<Book>>(json);
```

##### 2. Using Statements with Braces (MEDIUM)

**Locations**: Modules 9-12
**Lines**: 5234, 5453, 6477, 6599, 6715, 6837

```csharp
// CURRENT (Verbose)
using (var stream = File.OpenRead("file.txt"))
{
    // code
}

// MODERN (Minimal)
using var stream = File.OpenRead("file.txt");
// code - disposal at end of scope
```

#### 🔴 Missing C# 13 Features

##### 1. params IEnumerable<T> (NEW in C# 13)

```csharp
// C# 13: params works with any collection type!
void PrintAll(params IEnumerable<string> items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}

// Flexible calling
PrintAll("a", "b", "c");           // inline
PrintAll(["one", "two", "three"]); // collection expression
PrintAll(myList);                   // existing collection
```

**Recommendation**: Add to Module 6 (Methods and Functions)

##### 2. CountBy() and AggregateBy() (.NET 9 LINQ)

```csharp
var products = new[]
{
    new { Name = "Apple", Category = "Fruit", Price = 1.50m },
    new { Name = "Banana", Category = "Fruit", Price = 0.75m },
    new { Name = "Carrot", Category = "Vegetable", Price = 0.50m }
};

// .NET 9: CountBy - count items by key
var countByCategory = products.CountBy(p => p.Category);
// Result: { Fruit: 2, Vegetable: 1 }

// .NET 9: AggregateBy - aggregate by key
var totalByCategory = products.AggregateBy(
    p => p.Category,
    seed: 0m,
    (total, p) => total + p.Price);
// Result: { Fruit: 2.25, Vegetable: 0.50 }
```

**Recommendation**: Add to Module 9 (LINQ)

---

## Phase 2: Full Stack Gap Analysis

### ✅ Topics Covered

| Topic | Module | Coverage |
|-------|--------|----------|
| Minimal APIs | 11 | Comprehensive (MapGet, MapPost, routing) |
| Blazor | 13, 14 | Server, WASM, Auto modes, components |
| Dependency Injection | 11 | AddSingleton, AddScoped, AddTransient |
| .NET Aspire | 14 | Service discovery, dashboards |
| Entity Framework Core | 12 | CRUD, relationships, migrations |
| HybridCache | 12 | .NET 9 caching (L1+L2) |
| xUnit Testing | 15 | Facts, Theories, mocking |

### 🔴 Missing Modules

| Topic | Gap Analysis | Priority |
|-------|--------------|----------|
| **OpenAPI / Scalar** | No API documentation tooling | HIGH |
| **Native AOT** | No ahead-of-time compilation | MEDIUM |
| **Cloud-Native Patterns** | Limited resilience/observability | HIGH |

---

## Deliverable 1: Refactoring Checklist

### Critical (Must Fix)

- [ ] Replace all `Newtonsoft.Json` with `System.Text.Json` (Module 8)
- [ ] Add `params IEnumerable<T>` lesson (Module 6)
- [ ] Add `.CountBy()` and `.AggregateBy()` examples (Module 9)

### High Priority

- [ ] Replace `using (...){}` with `using var` (Modules 9-12)
- [ ] Expand primary constructor examples (Module 7)
- [ ] Add `Lock` object improvements (Module 10)

### Medium Priority

- [ ] Add `\e` escape sequence mention (Module 1)
- [ ] Add implicit indexer access in object initializers (Module 5)
- [ ] Update EF Core examples to use compiled models (Module 12)

---

## Deliverable 2: Three New Modules

### Module 16: Building Cloud-Native Apps with .NET Aspire

**Description**: Orchestrate distributed applications with .NET Aspire. Learn service discovery, centralized dashboards, OpenTelemetry integration, and production deployment patterns.

**Lessons**:
1. What is .NET Aspire? (The Orchestration Layer)
2. Service Discovery & Communication
3. Observability: Logs, Metrics, Traces (OpenTelemetry)
4. Resilience Patterns (Polly, Circuit Breakers)
5. Deploying to Azure Container Apps

**Key Code Example**:
```csharp
// AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder.AddPostgres("db").AddDatabase("catalog");

var api = builder.AddProject<Projects.CatalogApi>("api")
    .WithReference(cache)
    .WithReference(db);

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(api);

builder.Build().Run();
```

---

### Module 17: Native AOT and Performance Optimization

**Description**: Build lightning-fast applications with Native AOT compilation. Learn trimming, source generators, and performance profiling for minimal deployments.

**Lessons**:
1. What is Native AOT? (No JIT Required!)
2. Enabling AOT in Your Projects
3. Source Generators for AOT (JSON, Regex)
4. Minimal APIs with AOT
5. Benchmarking with BenchmarkDotNet

**Key Code Example**:
```csharp
// Project file
<PropertyGroup>
    <PublishAot>true</PublishAot>
    <InvariantGlobalization>true</InvariantGlobalization>
</PropertyGroup>

// AOT-compatible JSON
[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
internal partial class AppJsonContext : JsonSerializerContext { }

// Usage
var json = JsonSerializer.Serialize(product, AppJsonContext.Default.Product);
```

---

### Module 18: Modern API Development with OpenAPI & Scalar

**Description**: Document and test your APIs professionally. Learn OpenAPI specification, Scalar UI, API versioning, and generating type-safe clients.

**Lessons**:
1. OpenAPI in .NET 9 (Built-in Support)
2. Scalar: Modern API Documentation UI
3. API Versioning Strategies
4. Generating Typed Clients (Kiota)
5. API Security Documentation

**Key Code Example**:
```csharp
var builder = WebApplication.CreateBuilder(args);

// .NET 9 built-in OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Scalar UI (modern alternative to Swagger)
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/products", () => products)
   .WithName("GetProducts")
   .WithDescription("Returns all products")
   .WithTags("Products");
```

---

## Module Title Discrepancy Note

The current course has misaligned module titles:
- Module 9 says "Exception Handling" but teaches LINQ
- Module 11 says "LINQ and Query Expressions" but teaches ASP.NET Core
- Module 13 says "Generics and Advanced Types" but teaches Blazor

**Recommendation**: Rename modules to match actual lesson content.

---

## Conclusion

The course provides solid coverage of modern C# development but requires:
1. **5 refactoring tasks** for C# 13 syntax compliance
2. **3 new modules** for cloud-native completeness
3. **Module title corrections** for accuracy

Total estimated effort: Refactoring (2-3 hours), New modules (8-10 hours each)
