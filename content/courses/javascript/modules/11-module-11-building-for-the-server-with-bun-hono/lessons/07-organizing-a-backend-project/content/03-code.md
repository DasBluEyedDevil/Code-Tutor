---
type: "CODE"
title: "Separating Routes from Handlers"
---

The route layer should be thin and declarative. It defines what URLs your API responds to and which handlers process those requests. The route file is like a table of contents for your API - you can scan it quickly to see all available endpoints. By keeping routes separate from handlers, you gain the ability to test handlers in isolation, reuse handlers across multiple routes, and understand your API structure at a glance. The handler files contain the actual logic for processing requests, but even handlers should remain focused on HTTP concerns and delegate business logic to services.

```typescript
// src/routes/users.routes.ts
// Routes are THIN - they just connect URLs to handlers

import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { usersHandler } from '../handlers/users.handler';
import { createUserSchema, updateUserSchema, userIdSchema } from '../types/user.types';
import { authMiddleware } from '../middleware/auth';

const usersRouter = new Hono();

// List all users - GET /users
usersRouter.get('/', usersHandler.list);

// Get single user - GET /users/:id
usersRouter.get(
  '/:id',
  zValidator('param', userIdSchema),
  usersHandler.getById
);

// Create user - POST /users
usersRouter.post(
  '/',
  zValidator('json', createUserSchema),
  usersHandler.create
);

// Update user - PUT /users/:id (requires auth)
usersRouter.put(
  '/:id',
  authMiddleware,
  zValidator('param', userIdSchema),
  zValidator('json', updateUserSchema),
  usersHandler.update
);

// Delete user - DELETE /users/:id (requires auth)
usersRouter.delete(
  '/:id',
  authMiddleware,
  zValidator('param', userIdSchema),
  usersHandler.delete
);

export { usersRouter };

// ============================================================
// src/handlers/users.handler.ts
// Handlers process requests and call services
// ============================================================

import { Context } from 'hono';
import { UserService } from '../services/user.service';
import { CreateUserInput, UpdateUserInput } from '../types/user.types';

const userService = new UserService();

export const usersHandler = {
  // GET /users - List all users
  async list(c: Context) {
    const page = Number(c.req.query('page')) || 1;
    const limit = Number(c.req.query('limit')) || 20;
    
    const result = await userService.listUsers(page, limit);
    
    return c.json({
      success: true,
      data: result.users,
      pagination: {
        page,
        limit,
        total: result.total,
        totalPages: Math.ceil(result.total / limit)
      }
    });
  },

  // GET /users/:id - Get single user
  async getById(c: Context) {
    const { id } = c.req.valid('param');
    
    const user = await userService.getUserById(id);
    
    if (!user) {
      return c.json(
        { success: false, error: 'User not found' },
        404
      );
    }
    
    return c.json({ success: true, data: user });
  },

  // POST /users - Create new user
  async create(c: Context) {
    const input: CreateUserInput = c.req.valid('json');
    
    // Check if email already exists (business rule)
    const existing = await userService.getUserByEmail(input.email);
    if (existing) {
      return c.json(
        { success: false, error: 'Email already registered' },
        409 // Conflict
      );
    }
    
    const user = await userService.createUser(input);
    
    return c.json(
      { success: true, data: user, message: 'User created successfully' },
      201
    );
  },

  // PUT /users/:id - Update user
  async update(c: Context) {
    const { id } = c.req.valid('param');
    const input: UpdateUserInput = c.req.valid('json');
    
    // Check ownership or admin status
    const currentUser = c.get('user');
    if (currentUser.id !== id && !currentUser.isAdmin) {
      return c.json(
        { success: false, error: 'Not authorized to update this user' },
        403
      );
    }
    
    const user = await userService.updateUser(id, input);
    
    if (!user) {
      return c.json(
        { success: false, error: 'User not found' },
        404
      );
    }
    
    return c.json({ success: true, data: user });
  },

  // DELETE /users/:id - Delete user
  async delete(c: Context) {
    const { id } = c.req.valid('param');
    const currentUser = c.get('user');
    
    // Only admins can delete users
    if (!currentUser.isAdmin) {
      return c.json(
        { success: false, error: 'Admin access required' },
        403
      );
    }
    
    const deleted = await userService.deleteUser(id);
    
    if (!deleted) {
      return c.json(
        { success: false, error: 'User not found' },
        404
      );
    }
    
    return c.json(
      { success: true, message: 'User deleted successfully' },
      200
    );
  }
};

// ============================================================
// src/routes/index.ts
// Main router that combines all route modules
// ============================================================

import { Hono } from 'hono';
import { usersRouter } from './users.routes';
import { postsRouter } from './posts.routes';
import { authRouter } from './auth.routes';

const apiRouter = new Hono();

// Mount all route modules under /api
apiRouter.route('/users', usersRouter);
apiRouter.route('/posts', postsRouter);
apiRouter.route('/auth', authRouter);

// Health check endpoint
apiRouter.get('/health', (c) => c.json({ status: 'ok', timestamp: Date.now() }));

export { apiRouter };

// ============================================================
// src/index.ts
// Application entry point
// ============================================================

import { Hono } from 'hono';
import { cors } from 'hono/cors';
import { logger } from 'hono/logger';
import { apiRouter } from './routes';
import { errorHandler } from './middleware/errorHandler';
import { config } from './config';

const app = new Hono();

// Global middleware
app.use('*', logger());
app.use('*', cors(config.cors));

// Mount API routes
app.route('/api', apiRouter);

// Global error handler
app.onError(errorHandler);

// 404 handler
app.notFound((c) => c.json({ error: 'Not Found' }, 404));

export default {
  port: config.port,
  fetch: app.fetch
};
```
