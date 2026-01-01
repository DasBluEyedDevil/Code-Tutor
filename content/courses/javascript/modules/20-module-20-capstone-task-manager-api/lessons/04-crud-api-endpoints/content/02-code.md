---
type: "CODE"
title: "Task Validation Schemas"
---

Create Zod schemas for task operations:

```typescript
// src/schemas/task.ts
import { z } from 'zod';

export const taskStatusEnum = z.enum(['pending', 'in_progress', 'completed']);
export const taskPriorityEnum = z.enum(['low', 'medium', 'high']);

export const createTaskSchema = z.object({
  title: z
    .string()
    .min(1, 'Title is required')
    .max(200, 'Title too long')
    .trim(),
  description: z
    .string()
    .max(2000, 'Description too long')
    .trim()
    .optional(),
  status: taskStatusEnum.default('pending'),
  priority: taskPriorityEnum.default('medium'),
  dueDate: z
    .string()
    .datetime()
    .optional()
    .transform((val) => (val ? new Date(val) : undefined)),
  categoryId: z.string().cuid().optional(),
});

export const updateTaskSchema = createTaskSchema.partial();

export const taskQuerySchema = z.object({
  status: taskStatusEnum.optional(),
  priority: taskPriorityEnum.optional(),
  categoryId: z.string().cuid().optional(),
  search: z.string().optional(),
  page: z.coerce.number().int().positive().default(1),
  limit: z.coerce.number().int().min(1).max(100).default(20),
});

export type CreateTaskInput = z.infer<typeof createTaskSchema>;
export type UpdateTaskInput = z.infer<typeof updateTaskSchema>;
export type TaskQuery = z.infer<typeof taskQuerySchema>;
```
