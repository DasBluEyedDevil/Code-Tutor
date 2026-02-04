---
type: KEY_POINT
---

- `RequestContext` provides access to the incoming HTTP request, including method, headers, query parameters, and body
- Parse JSON request bodies with `await request.json()` and always validate the structure before using the data
- Return responses with `Response.json(body: {...})` for JSON or `Response(body: 'text')` for plain text
- Use `request.method` to branch logic: handle GET, POST, PUT, DELETE in a single route handler with a switch statement
- Set appropriate HTTP status codes (`200`, `201`, `400`, `404`) to communicate success or failure to clients
