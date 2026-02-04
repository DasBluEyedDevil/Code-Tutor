---
type: "EXAMPLE"
title: "Code Breakdown"
---


### The Validation Flow


### Error Response Examples

**Validation Error (400 Bad Request)**:

**Not Found Error (404)**:

**Conflict Error (409)**:

### Key Design Patterns

1. **Exception Hierarchy**: Sealed class ensures type safety and exhaustive handling
2. **Validation Result Accumulation**: Collects all errors instead of failing on first
3. **Reusable Validators**: Abstract base class with common validation logic
4. **Service Layer Validation**: Keeps routes thin, concentrates logic
5. **Result<T> Pattern**: Type-safe success/failure handling
6. **Global Error Handling**: StatusPages plugin provides consistent error responses
7. **Never Expose Internals**: Generic messages for unexpected errors, detailed logs server-side

---



```json
{
  "success": false,
  "message": "A book with title 'The Hobbit' by J.R.R. Tolkien already exists",
  "timestamp": "2025-01-15T10:32:10.789"
}
```
