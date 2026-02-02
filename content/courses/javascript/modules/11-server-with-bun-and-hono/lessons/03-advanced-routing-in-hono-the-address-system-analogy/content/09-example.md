---
type: "EXAMPLE"
title: "Multiple Handlers Per Route"
---

Hono allows you to chain multiple handler functions for a single route. Each handler runs in sequence and can perform validation, authentication, logging, or data transformation before passing control to the next handler using await next(). This pattern is perfect for separating concerns like validation middleware from business logic.

```javascript
// Hono Multiple Handlers Per Route (2025)
// Chain handlers for validation, auth, and processing

import { Hono } from 'hono';

const app = new Hono();

// INLINE MIDDLEWARE PATTERN
// Multiple handlers passed directly to the route

// Handler 1: Validate request body
const validateUser = async (c, next) => {
  if (c.req.method === 'POST' || c.req.method === 'PUT') {
    const body = await c.req.json();
    
    if (!body.name || body.name.length < 2) {
      return c.json({ error: 'Name must be at least 2 characters' }, 400);
    }
    if (!body.email || !body.email.includes('@')) {
      return c.json({ error: 'Valid email is required' }, 400);
    }
    
    // Store validated body for next handler
    c.set('validatedBody', body);
  }
  await next();
};

// Handler 2: Check authentication
const requireAuth = async (c, next) => {
  const token = c.req.header('Authorization');
  
  if (!token || !token.startsWith('Bearer ')) {
    return c.json({ error: 'Authentication required' }, 401);
  }
  
  // Simulate token validation
  const tokenValue = token.replace('Bearer ', '');
  if (tokenValue !== 'valid-token') {
    return c.json({ error: 'Invalid token' }, 401);
  }
  
  // Store user info for next handler
  c.set('user', { id: 1, name: 'Alice', role: 'admin' });
  await next();
};

// Handler 3: Check admin role
const requireAdmin = async (c, next) => {
  const user = c.get('user');
  
  if (!user || user.role !== 'admin') {
    return c.json({ error: 'Admin access required' }, 403);
  }
  
  await next();
};

// Handler 4: Log the request
const logRequest = async (c, next) => {
  const start = Date.now();
  console.log(`[${c.req.method}] ${c.req.path} - Started`);
  
  await next();
  
  const duration = Date.now() - start;
  console.log(`[${c.req.method}] ${c.req.path} - Completed in ${duration}ms`);
};

// USING MULTIPLE HANDLERS

// Public route - just logging
app.get('/public', 
  logRequest,
  (c) => c.json({ message: 'Public data' })
);

// Protected route - auth required
app.get('/profile',
  logRequest,
  requireAuth,
  (c) => {
    const user = c.get('user');
    return c.json({ profile: user });
  }
);

// Admin route - multiple checks
app.get('/admin/users',
  logRequest,
  requireAuth,
  requireAdmin,
  (c) => {
    return c.json({ 
      users: [
        { id: 1, name: 'Alice' },
        { id: 2, name: 'Bob' }
      ]
    });
  }
);

// Create user - validation + auth
app.post('/api/users',
  logRequest,
  requireAuth,
  validateUser,
  (c) => {
    const body = c.get('validatedBody');
    const user = c.get('user');
    
    return c.json({
      message: 'User created',
      createdBy: user.name,
      newUser: {
        id: crypto.randomUUID(),
        ...body
      }
    }, 201);
  }
);

// Delete user - full protection
app.delete('/api/users/:id',
  logRequest,
  requireAuth,
  requireAdmin,
  (c) => {
    const id = c.req.param('id');
    const admin = c.get('user');
    
    return c.json({
      message: `User ${id} deleted`,
      deletedBy: admin.name
    });
  }
);

// FACTORY PATTERN for reusable handler chains
const protectedRoute = (...handlers) => [
  logRequest,
  requireAuth,
  ...handlers
];

const adminRoute = (...handlers) => [
  logRequest,
  requireAuth,
  requireAdmin,
  ...handlers
];

// Usage with factory
app.get('/api/orders', ...protectedRoute(
  (c) => c.json({ orders: [] })
));

app.delete('/api/orders/:id', ...adminRoute(
  (c) => c.json({ deleted: c.req.param('id') })
));

export default app;

// EXECUTION ORDER:
// Request -> logRequest -> requireAuth -> requireAdmin -> handler -> Response
//                |              |              |             |
//                v              v              v             v
//          (can stop)     (can stop)     (can stop)    (final response)
```
