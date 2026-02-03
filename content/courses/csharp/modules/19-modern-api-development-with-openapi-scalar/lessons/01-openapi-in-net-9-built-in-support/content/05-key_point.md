---
type: "KEY_POINT"
title: "Built-in OpenAPI in .NET 9"
---

## Key Takeaways

- **`AddOpenApi()` and `MapOpenApi()` are built into .NET 9** -- no Swashbuckle or NSwag packages needed. The framework generates the OpenAPI spec from your endpoints automatically.

- **Enrich endpoints with metadata** -- `.WithName()` sets the operation ID (used for code generation), `.WithTags()` groups endpoints, `.Produces<T>()` declares return types. This metadata drives documentation and client generation.

- **The spec is available at `/openapi/v1.json`** -- tools, SDKs, and documentation UIs consume this JSON document. It is the single source of truth for your API contract.
