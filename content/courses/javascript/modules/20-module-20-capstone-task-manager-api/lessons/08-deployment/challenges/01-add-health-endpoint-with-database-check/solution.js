// src/routes/health.ts
import { Hono } from 'hono';
import { prisma } from '../lib/db';

const health = new Hono();

interface HealthResponse {
  status: 'ok' | 'degraded';
  timestamp: string;
  uptime: number;
  database: {
    status: 'connected' | 'disconnected';
    latency_ms: number;
  };
  version: string;
}

health.get('/', (c) => {
  return c.json({
    status: 'ok',
    timestamp: new Date().toISOString(),
  }, 200);
});

health.get('/detailed', async (c) => {
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