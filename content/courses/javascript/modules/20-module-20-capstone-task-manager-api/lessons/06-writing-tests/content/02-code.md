---
type: "CODE"
title: "Bun Test Setup - describe, it, expect patterns"
---

Bun comes with a built-in test runner. Create your first test file:

```typescript
// tests/auth.utils.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import * as authUtils from '../src/lib/auth';

describe('Password Hashing', () => {
  it('should hash a password', async () => {
    const password = 'myPassword123';
    const hash = await authUtils.hashPassword(password);
    
    // Hash should not be the same as original password
    expect(hash).not.toBe(password);
    
    // Hash should be a string
    expect(typeof hash).toBe('string');
  });

  it('should verify a correct password', async () => {
    const password = 'myPassword123';
    const hash = await authUtils.hashPassword(password);
    const isValid = await authUtils.verifyPassword(password, hash);
    
    expect(isValid).toBe(true);
  });

  it('should reject an incorrect password', async () => {
    const password = 'myPassword123';
    const hash = await authUtils.hashPassword(password);
    const isValid = await authUtils.verifyPassword('wrongPassword', hash);
    
    expect(isValid).toBe(false);
  });
});

describe('JWT Token Generation', () => {
  it('should create a valid JWT token', () => {
    const userId = 'user-123';
    const token = authUtils.createToken(userId);
    
    expect(typeof token).toBe('string');
    expect(token.split('.').length).toBe(3); // JWT format: header.payload.signature
  });

  it('should decode a valid token', () => {
    const userId = 'user-456';
    const token = authUtils.createToken(userId);
    const decoded = authUtils.verifyToken(token);
    
    expect(decoded).not.toBe(null);
    expect(decoded?.userId).toBe(userId);
  });

  it('should reject an invalid token', () => {
    const invalidToken = 'not.a.valid.token';
    const decoded = authUtils.verifyToken(invalidToken);
    
    expect(decoded).toBe(null);
  });
});
```
