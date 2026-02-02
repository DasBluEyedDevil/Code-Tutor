---
type: "EXAMPLE"
title: "Task Types"
---

Define task-related types for the full-stack app:

```typescript
// packages/shared/src/types/task.ts
import { z } from 'zod';

export type TaskStatus = 'pending' | 'in_progress' | 'completed';
export type TaskPriority = 'low' | 'medium' | 'high';

export interface Task {
  id: string;
  title: string;
  description: string | null;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate: string | null; // ISO date string
  categoryId: string | null;
  userId: string;
  createdAt: string;
  updatedAt: string;
}

export interface TaskWithCategory extends Task {
  category: Category | null;
}

export interface Category {
  id: string;
  name: string;
  color: string; // Hex color like #3B82F6
  userId: string;
  createdAt: string;
  updatedAt: string;
}

// Query filters
export interface TaskFilters {
  status?: TaskStatus;
  categoryId?: string;
  priority?: TaskPriority;
  search?: string; // Search in title/description
  page?: number;
  limit?: number;
}

// Validation schemas
export const createTaskSchema = z.object({
  title: z.string().min(1).max(200),
  description: z.string().max(1000).optional(),
  priority: z.enum(['low', 'medium', 'high']).default('medium'),
  categoryId: z.string().optional(),
  dueDate: z.string().datetime().optional(),
});

export const updateTaskSchema = createTaskSchema.partial();

export const createCategorySchema = z.object({
  name: z.string().min(1).max(50),
  color: z.string().regex(/^#[0-9A-F]{6}$/i),
});

export type CreateTaskInput = z.infer<typeof createTaskSchema>;
export type UpdateTaskInput = z.infer<typeof updateTaskSchema>;
export type CreateCategoryInput = z.infer<typeof createCategorySchema>;
```
