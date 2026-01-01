---
type: "CODE"
title: "Define Validation Schema"
---

Create shared Zod schema:

```typescript
import { z } from 'zod';

export const createTaskSchema = z.object({
  title: z.string().min(1, 'Required').max(200),
  description: z.string().max(2000).optional(),
  status: z.enum(['pending', 'in_progress', 'completed']).default('pending'),
  priority: z.enum(['low', 'medium', 'high']).default('medium'),
  dueDate: z.coerce.date().optional(),
  categoryId: z.string().optional(),
});

export type CreateTaskInput = z.infer<typeof createTaskSchema>;
```
