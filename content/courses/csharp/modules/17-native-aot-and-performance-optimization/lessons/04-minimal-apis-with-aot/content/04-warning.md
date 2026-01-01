---
type: "WARNING"
title: "Common Pitfalls"
---

## Minimal API AOT Gotchas

**Strongly Typed Hubs Not Supported**: SignalR strongly typed hubs are NOT compatible with Native AOT in .NET 9. Using them results in build warnings and runtime exceptions. Use weakly-typed hub methods instead.

**CreateSlimBuilder Excludes Features**: Unlike CreateBuilder, CreateSlimBuilder does not include all middleware by default. Features like session, static files, or routing may need explicit configuration.

**JSON Context Registration Order Matters**: Always insert your context at position 0 in TypeInfoResolverChain. If placed after the default resolver, AOT may still attempt reflection-based serialization.

**Lambda Closure Allocations**: Be mindful of closures in endpoint handlers. Capturing variables allocates memory on each request. For high-performance scenarios, use static lambdas or method groups.

**No Dynamic Model Binding**: Unlike MVC controllers, minimal APIs don't support dynamic model binding. All request/response types must be known at compile time and registered with the JSON source generator.

**OpenAPI Support in .NET 9**: ASP.NET Core 9 adds built-in OpenAPI document generation with Native AOT support via Microsoft.AspNetCore.OpenApi package.