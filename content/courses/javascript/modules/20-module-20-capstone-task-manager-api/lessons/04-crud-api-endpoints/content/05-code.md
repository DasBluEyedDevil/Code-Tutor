---
type: "CODE"
title: "Category Routes"
---

Implement complete CRUD for categories:

```typescript
// src/routes/categories.ts
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { z } from 'zod';
import { prisma } from '../lib/db';
import { authMiddleware } from '../middleware/auth';

const categories = new Hono();

categories.use('*', authMiddleware);

const categorySchema = z.object({
  name: z.string().min(1).max(50).trim(),
  color: z
    .string()
    .regex(/^#[0-9A-Fa-f]{6}$/, 'Invalid hex color')
    .default('#3B82F6'),
});

// GET /api/categories
categories.get('/', async (c) => {
  const userId = c.get('userId');

  const items = await prisma.category.findMany({
    where: { userId },
    include: {
      _count: {
        select: { tasks: true },
      },
    },
    orderBy: { name: 'asc' },
  });

  return c.json({ categories: items });
});

// POST /api/categories
categories.post(
  '/',
  zValidator('json', categorySchema),
  async (c) => {
    const userId = c.get('userId');
    const data = c.req.valid('json');

    // Check for duplicate name
    const existing = await prisma.category.findFirst({
      where: { userId, name: data.name },
    });

    if (existing) {
      return c.json({ error: 'Category with this name already exists' }, 409);
    }

    const category = await prisma.category.create({
      data: { ...data, userId },
    });

    return c.json({ category }, 201);
  }
);

// PUT /api/categories/:id
categories.put(
  '/:id',
  zValidator('json', categorySchema.partial()),
  async (c) => {
    const userId = c.get('userId');
    const categoryId = c.req.param('id');
    const data = c.req.valid('json');

    const existing = await prisma.category.findFirst({
      where: { id: categoryId, userId },
    });

    if (!existing) {
      return c.json({ error: 'Category not found' }, 404);
    }

    // Check for duplicate name (if changing)
    if (data.name && data.name !== existing.name) {
      const duplicate = await prisma.category.findFirst({
        where: { userId, name: data.name },
      });
      if (duplicate) {
        return c.json({ error: 'Category with this name already exists' }, 409);
      }
    }

    const category = await prisma.category.update({
      where: { id: categoryId },
      data,
    });

    return c.json({ category });
  }
);

// DELETE /api/categories/:id
categories.delete('/:id', async (c) => {
  const userId = c.get('userId');
  const categoryId = c.req.param('id');

  const existing = await prisma.category.findFirst({
    where: { id: categoryId, userId },
  });

  if (!existing) {
    return c.json({ error: 'Category not found' }, 404);
  }

  await prisma.category.delete({
    where: { id: categoryId },
  });

  return c.json({ message: 'Category deleted successfully' });
});

export default categories;
```
