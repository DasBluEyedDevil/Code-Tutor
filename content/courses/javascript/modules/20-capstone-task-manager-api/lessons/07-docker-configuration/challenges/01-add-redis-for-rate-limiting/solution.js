# Add to docker-compose.yml
services:
  redis:
    image: redis:7-alpine
    container_name: task-manager-redis
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data
    networks:
      - task-manager-network
    healthcheck:
      test: ["CMD", "redis-cli", "ping"]
      interval: 10s
      timeout: 5s
      retries: 5

volumes:
  redis_data:

# Add to src/middleware/rate-limit-redis.ts
import { Context, Next } from 'hono';
import { createClient } from 'redis';
import { TooManyRequestsError } from '../lib/errors';

const redisClient = createClient({
  socket: {
    host: process.env.REDIS_HOST || 'redis',
    port: parseInt(process.env.REDIS_PORT || '6379'),
  },
});

redisClient.on('error', (err) => console.error('Redis error:', err));
redisClient.connect();

export function createRedisRateLimitMiddleware(
  windowMs: number = 15 * 60 * 1000,
  maxRequests: number = 100
) {
  return async (c: Context, next: Next) => {
    const userId = c.get('userId');

    if (!userId) {
      await next();
      return;
    }

    const key = `ratelimit:${userId}`;
    const current = await redisClient.incr(key);

    if (current === 1) {
      // First request in window, set expiry
      await redisClient.expire(key, Math.ceil(windowMs / 1000));
    }

    if (current > maxRequests) {
      const ttl = await redisClient.ttl(key);
      c.header('Retry-After', String(Math.max(ttl, 1)));
      throw new TooManyRequestsError(ttl);
    }

    c.header('X-RateLimit-Limit', String(maxRequests));
    c.header('X-RateLimit-Remaining', String(maxRequests - current));

    await next();
  };
}