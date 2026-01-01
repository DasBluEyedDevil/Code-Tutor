---
type: "CODE"
title: "Task Routes - List and Create"
---

Implement GET (list) and POST (create) endpoints:

```typescript
// src/routes/tasks.ts
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { prisma } from '../lib/db';
import { authMiddleware } from '../middleware/auth';
import {
  createTaskSchema,
  updateTaskSchema,
  taskQuerySchema,
} from '../schemas/task';

const tasks = new Hono();

// All routes require authentication
tasks.use('*', authMiddleware);

// GET /api/tasks - List tasks with filtering and pagination
tasks.get(
  '/',
  zValidator('query', taskQuerySchema),
  async (c) => {
    const userId = c.get('userId');
    const { status, priority, categoryId, search, page, limit } = c.req.valid('query');

    const skip = (page - 1) * limit;

    // Build where clause dynamically
    const where = {
      userId,
      ...(status && { status }),
      ...(priority && { priority }),
      ...(categoryId && { categoryId }),
      ...(search && {
        OR: [
          { title: { contains: search } },
          { description: { contains: search } },
        ],
      }),
    };

    // Execute queries in parallel
    const [tasks, total] = await Promise.all([
      prisma.task.findMany({
        where,
        include: {
          category: {
            select: { id: true, name: true, color: true },
          },
        },
        orderBy: [
          { priority: 'desc' },
          { dueDate: 'asc' },
          { createdAt: 'desc' },
        ],
        skip,
        take: limit,
      }),
      prisma.task.count({ where }),
    ]);

    return c.json({
      tasks,
      pagination: {
        page,
        limit,
        total,
        totalPages: Math.ceil(total / limit),
      },
    });
  }
);

// POST /api/tasks - Create task
tasks.post(
  '/',
  zValidator('json', createTaskSchema),
  async (c) => {
    const userId = c.get('userId');
    const data = c.req.valid('json');

    // Verify category belongs to user (if provided)
    if (data.categoryId) {
      const category = await prisma.category.findFirst({
        where: { id: data.categoryId, userId },
      });
      if (!category) {
        return c.json({ error: 'Category not found' }, 404);
      }
    }

    const task = await prisma.task.create({
      data: {
        ...data,
        userId,
      },
      include: {
        category: {
          select: { id: true, name: true, color: true },
        },
      },
    });

    return c.json({ task }, 201);
  }
);

export default tasks;
```
