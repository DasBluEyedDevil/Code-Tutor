---
type: "THEORY"
title: "What is Middleware?"
---


Middleware is code that runs **BEFORE** your route handler executes. Think of it as a security checkpoint or processing station that every request must pass through.

**Common Use Cases**:
- **Logging**: Record every request and response
- **Authentication**: Verify users are logged in before accessing protected routes
- **CORS**: Add headers that allow cross-origin requests
- **Error Handling**: Catch and format errors consistently
- **Rate Limiting**: Prevent abuse by limiting requests per user

**Chain of Responsibility Pattern**:
Middleware forms a chain - each middleware can:
1. Process the request
2. Call the next handler in the chain
3. Process the response before returning

```
Request → [Logging] → [Auth] → [CORS] → Route Handler → Response
                ↓                              ↑
           (each middleware can modify request/response)
```

This pattern keeps your route handlers clean and focused on business logic, while cross-cutting concerns like logging and auth are handled in reusable middleware.

