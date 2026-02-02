---
type: "EXAMPLE"
title: "Error Response Format Utility"
---

Create utility functions for building consistent success and error responses:

```typescript
// src/lib/response.ts
import { Context } from 'hono';
import { AppError } from './errors';

export interface SuccessResponse<T> {
  success: true;
  data: T;
}

export interface ErrorResponseBody {
  success: false;
  error: {
    code: string;
    message: string;
    details?: Record<string, unknown>;
  };
}

export function successResponse<T>(data: T): SuccessResponse<T> {
  return {
    success: true,
    data,
  };
}

export function errorResponse(
  code: string,
  message: string,
  details?: Record<string, unknown>
): ErrorResponseBody {
  return {
    success: false,
    error: {
      code,
      message,
      ...(details && { details }),
    },
  };
}

// Throw errors with this function for consistency
export function throwError(
  c: Context,
  code: string,
  message: string,
  statusCode: number = 400,
  details?: Record<string, unknown>
) {
  const error = new AppError(code, message, statusCode, details);
  throw error;
}
```
