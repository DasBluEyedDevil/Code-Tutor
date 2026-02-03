---
type: "EXAMPLE"
title: "Global Error Handler"
---

Create a centralized error handler middleware that catches all errors and formats them consistently:

```typescript
// src/middleware/error-handler.ts
import { Context, Next } from 'hono';
import { AppError } from '../lib/errors';

export interface ErrorResponse {
  success: false;
  error: {
    code: string;
    message: string;
    details?: Record<string, unknown>;
  };
}

export async function errorHandler(c: Context, next: Next) {
  try {
    await next();
  } catch (err) {
    // Log the error for monitoring
    console.error('Error:', {
      timestamp: new Date().toISOString(),
      error: err instanceof Error ? err.message : 'Unknown error',
      stack: err instanceof Error ? err.stack : undefined,
    });

    // Check if it's our custom AppError
    if (err instanceof AppError) {
      const response: ErrorResponse = {
        success: false,
        error: {
          code: err.code,
          message: err.message,
          ...(err.details && { details: err.details }),
        },
      };
      return c.json(response, err.statusCode);
    }

    // Handle Zod validation errors
    if (err instanceof Error && err.message.includes('zod')) {
      const response: ErrorResponse = {
        success: false,
        error: {
          code: 'VALIDATION_ERROR',
          message: 'Request validation failed',
          details: { raw: err.message },
        },
      };
      return c.json(response, 400);
    }

    // Catch-all for unexpected errors
    const response: ErrorResponse = {
      success: false,
      error: {
        code: 'SERVER_ERROR',
        message: 'An unexpected error occurred',
      },
    };
    return c.json(response, 500);
  }
}
```
