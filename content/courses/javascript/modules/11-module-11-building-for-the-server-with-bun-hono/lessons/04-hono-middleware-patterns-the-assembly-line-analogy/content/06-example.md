---
type: "EXAMPLE"
title: "Error Handling Middleware"
---

Hono provides a built-in error handling mechanism using app.onError() that catches any errors thrown in your routes or middleware. This centralized error handling pattern ensures consistent error responses across your entire API and prevents sensitive error details from leaking to clients in production.

```javascript
// Hono Error Handling Middleware (2025)
// Centralized error handling for robust APIs

import { Hono } from 'hono';
import { HTTPException } from 'hono/http-exception';

const app = new Hono();

// CUSTOM ERROR CLASSES
class ValidationError extends Error {
  constructor(message, field) {
    super(message);
    this.name = 'ValidationError';
    this.field = field;
    this.statusCode = 400;
  }
}

class NotFoundError extends Error {
  constructor(resource) {
    super(`${resource} not found`);
    this.name = 'NotFoundError';
    this.statusCode = 404;
  }
}

class UnauthorizedError extends Error {
  constructor(message = 'Authentication required') {
    super(message);
    this.name = 'UnauthorizedError';
    this.statusCode = 401;
  }
}

// GLOBAL ERROR HANDLER
// Catches ALL errors thrown in routes and middleware
app.onError((err, c) => {
  console.error(`[ERROR] ${err.name}: ${err.message}`);
  
  // Handle HTTPException (Hono's built-in)
  if (err instanceof HTTPException) {
    return c.json({
      error: err.message,
      status: err.status
    }, err.status);
  }
  
  // Handle custom errors
  if (err instanceof ValidationError) {
    return c.json({
      error: 'Validation Error',
      message: err.message,
      field: err.field
    }, 400);
  }
  
  if (err instanceof NotFoundError) {
    return c.json({
      error: 'Not Found',
      message: err.message
    }, 404);
  }
  
  if (err instanceof UnauthorizedError) {
    return c.json({
      error: 'Unauthorized',
      message: err.message
    }, 401);
  }
  
  // Handle unknown errors (don't leak details in production!)
  const isDev = process.env.NODE_ENV !== 'production';
  
  return c.json({
    error: 'Internal Server Error',
    message: isDev ? err.message : 'Something went wrong',
    ...(isDev && { stack: err.stack })
  }, 500);
});

// NOT FOUND HANDLER
// Catches requests that don't match any route
app.notFound((c) => {
  return c.json({
    error: 'Not Found',
    message: `Route ${c.req.method} ${c.req.path} not found`,
    hint: 'Check the API documentation for available endpoints'
  }, 404);
});

// ERROR-THROWING MIDDLEWARE
// Wrap async operations with try/catch
const asyncHandler = (fn) => async (c, next) => {
  try {
    await fn(c, next);
  } catch (error) {
    throw error; // Let onError handle it
  }
};

// ROUTES THAT THROW ERRORS

// Validation error example
app.post('/api/users', async (c) => {
  const body = await c.req.json();
  
  if (!body.email) {
    throw new ValidationError('Email is required', 'email');
  }
  
  if (!body.email.includes('@')) {
    throw new ValidationError('Invalid email format', 'email');
  }
  
  return c.json({ message: 'User created', email: body.email }, 201);
});

// Not found error example
app.get('/api/users/:id', (c) => {
  const id = c.req.param('id');
  const users = { '1': { name: 'Alice' }, '2': { name: 'Bob' } };
  
  if (!users[id]) {
    throw new NotFoundError('User');
  }
  
  return c.json(users[id]);
});

// Using HTTPException (Hono's built-in)
app.get('/api/admin', (c) => {
  const isAdmin = false;
  
  if (!isAdmin) {
    throw new HTTPException(403, { message: 'Admin access required' });
  }
  
  return c.json({ secret: 'admin data' });
});

// Simulating unexpected error
app.get('/api/crash', (c) => {
  // This will be caught by onError
  throw new Error('Unexpected database connection failed!');
});

// TRY-CATCH IN MIDDLEWARE
app.use('/api/risky/*', async (c, next) => {
  try {
    // Some risky operation
    const result = await someRiskyOperation();
    c.set('riskyData', result);
    await next();
  } catch (error) {
    // Handle or transform the error
    console.error('Risky operation failed:', error.message);
    throw new Error('Failed to process request: ' + error.message);
  }
});

export default app;

// ERROR HANDLING BEST PRACTICES:
// 1. Use custom error classes for different error types
// 2. Always use onError() for centralized handling
// 3. Don't leak stack traces in production
// 4. Log errors for debugging
// 5. Return consistent error response format
// 6. Use notFound() for 404 handling
```
