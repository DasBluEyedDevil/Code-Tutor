---
type: "CODE"
title: "Create the Entry Point"
---

Set up the main application file with Hono:

```typescript
// src/index.ts
import { Hono } from 'hono';
import { cors } from 'hono/cors';
import { logger } from 'hono/logger';

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
    endpoints: {
      auth: '/api/auth/*',
      tasks: '/api/tasks/*',
      categories: '/api/categories/*'
    }
  });
});

// Routes will be added here in later lessons
// app.route('/api/auth', authRoutes);
// app.route('/api/tasks', taskRoutes);
// app.route('/api/categories', categoryRoutes);

const port = process.env.PORT || 3000;
console.log(`Server running on http://localhost:${port}`);

export default {
  port,
  fetch: app.fetch
};
```
