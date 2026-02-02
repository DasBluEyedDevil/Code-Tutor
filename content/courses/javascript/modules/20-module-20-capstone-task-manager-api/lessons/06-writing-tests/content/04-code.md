---
type: "EXAMPLE"
title: "Integration Tests for API Endpoints"
---

Test API endpoints using app.request() to simulate HTTP calls:

```typescript
// tests/api.integration.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import app from '../src/index';

describe('Auth API Integration', () => {
  let authToken: string;

  describe('POST /api/auth/register', () => {
    it('should register a new user', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'test@example.com',
          password: 'Password123!',
          name: 'Test User'
        })
      }));

      expect(response.status).toBe(201);
      const data = await response.json();
      expect(data.success).toBe(true);
      expect(data.data.user.email).toBe('test@example.com');
      expect(data.data.token).toBeDefined();
    });

    it('should reject duplicate email', async () => {
      const email = 'duplicate@example.com';
      
      // First registration
      await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email,
          password: 'Password123!'
        })
      }));

      // Second registration with same email
      const response = await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email,
          password: 'DifferentPassword123!'
        })
      }));

      expect(response.status).toBe(409);
      const data = await response.json();
      expect(data.error.code).toBe('CONFLICT');
    });

    it('should validate email format', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'invalid-email',
          password: 'Password123!'
        })
      }));

      expect(response.status).toBe(400);
      const data = await response.json();
      expect(data.error.code).toBe('VALIDATION_ERROR');
    });
  });

  describe('POST /api/auth/login', () => {
    beforeEach(async () => {
      // Register a user first
      await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'login@example.com',
          password: 'Password123!'
        })
      }));
    });

    it('should login with correct credentials', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'login@example.com',
          password: 'Password123!'
        })
      }));

      expect(response.status).toBe(200);
      const data = await response.json();
      expect(data.success).toBe(true);
      expect(data.data.token).toBeDefined();
      authToken = data.data.token;
    });

    it('should reject wrong password', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'login@example.com',
          password: 'WrongPassword'
        })
      }));

      expect(response.status).toBe(401);
      const data = await response.json();
      expect(data.error.code).toBe('UNAUTHORIZED');
    });
  });
});
```
