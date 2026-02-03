---
type: "EXAMPLE"
title: "Auth Routes"
---

Implement registration and login endpoints:

```typescript
// src/routes/auth.ts
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { prisma } from '../lib/db';
import { generateToken } from '../lib/jwt';
import { registerSchema, loginSchema } from '../schemas/auth';
import { authMiddleware } from '../middleware/auth';

const auth = new Hono();

// POST /api/auth/register
auth.post(
  '/register',
  zValidator('json', registerSchema),
  async (c) => {
    const { email, password, name } = c.req.valid('json');

    // Check if user exists
    const existing = await prisma.user.findUnique({
      where: { email },
    });

    if (existing) {
      return c.json({ error: 'Email already registered' }, 409);
    }

    // Hash password
    const passwordHash = await Bun.password.hash(password, {
      algorithm: 'argon2id',
    });

    // Create user
    const user = await prisma.user.create({
      data: {
        email,
        passwordHash,
        name,
      },
      select: {
        id: true,
        email: true,
        name: true,
        createdAt: true,
      },
    });

    // Generate token
    const token = await generateToken(user);

    return c.json({
      message: 'Registration successful',
      user,
      token,
    }, 201);
  }
);

// POST /api/auth/login
auth.post(
  '/login',
  zValidator('json', loginSchema),
  async (c) => {
    const { email, password } = c.req.valid('json');

    // Find user
    const user = await prisma.user.findUnique({
      where: { email },
    });

    if (!user) {
      // Use same message for both cases (security)
      return c.json({ error: 'Invalid email or password' }, 401);
    }

    // Verify password
    const validPassword = await Bun.password.verify(
      password,
      user.passwordHash
    );

    if (!validPassword) {
      return c.json({ error: 'Invalid email or password' }, 401);
    }

    // Generate token
    const token = await generateToken(user);

    return c.json({
      message: 'Login successful',
      user: {
        id: user.id,
        email: user.email,
        name: user.name,
      },
      token,
    });
  }
);

// GET /api/auth/me (protected)
auth.get('/me', authMiddleware, async (c) => {
  const userId = c.get('userId');

  const user = await prisma.user.findUnique({
    where: { id: userId },
    select: {
      id: true,
      email: true,
      name: true,
      createdAt: true,
      _count: {
        select: {
          tasks: true,
          categories: true,
        },
      },
    },
  });

  return c.json({ user });
});

export default auth;
```
