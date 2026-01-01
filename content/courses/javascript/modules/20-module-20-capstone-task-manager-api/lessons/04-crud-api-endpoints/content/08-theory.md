---
type: "THEORY"
title: "Owner-Only Access Pattern"
---

Notice how every database query includes `userId` in the where clause:

```typescript
// WRONG - Allows access to any user's data!
const task = await prisma.task.findUnique({
  where: { id: taskId },
});

// RIGHT - Only returns data owned by the authenticated user
const task = await prisma.task.findFirst({
  where: { id: taskId, userId },
});
```

**Why findFirst instead of findUnique?**
- `findUnique` only works with unique constraints
- `id` alone is unique, but we need to check `id + userId`
- `findFirst` allows multiple conditions

**Defense in Depth:**
1. JWT verifies the user is authenticated
2. Middleware extracts userId from token
3. Every query filters by userId
4. Even if an attacker guesses a valid task ID, they can't access it