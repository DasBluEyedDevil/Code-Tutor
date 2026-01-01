---
type: "CODE"
title: "Error Handling Best Practices"
---

Handle different error scenarios gracefully:

```typescript
// packages/web/src/lib/error-handler.ts
import { ApiError } from './api-client';

export function getErrorMessage(error: unknown): string {
  if (error instanceof ApiError) {
    // API-specific error
    switch (error.code) {
      case 'INVALID_CREDENTIALS':
        return 'Email or password is incorrect';
      case 'USER_NOT_FOUND':
        return 'User account not found';
      case 'EMAIL_EXISTS':
        return 'Email is already registered';
      case 'UNAUTHORIZED':
        return 'You need to log in';
      case 'FORBIDDEN':
        return "You don't have permission to do this";
      default:
        return error.message;
    }
  }

  if (error instanceof TypeError) {
    // Network error
    return 'Network error - check your connection';
  }

  if (error instanceof Error) {
    return error.message;
  }

  return 'An unknown error occurred';
}

export function isAuthError(error: unknown): boolean {
  return error instanceof ApiError && error.status === 401;
}

export function isNotFoundError(error: unknown): boolean {
  return error instanceof ApiError && error.status === 404;
}

export function isValidationError(error: unknown): boolean {
  return error instanceof ApiError && error.status === 400;
}
```
