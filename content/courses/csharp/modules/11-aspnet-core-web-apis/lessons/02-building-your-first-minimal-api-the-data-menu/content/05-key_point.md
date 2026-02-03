---
type: "KEY_POINT"
title: "First Minimal API Patterns"
---

## Key Takeaways

- **Use union return types `Results<Ok<T>, NotFound>`** -- this tells the compiler and OpenAPI documentation exactly what your endpoint can return. No more guessing about possible responses.

- **`TypedResults.NotFound()` for missing resources** -- return proper HTTP 404 status codes instead of null. The client receives a clear signal that the resource does not exist.

- **Start with in-memory data, move to databases later** -- `List<T>` simulates a database for learning. The patterns (find, add, update, delete) are identical to what you will use with Entity Framework Core.
