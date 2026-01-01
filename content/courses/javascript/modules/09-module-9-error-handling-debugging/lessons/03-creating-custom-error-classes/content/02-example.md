---
type: "EXAMPLE"
title: "Extending the Error Class Properly"
---

The correct way to create custom error classes in JavaScript, with all necessary setup for proper error behavior.

```javascript
// Basic custom error class
class CustomError extends Error {
  constructor(message) {
    super(message); // Call parent constructor with message
    this.name = 'CustomError'; // Set the error name
    
    // This line is needed for proper stack traces in some environments
    if (Error.captureStackTrace) {
      Error.captureStackTrace(this, CustomError);
    }
  }
}

// Using the custom error
try {
  throw new CustomError('Something custom went wrong');
} catch (error) {
  console.log(error.name);    // 'CustomError'
  console.log(error.message); // 'Something custom went wrong'
  console.log(error instanceof CustomError); // true
  console.log(error instanceof Error);       // true (inherits from Error)
}

// More realistic example with application context
class ApplicationError extends Error {
  constructor(message) {
    super(message);
    this.name = 'ApplicationError';
    this.timestamp = new Date().toISOString();
  }
}

try {
  throw new ApplicationError('Application initialization failed');
} catch (error) {
  console.log(`[${error.timestamp}] ${error.name}: ${error.message}`);
  // Output: [2024-01-15T10:30:00.000Z] ApplicationError: Application initialization failed
}
```
