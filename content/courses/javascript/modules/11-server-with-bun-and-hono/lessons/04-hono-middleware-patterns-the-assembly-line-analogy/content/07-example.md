---
type: "EXAMPLE"
title: "JWT Authentication Middleware"
---

JSON Web Tokens (JWT) are the industry standard for stateless authentication in modern APIs. This middleware pattern verifies JWT tokens, extracts user information, and makes it available to your route handlers. The pattern includes token extraction, verification, and proper error handling for expired or invalid tokens.

```javascript
// Hono JWT Authentication Middleware (2025)
// Stateless authentication for APIs

import { Hono } from 'hono';
import { jwt } from 'hono/jwt';
import { sign, verify } from 'hono/jwt';

const app = new Hono();

// SECRET KEY (in production, use environment variable!)
const JWT_SECRET = 'your-super-secret-key-change-in-production';

// CUSTOM JWT MIDDLEWARE (manual implementation for learning)
const jwtAuth = async (c, next) => {
  // 1. Extract token from Authorization header
  const authHeader = c.req.header('Authorization');
  
  if (!authHeader) {
    return c.json({ 
      error: 'Unauthorized',
      message: 'No authorization header provided'
    }, 401);
  }
  
  // 2. Check Bearer format
  if (!authHeader.startsWith('Bearer ')) {
    return c.json({
      error: 'Unauthorized', 
      message: 'Invalid authorization format. Use: Bearer <token>'
    }, 401);
  }
  
  const token = authHeader.replace('Bearer ', '');
  
  try {
    // 3. Verify and decode token
    const payload = await verify(token, JWT_SECRET);
    
    // 4. Check expiration
    if (payload.exp && payload.exp < Date.now() / 1000) {
      return c.json({
        error: 'Unauthorized',
        message: 'Token has expired'
      }, 401);
    }
    
    // 5. Store user in context for route handlers
    c.set('jwtPayload', payload);
    c.set('user', {
      id: payload.sub,
      email: payload.email,
      role: payload.role
    });
    
    await next();
    
  } catch (error) {
    return c.json({
      error: 'Unauthorized',
      message: 'Invalid token'
    }, 401);
  }
};

// ROLE-BASED ACCESS CONTROL MIDDLEWARE
const requireRole = (...allowedRoles) => {
  return async (c, next) => {
    const user = c.get('user');
    
    if (!user) {
      return c.json({ error: 'Unauthorized' }, 401);
    }
    
    if (!allowedRoles.includes(user.role)) {
      return c.json({
        error: 'Forbidden',
        message: `Required role: ${allowedRoles.join(' or ')}`
      }, 403);
    }
    
    await next();
  };
};

// PUBLIC ROUTES (no auth required)

app.get('/', (c) => c.text('Welcome to the API!'));

app.get('/api/health', (c) => c.json({ status: 'ok' }));

// LOGIN ROUTE - Generate JWT
app.post('/api/login', async (c) => {
  const body = await c.req.json();
  const { email, password } = body;
  
  // Simulate user lookup (in real app, check database)
  const users = {
    'admin@example.com': { id: 1, password: 'admin123', role: 'admin' },
    'user@example.com': { id: 2, password: 'user123', role: 'user' }
  };
  
  const user = users[email];
  
  if (!user || user.password !== password) {
    return c.json({ error: 'Invalid credentials' }, 401);
  }
  
  // Create JWT token
  const payload = {
    sub: user.id,           // Subject (user ID)
    email: email,
    role: user.role,
    iat: Math.floor(Date.now() / 1000),  // Issued at
    exp: Math.floor(Date.now() / 1000) + (60 * 60 * 24)  // Expires in 24h
  };
  
  const token = await sign(payload, JWT_SECRET);
  
  return c.json({
    message: 'Login successful',
    token: token,
    expiresIn: '24h'
  });
});

// PROTECTED ROUTES (require JWT)

// Apply JWT middleware to all /api/protected routes
app.use('/api/protected/*', jwtAuth);

app.get('/api/protected/profile', (c) => {
  const user = c.get('user');
  return c.json({
    message: 'Protected profile data',
    user: user
  });
});

app.get('/api/protected/dashboard', (c) => {
  const user = c.get('user');
  return c.json({
    message: `Welcome back, ${user.email}!`,
    userId: user.id,
    role: user.role
  });
});

// ADMIN ONLY ROUTES (require JWT + admin role)

app.use('/api/admin/*', jwtAuth);
app.use('/api/admin/*', requireRole('admin'));

app.get('/api/admin/users', (c) => {
  return c.json({
    message: 'Admin area - all users',
    users: [
      { id: 1, email: 'admin@example.com', role: 'admin' },
      { id: 2, email: 'user@example.com', role: 'user' }
    ]
  });
});

app.delete('/api/admin/users/:id', (c) => {
  const id = c.req.param('id');
  const admin = c.get('user');
  
  return c.json({
    message: `User ${id} deleted by admin ${admin.email}`
  });
});

// USING HONO'S BUILT-IN JWT MIDDLEWARE
// Simpler alternative to custom implementation

// import { jwt } from 'hono/jwt';
// app.use('/api/v2/*', jwt({ secret: JWT_SECRET, alg: 'HS256' }));
// Then access payload via c.get('jwtPayload')
// Note: The alg option is required since Hono 4.11.0

export default app;

// USAGE EXAMPLE:
// 1. Login: POST /api/login { email, password } -> get token
// 2. Access protected: GET /api/protected/profile
//    Header: Authorization: Bearer <token>
// 3. Admin access: GET /api/admin/users (requires admin role)
```
