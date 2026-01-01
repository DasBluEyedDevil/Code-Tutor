---
type: "CODE"
title: "Health Endpoint with Database Check"
---

Create a comprehensive health check endpoint that monitors your application and database:

This endpoint is used by hosting platforms for uptime monitoring and load balancer health checks.

```typescript
// src/routes/health.ts
import { Hono } from 'hono';
import { prisma } from '../lib/db';

const health = new Hono();

interface HealthResponse {
  status: 'ok' | 'degraded' | 'error';
  timestamp: string;
  uptime: number;
  database: {
    status: 'connected' | 'disconnected';
    latency_ms: number;
  };
  version: string;
}

// GET /health - Quick health check
health.get('/', (c) => {
  return c.json({
    status: 'ok',
    timestamp: new Date().toISOString(),
  }, 200);
});

// GET /health/detailed - Comprehensive health check
health.get('/detailed', async (c) => {
  const startTime = Date.now();
  const response: HealthResponse = {
    status: 'ok',
    timestamp: new Date().toISOString(),
    uptime: process.uptime(),
    database: {
      status: 'disconnected',
      latency_ms: 0,
    },
    version: '1.0.0',
  };

  try {
    // Check database connectivity
    const dbStartTime = Date.now();
    await prisma.$queryRaw`SELECT 1`;
    response.database.status = 'connected';
    response.database.latency_ms = Date.now() - dbStartTime;
  } catch (error) {
    response.status = 'degraded';
    response.database.status = 'disconnected';
  }

  const statusCode = response.status === 'ok' ? 200 : 503;
  return c.json(response, statusCode);
});

export default health;
```
