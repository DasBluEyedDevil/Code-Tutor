// src/middleware/rate-limit.ts
import { Context, Next } from 'hono';
import { AppError } from '../lib/errors';

export class TooManyRequestsError extends AppError {
  constructor(retryAfter: number = 60) {
    super(
      'RATE_LIMIT_EXCEEDED',
      `Too many requests. Please try again in ${retryAfter} seconds.`,
      429
    );
  }
}

interface RateLimitRecord {
  count: number;
  resetTime: number;
}

const rateLimitStore: Map<string, RateLimitRecord> = new Map();

export function createRateLimitMiddleware(
  windowMs: number = 15 * 60 * 1000,
  maxRequests: number = 100
) {
  return async (c: Context, next: Next) => {
    const userId = c.get('userId');

    if (!userId) {
      // Not authenticated, skip rate limiting for now
      await next();
      return;
    }

    const now = Date.now();
    const record = rateLimitStore.get(userId);

    if (record && now < record.resetTime) {
      record.count++;

      if (record.count > maxRequests) {
        const retryAfter = Math.ceil((record.resetTime - now) / 1000);
        c.header('Retry-After', String(retryAfter));
        throw new TooManyRequestsError(retryAfter);
      }
    } else {
      // Create new window
      rateLimitStore.set(userId, {
        count: 1,
        resetTime: now + windowMs,
      });
    }

    // Clean up expired windows
    for (const [key, record] of rateLimitStore.entries()) {
      if (record.resetTime < now) {
        rateLimitStore.delete(key);
      }
    }

    await next();
  };
}