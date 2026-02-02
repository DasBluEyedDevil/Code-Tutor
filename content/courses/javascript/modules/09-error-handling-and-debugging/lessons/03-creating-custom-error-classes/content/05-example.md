---
type: "EXAMPLE"
title: "Using instanceof to Check Error Types"
---

The instanceof operator lets you handle different error types differently, enabling precise error handling.

```javascript
// Define a hierarchy of errors
class AppError extends Error {
  constructor(message, statusCode = 500) {
    super(message);
    this.name = 'AppError';
    this.statusCode = statusCode;
  }
}

class ClientError extends AppError {
  constructor(message, statusCode = 400) {
    super(message, statusCode);
    this.name = 'ClientError';
  }
}

class ServerError extends AppError {
  constructor(message) {
    super(message, 500);
    this.name = 'ServerError';
  }
}

class ValidationError extends ClientError {
  constructor(message, field) {
    super(message, 400);
    this.name = 'ValidationError';
    this.field = field;
  }
}

class NotFoundError extends ClientError {
  constructor(resource) {
    super(`${resource} not found`, 404);
    this.name = 'NotFoundError';
    this.resource = resource;
  }
}

// Using instanceof for type-specific handling
function handleError(error) {
  // Check most specific types first!
  if (error instanceof ValidationError) {
    return {
      status: error.statusCode,
      body: {
        error: 'Validation failed',
        field: error.field,
        message: error.message
      }
    };
  }
  
  if (error instanceof NotFoundError) {
    return {
      status: 404,
      body: {
        error: 'Not found',
        resource: error.resource
      }
    };
  }
  
  if (error instanceof ClientError) {
    return {
      status: error.statusCode,
      body: { error: error.message }
    };
  }
  
  if (error instanceof ServerError) {
    // Don't expose server error details to clients
    console.error('Server error:', error.message);
    return {
      status: 500,
      body: { error: 'Internal server error' }
    };
  }
  
  // Unknown error type
  console.error('Unexpected error:', error);
  return {
    status: 500,
    body: { error: 'Something went wrong' }
  };
}

// Testing
console.log(handleError(new ValidationError('Email is required', 'email')));
// { status: 400, body: { error: 'Validation failed', field: 'email', message: 'Email is required' } }

console.log(handleError(new NotFoundError('User')));
// { status: 404, body: { error: 'Not found', resource: 'User' } }

// Hierarchy check - ValidationError is also a ClientError and AppError!
let validationErr = new ValidationError('Bad input', 'name');
console.log(validationErr instanceof ValidationError); // true
console.log(validationErr instanceof ClientError);     // true
console.log(validationErr instanceof AppError);        // true
console.log(validationErr instanceof Error);           // true
```
