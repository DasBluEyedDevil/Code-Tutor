---
type: "WARNING"
title: "Common Pitfalls"
---

**Install CLI First**: Kiota is a .NET global tool. Install it with `dotnet tool install --global Microsoft.OpenApi.Kiota` before generating clients.

**Multiple NuGet Packages**: Generated clients need THREE packages: `Microsoft.Kiota.Abstractions`, `Microsoft.Kiota.Http.HttpClientLibrary`, and `Microsoft.Kiota.Serialization.Json`. Missing any will cause compile errors.

**BaseUrl is Required**: Always set `adapter.BaseUrl` before making requests. Without it, requests go nowhere or throw confusing errors.

**Indexer vs Method**: Path parameters use indexer syntax `client.Products[id]`, NOT method syntax `client.Products(id)`. This is a common mistake that causes compile errors.

**Regenerate After API Changes**: When the API spec changes, you must regenerate the client with `kiota update -o ./Client`. Forgetting this leads to runtime errors or missing endpoints. Consider adding regeneration to your CI/CD pipeline.

**NSwag Alternative**: For simpler scenarios, NSwag also generates typed clients and has a GUI (NSwagStudio). Kiota is Microsoft's newer tool optimized for large APIs and multi-language support. Choose based on your needs.

**Nullable Reference Types**: Kiota generates nullable types for optional fields. Always check for null: `product?.Name` instead of `product.Name`.