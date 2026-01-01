---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`app.MapPost(route, handler)`**: Handles POST requests (create). Handler receives request body as parameter. ASP.NET Core auto-deserializes JSON to object!

**`TypedResults.Created(location, value)`**: Returns 201 Created with strongly-typed response. First param is URL of new resource. Second is the created object. Standard for POST!

**`app.MapPut(route, handler)`**: Handles PUT requests (update). Typically receives ID in route and updated object in body: (int id, Product product) => ...

**`TypedResults.NoContent()`**: Returns 204 No Content. Common for DELETE - operation succeeded but no data to return. TypedResults version for better OpenAPI docs!