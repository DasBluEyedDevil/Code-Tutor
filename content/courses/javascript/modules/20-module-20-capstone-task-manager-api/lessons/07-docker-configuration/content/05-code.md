---
type: "EXAMPLE"
title: "Health Checks - HEALTHCHECK Directive"
---

Configure health checks for container orchestration and load balancers:

```typescript
// Add health check endpoint to src/index.ts
import { Hono } from 'hono';
import { prisma } from './lib/db';

const app = new Hono();

// Liveness probe - is the app running?
app.get('/health/live', (c) => {
  return c.json({
    status: 'alive',
    timestamp: new Date().toISOString(),
  });
});

// Readiness probe - is the app ready to accept traffic?
app.get('/health/ready', async (c) => {
  try {
    // Check database connection
    await prisma.$queryRaw`SELECT 1`;

    return c.json(
      {
        status: 'ready',
        checks: {
          database: 'ok',
        },
        timestamp: new Date().toISOString(),
      },
      200
    );
  } catch (err) {
    return c.json(
      {
        status: 'not_ready',
        checks: {
          database: 'failed',
        },
        error: err instanceof Error ? err.message : 'Unknown error',
      },
      503 // Service Unavailable
    );
  }
});

// Startup probe - give app time to warm up
app.get('/health/startup', async (c) => {
  // Perform expensive initialization checks
  try {
    // Check all critical services
    const health = {
      database: 'checking...',
      cache: 'checking...',
    };

    return c.json(
      {
        status: 'started',
        checks: health,
      },
      200
    );
  } catch (err) {
    return c.json(
      {
        status: 'startup_failed',
        error: err instanceof Error ? err.message : 'Unknown error',
      },
      503
    );
  }
});
```
