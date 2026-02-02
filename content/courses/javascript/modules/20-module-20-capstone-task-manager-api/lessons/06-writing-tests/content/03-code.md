---
type: "EXAMPLE"
title: "Unit Tests for Auth Utilities"
---

Create comprehensive unit tests for authentication functions:

```typescript
// tests/auth.unit.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import { hashPassword, verifyPassword, createToken, verifyToken } from '../src/lib/auth';

describe('Auth Utilities', () => {
  describe('Password Hashing', () => {
    it('should produce different hashes for the same password', async () => {
      const password = 'test123';
      const hash1 = await hashPassword(password);
      const hash2 = await hashPassword(password);
      
      // Same password should produce different hashes (due to salt)
      expect(hash1).not.toBe(hash2);
    });

    it('should handle long passwords', async () => {
      const longPassword = 'a'.repeat(1000);
      const hash = await hashPassword(longPassword);
      const isValid = await verifyPassword(longPassword, hash);
      
      expect(isValid).toBe(true);
    });

    it('should be case sensitive', async () => {
      const password = 'MyPassword';
      const hash = await hashPassword(password);
      
      expect(await verifyPassword('mypassword', hash)).toBe(false);
      expect(await verifyPassword('MYPASSWORD', hash)).toBe(false);
      expect(await verifyPassword('MyPassword', hash)).toBe(true);
    });
  });

  describe('JWT Tokens', () => {
    it('should include userId in token payload', () => {
      const userId = 'user-789';
      const token = createToken(userId);
      const payload = verifyToken(token);
      
      expect(payload?.userId).toBe(userId);
    });

    it('should include expiration time', () => {
      const token = createToken('user-123');
      const payload = verifyToken(token);
      
      expect(payload?.exp).toBeDefined();
      expect(typeof payload?.exp).toBe('number');
    });

    it('should reject expired tokens', () => {
      // This is harder to test without time manipulation
      // For now, just test that old timestamps are rejected
      const oldTimestamp = Math.floor(Date.now() / 1000) - 86400; // 1 day ago
      const expiredToken = 'header.payload.signature'; // Simplified
      
      // Would need a mock or test-specific token creation
      // This is where you might use jest.useFakeTimers() in Jest
    });

    it('should reject tampered tokens', () => {
      const token = createToken('user-123');
      const tamperedToken = token.replace(/.$/, 'X'); // Change last character
      const payload = verifyToken(tamperedToken);
      
      expect(payload).toBe(null);
    });
  });
});
```
