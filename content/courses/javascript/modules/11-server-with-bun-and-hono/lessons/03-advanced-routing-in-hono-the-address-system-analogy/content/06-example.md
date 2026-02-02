---
type: "EXAMPLE"
title: "Route Groups and Prefixes"
---

Hono provides a powerful way to organize routes using route groups and prefixes. Instead of repeating the same path prefix for every route, you can group related routes together using app.route(). This is essential for building modular, maintainable APIs where different features are organized into separate route files or modules.

```javascript
// Hono Route Groups and Prefixes (2025)
// Organize routes into modular, reusable groups

import { Hono } from 'hono';

// MAIN APPLICATION
const app = new Hono();

// CREATE SEPARATE ROUTE GROUPS
// Each group is its own mini Hono app!

// Users API group
const usersApi = new Hono();

usersApi.get('/', (c) => {
  // This handles GET /api/users
  return c.json({ users: ['Alice', 'Bob', 'Charlie'] });
});

usersApi.get('/:id', (c) => {
  // This handles GET /api/users/:id
  const id = c.req.param('id');
  return c.json({ id, name: 'Alice', email: 'alice@example.com' });
});

usersApi.post('/', async (c) => {
  // This handles POST /api/users
  const body = await c.req.json();
  return c.json({ message: 'User created', user: body }, 201);
});

// Products API group
const productsApi = new Hono();

productsApi.get('/', (c) => {
  // This handles GET /api/products
  return c.json({ products: ['Laptop', 'Phone', 'Tablet'] });
});

productsApi.get('/:id', (c) => {
  // This handles GET /api/products/:id
  const id = c.req.param('id');
  return c.json({ id, name: 'Laptop', price: 999 });
});

// Admin routes group (can have its own middleware!)
const adminApi = new Hono();

// Admin-specific middleware
adminApi.use('*', async (c, next) => {
  const isAdmin = c.req.header('X-Admin-Key') === 'secret';
  if (!isAdmin) {
    return c.json({ error: 'Admin access required' }, 403);
  }
  await next();
});

adminApi.get('/stats', (c) => {
  return c.json({ totalUsers: 1000, totalOrders: 5000 });
});

adminApi.delete('/users/:id', (c) => {
  const id = c.req.param('id');
  return c.json({ message: `User ${id} deleted by admin` });
});

// MOUNT ROUTE GROUPS WITH PREFIXES
app.route('/api/users', usersApi);     // All usersApi routes prefixed with /api/users
app.route('/api/products', productsApi); // All productsApi routes prefixed with /api/products
app.route('/admin', adminApi);           // All adminApi routes prefixed with /admin

// Root routes (not grouped)
app.get('/', (c) => c.text('Welcome to the API!'));
app.get('/health', (c) => c.json({ status: 'ok' }));

// RESULT:
// GET /                    -> 'Welcome to the API!'
// GET /health              -> { status: 'ok' }
// GET /api/users           -> { users: [...] }
// GET /api/users/42        -> { id: '42', name: 'Alice', ... }
// POST /api/users          -> { message: 'User created', ... }
// GET /api/products        -> { products: [...] }
// GET /api/products/1      -> { id: '1', name: 'Laptop', ... }
// GET /admin/stats         -> { totalUsers: 1000, ... } (requires X-Admin-Key)
// DELETE /admin/users/5    -> { message: 'User 5 deleted...' } (requires X-Admin-Key)

export default app;

// In a real project, you'd organize like this:
// src/
//   index.ts        <- main app, mounts route groups
//   routes/
//     users.ts      <- export usersApi
//     products.ts   <- export productsApi
//     admin.ts      <- export adminApi
```
