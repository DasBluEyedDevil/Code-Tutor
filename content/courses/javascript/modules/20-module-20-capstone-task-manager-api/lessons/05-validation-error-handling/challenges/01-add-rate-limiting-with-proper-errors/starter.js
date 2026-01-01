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
    // Your implementation here
  };
}