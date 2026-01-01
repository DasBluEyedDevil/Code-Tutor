---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates Compiled Models in EF Core 9.

```csharp
// === OPTION 1: Manual Generation (Traditional) ===
// Run in terminal:
// dotnet ef dbcontext optimize --output-dir CompiledModels --namespace MyApp.CompiledModels

// This generates files like:
// CompiledModels/
//   MyDbContextModel.cs
//   ProductEntityType.cs
//   OrderEntityType.cs
//   ... (one file per entity)

// Then use in DbContext:
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        options
            // Use the compiled model!
            .UseModel(MyApp.CompiledModels.AppDbContextModel.Instance)
            .UseSqlServer(connectionString);
    }
}

// === OPTION 2: Auto-Compiled Models (EF Core 9 - RECOMMENDED!) ===
// Step 1: Install the MSBuild task package
// dotnet add package Microsoft.EntityFrameworkCore.Tasks --version 9.0.0

// Step 2: Add to your .csproj file:
// <PropertyGroup>
//   <EFOptimizeContext>true</EFOptimizeContext>
// </PropertyGroup>

// Step 3: Build your project - model auto-regenerates!
// dotnet build
// The compiled model is regenerated automatically when
// your entity types or DbContext configuration changes!

// === PERFORMANCE COMPARISON ===
// Model with 449 entities, 6390 properties, 720 relationships:
//
// Without Compiled Model: Runtime model building
// With Compiled Model:    60-80% faster startup!
//
// The bigger your model, the bigger the savings!

// === WHEN TO USE ===
// USE: Large models (100+ entities)
// USE: Microservices (fast cold start critical)
// USE: Serverless (Azure Functions, AWS Lambda)
// USE: Container orchestration (Kubernetes pods scaling)
// SKIP: Small apps (< 50 entities, overhead not worth it)
// SKIP: Rapid prototyping (still need to rebuild)

// === LIMITATIONS ===
// - Global Query Filters not compatible with compiled models
// - Lazy-loading proxies not supported
// - Must rebuild after entity changes (auto-compile helps!)
```
