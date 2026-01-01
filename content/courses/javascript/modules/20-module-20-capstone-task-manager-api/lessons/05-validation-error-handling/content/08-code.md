---
type: "CODE"
title: "Practical Example: Rate Limiting with Errors"
---

Implement rate limiting that returns proper error responses:

```typescript
// src/middleware/rate-limit.ts
import { Context, Next } from 'hono';
import { TooManyRequestsError } from '../lib/errors';

interface RateLimitStore {
  [key: string]: { count: number; resetTime: number };
}

const store: RateLimitStore = {};

export class TooManyRequestsError extends AppError {
  constructor(retryAfter: number = 60) {
    super(
      'RATE_LIMIT_EXCEEDED',
      `Too many requests. Please try again in ${retryAfter} seconds.`,
      429
    );
    this.name = 'TooManyRequestsError';
  }
}

export function rateLimitMiddleware(
  windowMs: number = 15 * 60 * 1000, // 15 minutes
  maxRequests: number = 100
) {
  return async (c: Context, next: Next) => {
    const ip = c.req.header('x-forwarded-for') || 'unknown';
    const key = `${ip}:${c.req.path}`;
    const now = Date.now();

    const record = store[key];

    if (record && now < record.resetTime) {
      record.count++;

      if (record.count > maxRequests) {
        const retryAfter = Math.ceil((record.resetTime - now) / 1000);
        c.header('Retry-After', String(retryAfter));
        throw new TooManyRequestsError(retryAfter);
      }
    } else {
      // Create new window
      store[key] = {
        count: 1,
        resetTime: now + windowMs,
      };
    }

    // Clean up old entries
    for (const k in store) {
      if (store[k].resetTime < now) {
        delete store[k];
      }
    }

    await next();
  };
}

// Usage in main app:
// app.use('*', rateLimitMiddleware(15 * 60 * 1000, 100));
// Returns proper error with Retry-After header when limit exceeded
```
