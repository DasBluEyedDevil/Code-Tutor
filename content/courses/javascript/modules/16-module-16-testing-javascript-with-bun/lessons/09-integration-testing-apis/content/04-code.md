---
type: "EXAMPLE"
title: "Testing Authentication Flows"
---

Authentication tests verify login, token handling, and protected route access. You need to test the full flow: login to get a token, use the token to access protected routes, and verify unauthorized access is blocked.

```javascript
import { describe, it, expect, beforeEach } from 'bun:test';
import { Hono } from 'hono';
import { sign, verify } from 'hono/jwt';

const JWT_SECRET = 'test-secret-key';

// Create app with authentication
function createAuthApp() {
  const app = new Hono();
  
  // Mock user database
  const users = new Map([
    ['alice@test.com', { id: 1, email: 'alice@test.com', password: 'password123', role: 'admin' }],
    ['bob@test.com', { id: 2, email: 'bob@test.com', password: 'secret456', role: 'user' }]
  ]);

  // Login endpoint
  app.post('/api/auth/login', async (c) => {
    const { email, password } = await c.req.json();
    const user = users.get(email);
    
    if (!user || user.password !== password) {
      return c.json({ error: 'Invalid credentials' }, 401);
    }
    
    const token = await sign(
      { userId: user.id, email: user.email, role: user.role },
      JWT_SECRET
    );
    
    return c.json({ token, user: { id: user.id, email: user.email, role: user.role } });
  });

  // Auth middleware
  const authMiddleware = async (c, next) => {
    const authHeader = c.req.header('Authorization');
    if (!authHeader?.startsWith('Bearer ')) {
      return c.json({ error: 'No token provided' }, 401);
    }
    
    const token = authHeader.slice(7);
    try {
      const payload = await verify(token, JWT_SECRET);
      c.set('user', payload);
      await next();
    } catch {
      return c.json({ error: 'Invalid token' }, 401);
    }
  };

  // Admin-only middleware
  const adminOnly = async (c, next) => {
    const user = c.get('user');
    if (user.role !== 'admin') {
      return c.json({ error: 'Admin access required' }, 403);
    }
    await next();
  };

  // Public endpoint
  app.get('/api/public', (c) => c.json({ message: 'Public data' }));

  // Protected endpoint
  app.get('/api/profile', authMiddleware, (c) => {
    const user = c.get('user');
    return c.json({ userId: user.userId, email: user.email });
  });

  // Admin-only endpoint
  app.get('/api/admin/users', authMiddleware, adminOnly, (c) => {
    return c.json(Array.from(users.values()).map(u => ({
      id: u.id, email: u.email, role: u.role
    })));
  });

  // Logout (client-side token deletion, but we can test the endpoint)
  app.post('/api/auth/logout', authMiddleware, (c) => {
    return c.json({ message: 'Logged out successfully' });
  });

  return app;
}

// Helper to get auth token
async function loginAs(app, email, password) {
  const res = await app.request('/api/auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
  });
  const { token } = await res.json();
  return token;
}

describe('Authentication API', () => {
  let app;

  beforeEach(() => {
    app = createAuthApp();
  });

  describe('POST /api/auth/login', () => {
    it('returns token for valid credentials', async () => {
      const res = await app.request('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: 'alice@test.com', password: 'password123' })
      });
      
      expect(res.status).toBe(200);
      const data = await res.json();
      expect(data.token).toBeDefined();
      expect(data.user.email).toBe('alice@test.com');
      expect(data.user.role).toBe('admin');
    });

    it('rejects invalid email', async () => {
      const res = await app.request('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: 'wrong@test.com', password: 'password123' })
      });
      
      expect(res.status).toBe(401);
      const error = await res.json();
      expect(error.error).toBe('Invalid credentials');
    });

    it('rejects invalid password', async () => {
      const res = await app.request('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: 'alice@test.com', password: 'wrongpassword' })
      });
      
      expect(res.status).toBe(401);
    });
  });

  describe('Protected Routes', () => {
    it('allows access with valid token', async () => {
      const token = await loginAs(app, 'alice@test.com', 'password123');
      
      const res = await app.request('/api/profile', {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      
      expect(res.status).toBe(200);
      const profile = await res.json();
      expect(profile.email).toBe('alice@test.com');
    });

    it('rejects request without token', async () => {
      const res = await app.request('/api/profile');
      
      expect(res.status).toBe(401);
      const error = await res.json();
      expect(error.error).toBe('No token provided');
    });

    it('rejects invalid token', async () => {
      const res = await app.request('/api/profile', {
        headers: { 'Authorization': 'Bearer invalid-token-here' }
      });
      
      expect(res.status).toBe(401);
      const error = await res.json();
      expect(error.error).toBe('Invalid token');
    });

    it('rejects malformed authorization header', async () => {
      const res = await app.request('/api/profile', {
        headers: { 'Authorization': 'NotBearer token' }
      });
      
      expect(res.status).toBe(401);
    });
  });

  describe('Authorization (Roles)', () => {
    it('allows admin to access admin routes', async () => {
      const token = await loginAs(app, 'alice@test.com', 'password123');
      
      const res = await app.request('/api/admin/users', {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      
      expect(res.status).toBe(200);
      const users = await res.json();
      expect(users).toHaveLength(2);
    });

    it('denies non-admin from admin routes', async () => {
      const token = await loginAs(app, 'bob@test.com', 'secret456');
      
      const res = await app.request('/api/admin/users', {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      
      expect(res.status).toBe(403);
      const error = await res.json();
      expect(error.error).toBe('Admin access required');
    });
  });

  describe('Logout', () => {
    it('successfully logs out authenticated user', async () => {
      const token = await loginAs(app, 'alice@test.com', 'password123');
      
      const res = await app.request('/api/auth/logout', {
        method: 'POST',
        headers: { 'Authorization': `Bearer ${token}` }
      });
      
      expect(res.status).toBe(200);
      const data = await res.json();
      expect(data.message).toBe('Logged out successfully');
    });
  });
});
```
