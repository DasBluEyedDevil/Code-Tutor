---
type: "THEORY"
title: "Error Class Hierarchy and Inheritance"
---

Understanding error class inheritance is crucial for building a robust error handling system:

**Inheritance Chain:**
```
Error (built-in)
  └─ AppError (your base error)
       ├─ ClientError (4xx errors)
       │    ├─ ValidationError (400)
       │    ├─ AuthenticationError (401)
       │    ├─ AuthorizationError (403)
       │    └─ NotFoundError (404)
       └─ ServerError (5xx errors)
            ├─ DatabaseError (500)
            └─ ExternalServiceError (502/503)
```

**Benefits of Hierarchy:**

1. **Catch at any level:**
```javascript
catch (error) {
  if (error instanceof ClientError) {
    // Catches ValidationError, NotFoundError, etc.
  }
}
```

2. **Share common behavior:**
```javascript
class AppError extends Error {
  constructor(message, statusCode) {
    super(message);
    this.statusCode = statusCode;
    this.timestamp = Date.now();
  }
  
  // All subclasses get this method
  toJSON() {
    return { message: this.message, status: this.statusCode };
  }
}
```

3. **Type-safe handling:**
```javascript
// Handle specific, then general
if (error instanceof ValidationError) {
  // Most specific
} else if (error instanceof ClientError) {
  // General client errors
} else if (error instanceof AppError) {
  // All app errors
} else {
  // Unknown errors
}
```

**Design Principles:**
- Always extend from Error or your base AppError
- Keep the hierarchy shallow (2-3 levels max)
- Group errors by who can fix them (client vs server)
- Include HTTP status codes for API errors
- Add context-specific properties (field, resource, etc.)