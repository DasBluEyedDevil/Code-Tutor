---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Route order matters**: /api/todos/completed MUST come BEFORE /api/todos/{id}! Otherwise {id} matches 'completed' as a string ID. Specific routes before parameterized ones!

**TypedResults vs Results**: Both work, but TypedResults provides compile-time checking and automatic OpenAPI documentation. Use TypedResults in .NET 7+!

**Results<T1, T2> return type**: When using TypedResults with multiple possible returns, declare the union type explicitly: Results<Ok<Todo>, NotFound>. This enables full OpenAPI support.

**Null checks with FirstOrDefault**: Always check for null! FirstOrDefault returns null if not found. Accessing properties on null = NullReferenceException crash!