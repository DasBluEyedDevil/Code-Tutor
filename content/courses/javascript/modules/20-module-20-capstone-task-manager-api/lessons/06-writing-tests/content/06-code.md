---
type: "EXAMPLE"
title: "Testing Auth Flows - Register, Login, Protected Routes"
---

Test complete authentication flows end-to-end:

```typescript
// tests/auth-flow.integration.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import { setupTestDatabase, cleanupDatabase } from './setup';
import app from '../src/index';
import { PrismaClient } from '@prisma/client';

let prisma: PrismaClient;
let validToken: string;

describe('Authentication Flow', () => {
  beforeEach(async () => {
    prisma = await setupTestDatabase();
  });

  describe('Complete Auth Flow', () => {
    it('should register, login, and access protected route', async () => {
      // Step 1: Register
      const registerResponse = await app.request(new Request('http://localhost/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'flow@example.com',
          password: 'Secure123!',
          name: 'Flow User'
        })
      }));

      expect(registerResponse.status).toBe(201);
      const registerData = await registerResponse.json();
      expect(registerData.data.token).toBeDefined();

      // Step 2: Login
      const loginResponse = await app.request(new Request('http://localhost/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'flow@example.com',
          password: 'Secure123!'
        })
      }));

      expect(loginResponse.status).toBe(200);
      const loginData = await loginResponse.json();
      const token = loginData.data.token;

      // Step 3: Access protected route
      const meResponse = await app.request(new Request('http://localhost/api/auth/me', {
        method: 'GET',
        headers: { 'Authorization': `Bearer ${token}` }
      }));

      expect(meResponse.status).toBe(200);
      const meData = await meResponse.json();
      expect(meData.data.email).toBe('flow@example.com');
    });

    it('should reject access to protected route without token', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/me', {
        method: 'GET'
      }));

      expect(response.status).toBe(401);
      const data = await response.json();
      expect(data.error.code).toBe('UNAUTHORIZED');
    });

    it('should reject access with invalid token', async () => {
      const response = await app.request(new Request('http://localhost/api/auth/me', {
        method: 'GET',
        headers: { 'Authorization': 'Bearer invalid.token.here' }
      }));

      expect(response.status).toBe(401);
      const data = await response.json();
      expect(data.error.code).toBe('UNAUTHORIZED');
    });
  });
});
```
