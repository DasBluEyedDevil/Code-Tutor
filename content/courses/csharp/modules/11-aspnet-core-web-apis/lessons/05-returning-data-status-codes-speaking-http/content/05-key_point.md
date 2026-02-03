---
type: "KEY_POINT"
title: "HTTP Status Codes in APIs"
---

## Key Takeaways

- **Use correct status codes** -- 200 OK for success, 201 Created for new resources, 204 No Content for successful deletions, 400 Bad Request for invalid input, 404 Not Found for missing resources.

- **`TypedResults.Problem()` returns RFC 7807 error details** -- the standard `ProblemDetails` format includes title, status, and detail fields. Clients can parse errors consistently across all your endpoints.

- **Declare return types with `Results<T1, T2, T3>`** -- explicitly listing possible return types enables compile-time checking and generates complete OpenAPI documentation for every response scenario.
