---
type: "WARNING"
title: "Common API Mistakes to Avoid"
---

**1. Forgetting owner checks**
```typescript
// NEVER do this for user-owned resources
const task = await prisma.task.delete({
  where: { id: taskId }, // Missing userId check!
});
```

**2. Leaking sensitive data**
```typescript
// DON'T return password hash
const user = await prisma.user.findUnique({
  where: { id: userId },
}); // Includes passwordHash!

// DO use select to limit fields
const user = await prisma.user.findUnique({
  where: { id: userId },
  select: { id: true, email: true, name: true },
});
```

**3. Not validating foreign keys**
```typescript
// DON'T trust client-provided categoryId
await prisma.task.create({
  data: { categoryId: userInput }, // Could be another user's category!
});

// DO verify ownership first
const category = await prisma.category.findFirst({
  where: { id: userInput, userId },
});
if (!category) return c.json({ error: 'Category not found' }, 404);
```

**4. Inconsistent error responses**
Always use the same structure:
```typescript
{ error: 'Message here' }  // For errors
{ task: {...} }            // For success
```