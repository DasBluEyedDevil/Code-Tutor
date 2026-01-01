---
type: "CODE"
title: "Validation Schemas"
---

Centralized schemas for request validation on both frontend and backend:

```typescript
// packages/shared/src/schemas/validation.ts
import { z } from 'zod';

// Re-export all schemas for convenience
export { registerInputSchema, loginInputSchema } from '../types/user';
export { createTaskSchema, updateTaskSchema, createCategorySchema } from '../types/task';

// Task filter schema (used by frontend before sending to backend)
export const taskFiltersSchema = z.object({
  status: z.enum(['pending', 'in_progress', 'completed']).optional(),
  categoryId: z.string().optional(),
  priority: z.enum(['low', 'medium', 'high']).optional(),
  search: z.string().max(100).optional(),
  page: z.number().int().positive().default(1),
  limit: z.number().int().min(1).max(100).default(10),
});

export type TaskFiltersInput = z.infer<typeof taskFiltersSchema>;
```
