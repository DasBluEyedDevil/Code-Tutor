---
type: "CODE"
title: "Testing HTTP Endpoints"
---

Integration tests make real HTTP requests to your API and verify the complete request/response cycle. With Hono's built-in test client, you can test endpoints without starting a server.

```javascript
import { describe, it, expect, beforeEach } from 'bun:test';
import { Hono } from 'hono';

// Create a testable app factory
function createApp() {
  const app = new Hono();
  const users = new Map();
  let nextId = 1;

  // GET all users
  app.get('/api/users', (c) => {
    return c.json(Array.from(users.values()));
  });

  // GET single user
  app.get('/api/users/:id', (c) => {
    const id = parseInt(c.req.param('id'));
    const user = users.get(id);
    if (!user) {
      return c.json({ error: 'User not found' }, 404);
    }
    return c.json(user);
  });

  // POST create user
  app.post('/api/users', async (c) => {
    const body = await c.req.json();
    if (!body.name || !body.email) {
      return c.json({ error: 'Name and email required' }, 400);
    }
    const user = { id: nextId++, name: body.name, email: body.email };
    users.set(user.id, user);
    return c.json(user, 201);
  });

  // PUT update user
  app.put('/api/users/:id', async (c) => {
    const id = parseInt(c.req.param('id'));
    const user = users.get(id);
    if (!user) {
      return c.json({ error: 'User not found' }, 404);
    }
    const body = await c.req.json();
    const updated = { ...user, ...body };
    users.set(id, updated);
    return c.json(updated);
  });

  // DELETE user
  app.delete('/api/users/:id', (c) => {
    const id = parseInt(c.req.param('id'));
    if (!users.has(id)) {
      return c.json({ error: 'User not found' }, 404);
    }
    users.delete(id);
    return c.json({ deleted: true });
  });

  return app;
}

describe('Users API', () => {
  let app;

  beforeEach(() => {
    app = createApp();  // Fresh app with empty state
  });

  describe('GET /api/users', () => {
    it('returns empty array when no users exist', async () => {
      const res = await app.request('/api/users');
      
      expect(res.status).toBe(200);
      expect(await res.json()).toEqual([]);
    });

    it('returns all users', async () => {
      // Create two users first
      await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Alice', email: 'alice@test.com' })
      });
      await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Bob', email: 'bob@test.com' })
      });

      const res = await app.request('/api/users');
      const users = await res.json();
      
      expect(res.status).toBe(200);
      expect(users).toHaveLength(2);
      expect(users[0].name).toBe('Alice');
      expect(users[1].name).toBe('Bob');
    });
  });

  describe('POST /api/users', () => {
    it('creates user with valid data', async () => {
      const res = await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Alice', email: 'alice@test.com' })
      });
      
      expect(res.status).toBe(201);
      const user = await res.json();
      expect(user.id).toBe(1);
      expect(user.name).toBe('Alice');
      expect(user.email).toBe('alice@test.com');
    });
  });

  describe('PUT /api/users/:id', () => {
    it('updates existing user', async () => {
      // Create user
      const createRes = await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Alice', email: 'alice@test.com' })
      });
      const { id } = await createRes.json();

      // Update user
      const updateRes = await app.request(`/api/users/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Alice Updated' })
      });
      
      expect(updateRes.status).toBe(200);
      const updated = await updateRes.json();
      expect(updated.name).toBe('Alice Updated');
      expect(updated.email).toBe('alice@test.com');  // Unchanged
    });
  });

  describe('DELETE /api/users/:id', () => {
    it('deletes existing user', async () => {
      // Create user
      const createRes = await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Alice', email: 'alice@test.com' })
      });
      const { id } = await createRes.json();

      // Delete user
      const deleteRes = await app.request(`/api/users/${id}`, {
        method: 'DELETE'
      });
      expect(deleteRes.status).toBe(200);

      // Verify deleted
      const getRes = await app.request(`/api/users/${id}`);
      expect(getRes.status).toBe(404);
    });
  });
});
```
