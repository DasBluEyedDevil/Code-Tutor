---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`TypedResults.Ok(value)`**: Returns 200 OK with strongly-typed response. Prefer over Results.Ok() for: compile-time safety, better unit tests, auto OpenAPI metadata!

**`Results<Ok<T>, NotFound>`**: Union return type! Tells compiler AND OpenAPI exactly what this endpoint can return. No more guessing!

**`TypedResults.NotFound()`**: Returns 404 Not Found status. Use when resource doesn't exist. TypedResults version generates accurate API documentation!

**`FirstOrDefault() with null check`**: LINQ method returns first match or null. Use 'is not null' pattern matching to check. Modern C# syntax!

**`In-memory data store`**: List<T> simulates database for learning. Changes persist during app lifetime. Restarting app resets data. Use real DB in production!