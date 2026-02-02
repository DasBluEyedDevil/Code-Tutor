---
type: "EXAMPLE"
title: "Updated Main App with Error Handling"
---

Update your main application file to use the error handler and consistent responses:

```typescript
// src/index.ts (updated)
import { Hono } from 'hono';
import { cors } from 'hono/cors';
import { logger } from 'hono/logger';
import { errorHandler } from './middleware/error-handler';
import { successResponse } from './lib/response';
import auth from './routes/auth';
import tasks from './routes/tasks';
import categories from './routes/categories';

const app = new Hono();

// Middleware (order matters!)
app.use('*', logger());
app.use('*', cors());
app.use('*', errorHandler); // Global error handler

// Health check
app.get('/', (c) => {
  return c.json(
    successResponse({
      message: 'Task Manager API',
      version: '1.0.0',
      endpoints: {
        auth: '/api/auth/*',
        tasks: '/api/tasks/*',
        categories: '/api/categories/*',
      },
    })
  );
});

// Routes
app.route('/api/auth', auth);
app.route('/api/tasks', tasks);
app.route('/api/categories', categories);

// 404 handler
app.notFound((c) => {
  return c.json(
    {
      success: false,
      error: {
        code: 'NOT_FOUND',
        message: 'Endpoint not found',
      },
    },
    404
  );
});

const port = process.env.PORT || 3000;
console.log(`Task Manager API running on http://localhost:${port}`);

export default {
  port,
  fetch: app.fetch,
};
```
