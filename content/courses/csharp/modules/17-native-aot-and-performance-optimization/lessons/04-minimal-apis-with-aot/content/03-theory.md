---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`WebApplication.CreateSlimBuilder(args)`**: Lighter than CreateBuilder(). Excludes features not needed for minimal APIs. Better for AOT size optimization.

**`ConfigureHttpJsonOptions`**: Registers your JSON context with the HTTP pipeline. Without this, serialization fails in AOT!

**`TypeInfoResolverChain.Insert(0, Context.Default)`**: Puts your source-generated context first in the resolver chain. Ensures AOT-compatible serialization is used.

**`app.MapGet("/path", handler)`**: Registers GET endpoint. Handler can be lambda or method. Return type determines response format.

**`Results.Ok(value)`**: Returns 200 OK with serialized body. Results class provides all common HTTP responses.

**`Results.Created(location, value)`**: Returns 201 Created with Location header and body. Used for POST endpoints.

**Route parameters**: `/products/{id}` - curly braces define route parameters. Lambda parameter `(int id)` receives the value.

**Query parameters**: Method parameters not in route come from query string. `?query=test&page=2`