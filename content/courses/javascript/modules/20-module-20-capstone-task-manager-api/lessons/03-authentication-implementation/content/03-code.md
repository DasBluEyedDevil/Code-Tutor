---
type: "CODE"
title: "JWT Utilities"
---

Create helper functions for JWT token generation and verification:

```typescript
// src/lib/jwt.ts
import { sign, verify } from 'hono/jwt';

const JWT_SECRET = process.env.JWT_SECRET || 'dev-secret-change-me';
const TOKEN_EXPIRY = '7d'; // 7 days

export interface JWTPayload {
  sub: string;      // User ID
  email: string;
  iat: number;      // Issued at
  exp: number;      // Expires at
}

export async function generateToken(user: { id: string; email: string }): Promise<string> {
  const now = Math.floor(Date.now() / 1000);
  const expiresIn = 7 * 24 * 60 * 60; // 7 days in seconds

  const payload: JWTPayload = {
    sub: user.id,
    email: user.email,
    iat: now,
    exp: now + expiresIn,
  };

  return sign(payload, JWT_SECRET);
}

export async function verifyToken(token: string): Promise<JWTPayload | null> {
  try {
    const payload = await verify(token, JWT_SECRET) as JWTPayload;
    return payload;
  } catch (error) {
    return null;
  }
}

export { JWT_SECRET };
```
