---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`TypedResults.Ok(value)`**: Returns 200 OK with strongly-typed data. Preferred over Results.Ok() for compile-time safety and auto OpenAPI docs!

**`TypedResults.Created(uri, value)`**: Returns 201 Created. First param is URL of new resource. Used for POST. Tells client where to find the new item!

**`TypedResults.BadRequest(message)`**: Returns 400 Bad Request. Use when client sends invalid data. Include helpful error message!

**`TypedResults.NotFound()`**: Returns 404 Not Found. Resource doesn't exist. Don't return null - use proper 404!

**`TypedResults.NoContent()`**: Returns 204 No Content. Success but no data to return. Common for DELETE operations.

**`Results<T1, T2, T3> return type`**: Declare possible return types! Example: 'Results<Ok<User>, BadRequest<string>, NotFound>' tells OpenAPI all possible responses.

**`TypedResults.Problem()`**: Returns RFC 7807 Problem Details (.NET 9 enhanced!). Standard format for API errors with title, status, detail fields.