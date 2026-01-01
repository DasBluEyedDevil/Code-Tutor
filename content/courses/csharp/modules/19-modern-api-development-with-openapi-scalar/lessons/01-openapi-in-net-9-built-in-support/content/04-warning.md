---
type: "WARNING"
title: "Common Pitfalls"
---

**No UI Included**: Unlike Swashbuckle, Microsoft.AspNetCore.OpenApi only generates the OpenAPI document - it does NOT include Swagger UI or any visualization. You need Scalar.AspNetCore or another UI package to view the documentation interactively.

**TypedResults vs IResult**: When using `TypedResults` (like `TypedResults.Ok()`), OpenAPI metadata is automatically inferred. But when returning `IResult` or using `Results.Ok()`, you MUST manually add `.Produces<T>()` or the response types won't appear in documentation.

**NSwag Still Has Value**: While .NET 9's built-in OpenAPI is great for document generation, NSwag still excels at client code generation. Consider using both: Microsoft.AspNetCore.OpenApi for the spec, NSwag/Kiota for generating typed clients.

**Controllers Need Extra Work**: The built-in OpenAPI works best with Minimal APIs. For controllers, you may still need `[ProducesResponseType]` attributes and additional configuration for complete documentation.

**Breaking Change from Swashbuckle**: If migrating from Swashbuckle, note that `AddSwaggerGen()` and `UseSwagger()` are replaced by `AddOpenApi()` and `MapOpenApi()`. The configuration patterns are different - transformers replace operation filters.