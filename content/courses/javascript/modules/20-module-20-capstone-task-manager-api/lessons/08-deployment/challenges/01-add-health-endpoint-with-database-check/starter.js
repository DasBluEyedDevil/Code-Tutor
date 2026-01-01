// src/routes/health.ts
import { Hono } from 'hono';
import { prisma } from '../lib/db';

const health = new Hono();

// GET /health - Quick health check
health.get('/', (c) => {
  // Your code here
});

// GET /health/detailed - Comprehensive health check
health.get('/detailed', async (c) => {
  // Your code here
  // Check database with prisma.$queryRaw`SELECT 1`
  // Measure latency
  // Return appropriate status code (200 or 503)
});

export default health;