---
type: "EXAMPLE"
title: "Adding Custom Properties"
---

Custom errors become powerful when you add properties like status codes, error codes, and contextual details.

```javascript
// Custom error with additional properties
class APIError extends Error {
  constructor(message, statusCode, errorCode = null) {
    super(message);
    this.name = 'APIError';
    this.statusCode = statusCode;   // HTTP status code
    this.errorCode = errorCode;      // Application-specific code
    this.timestamp = new Date().toISOString();
  }
  
  // Helper method to convert to JSON for API responses
  toJSON() {
    return {
      error: {
        name: this.name,
        message: this.message,
        statusCode: this.statusCode,
        errorCode: this.errorCode,
        timestamp: this.timestamp
      }
    };
  }
}

// Using the APIError
function fetchUserData(userId) {
  if (userId < 0) {
    throw new APIError(
      'User ID must be positive',
      400,           // Bad Request
      'INVALID_USER_ID'
    );
  }
  // ... fetch logic
}

try {
  fetchUserData(-1);
} catch (error) {
  if (error instanceof APIError) {
    console.log('Status:', error.statusCode);   // 400
    console.log('Code:', error.errorCode);       // INVALID_USER_ID
    console.log('Response:', JSON.stringify(error.toJSON(), null, 2));
  }
}

// Error with detailed context
class DatabaseError extends Error {
  constructor(message, details = {}) {
    super(message);
    this.name = 'DatabaseError';
    this.query = details.query || null;
    this.table = details.table || null;
    this.operation = details.operation || null;
    this.originalError = details.originalError || null;
  }
}

try {
  throw new DatabaseError('Failed to insert record', {
    query: 'INSERT INTO users VALUES (...)',
    table: 'users',
    operation: 'INSERT',
    originalError: new Error('Duplicate key violation')
  });
} catch (error) {
  console.log('Database error on table:', error.table);
  console.log('Failed query:', error.query);
  console.log('Original cause:', error.originalError?.message);
}
```
