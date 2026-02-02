---
type: "EXAMPLE"
title: "Auth Middleware"
---

Create middleware that protects routes and provides user context:

```typescript
// src/middleware/auth.ts
import { Context, Next } from 'hono';
import { verifyToken, JWTPayload } from '../lib/jwt';
import { prisma } from '../lib/db';

// Extend Hono's context to include user
export interface AuthContext {
  userId: string;
  email: string;
}

export async function authMiddleware(c: Context, next: Next) {
  const authHeader = c.req.header('Authorization');

  if (!authHeader || !authHeader.startsWith('Bearer ')) {
    return c.json(
      { error: 'Missing or invalid Authorization header' },
      401
    );
  }

  const token = authHeader.slice(7); // Remove 'Bearer ' prefix

  const payload = await verifyToken(token);
  if (!payload) {
    return c.json(
      { error: 'Invalid or expired token' },
      401
    );
  }

  // Verify user still exists in database
  const user = await prisma.user.findUnique({
    where: { id: payload.sub },
    select: { id: true, email: true },
  });

  if (!user) {
    return c.json(
      { error: 'User not found' },
      401
    );
  }

  // Attach user to context for use in route handlers
  c.set('userId', user.id);
  c.set('email', user.email);

  await next();
}
```
