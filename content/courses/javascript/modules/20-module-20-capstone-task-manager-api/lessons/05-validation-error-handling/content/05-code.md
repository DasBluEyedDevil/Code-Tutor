---
type: "CODE"
title: "Integrating @hono/zod-validator"
---

Use @hono/zod-validator for automatic schema validation with error handling:

```typescript
// Example in src/routes/tasks.ts
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { z } from 'zod';
import { ValidationError } from '../lib/errors';

const tasks = new Hono();

const createTaskSchema = z.object({
  title: z
    .string()
    .min(1, 'Title is required')
    .max(200, 'Title too long'),
  description: z.string().optional(),
  status: z.enum(['pending', 'in_progress', 'completed']).default('pending'),
});

// zValidator automatically returns 400 with error details if validation fails
tasks.post(
  '/',
  zValidator('json', createTaskSchema, (result, c) => {
    if (!result.success) {
      // Custom error handling for validation failures
      const errors = result.error.flatten().fieldErrors;
      throw new ValidationError('Validation failed', errors);
    }
  }),
  async (c) => {
    const data = c.req.valid('json');
    // Data is now guaranteed to match the schema
    return c.json({ success: true, data });
  }
);

export default tasks;
```
