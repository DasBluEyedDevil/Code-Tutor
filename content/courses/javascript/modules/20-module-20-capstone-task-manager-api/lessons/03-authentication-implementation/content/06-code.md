---
type: "CODE"
title: "Wire Up Auth Routes"
---

Add the auth routes to your main application:

```typescript
// src/index.ts (updated)
import { Hono } from 'hono';
import { cors } from 'hono/cors';
import { logger } from 'hono/logger';
import auth from './routes/auth';

const app = new Hono();

// Middleware
app.use('*', logger());
app.use('*', cors());

// Health check
app.get('/', (c) => {
  return c.json({
    status: 'ok',
    message: 'Task Manager API',
    version: '1.0.0',
  });
});

// Routes
app.route('/api/auth', auth);

// Error handler
app.onError((err, c) => {
  console.error('Server error:', err);
  return c.json(
    { error: 'Internal server error' },
    500
  );
});

// 404 handler
app.notFound((c) => {
  return c.json(
    { error: 'Not found' },
    404
  );
});

const port = process.env.PORT || 3000;
console.log(`Server running on http://localhost:${port}`);

export default {
  port,
  fetch: app.fetch
};
```
