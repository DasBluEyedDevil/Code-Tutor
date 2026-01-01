# Add to docker-compose.yml
services:
  redis:
    image: redis:7-alpine
    container_name: task-manager-redis
    # Your configuration here

# Add to src/middleware/rate-limit-redis.ts
import { Context, Next } from 'hono';
import { createClient } from 'redis';

const client = createClient({
  // Your configuration here
});

export function createRedisRateLimitMiddleware(
  windowMs: number = 15 * 60 * 1000,
  maxRequests: number = 100
) {
  return async (c: Context, next: Next) => {
    // Your implementation here
  };
}