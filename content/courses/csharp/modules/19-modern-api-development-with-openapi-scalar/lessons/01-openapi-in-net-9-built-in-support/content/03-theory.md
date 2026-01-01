---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`builder.Services.AddOpenApi()`**: Registers OpenAPI services in the DI container. This is the .NET 9 built-in method - no external packages needed! Analyzes your endpoints at startup.

**`app.MapOpenApi()`**: Exposes the OpenAPI specification at `/openapi/v1.json`. Clients and tools can fetch this document to understand your API.

**`.WithName("GetProducts")`**: Sets the operationId in OpenAPI. Used for code generation - this becomes the method name in generated clients.

**`.WithDescription("...")`**: Human-readable description shown in documentation UIs. Explain what the endpoint does, not how.

**`.WithTags("Products")`**: Groups endpoints in the documentation. All 'Products' endpoints appear together in Swagger/Scalar UI.

**`.Produces<T>(statusCode)`**: Declares what the endpoint returns. T is the response type, statusCode is the HTTP status. Enables accurate documentation.

**`.Accepts<T>(contentType)`**: Declares what request body the endpoint expects. Used for POST/PUT methods.

**`StatusCodes.Status200OK`**: Strongly-typed status codes. Use these instead of magic numbers (200, 201, 404) for clarity.