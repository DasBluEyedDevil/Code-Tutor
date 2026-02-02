---
type: "EXAMPLE"
title: "Task Routes - Get, Update, Delete"
---

Implement single-task operations with owner verification:

```typescript
// Continue in src/routes/tasks.ts

// GET /api/tasks/:id - Get single task
tasks.get('/:id', async (c) => {
  const userId = c.get('userId');
  const taskId = c.req.param('id');

  const task = await prisma.task.findFirst({
    where: { id: taskId, userId }, // Owner check!
    include: {
      category: true,
    },
  });

  if (!task) {
    return c.json({ error: 'Task not found' }, 404);
  }

  return c.json({ task });
});

// PUT /api/tasks/:id - Update task
tasks.put(
  '/:id',
  zValidator('json', updateTaskSchema),
  async (c) => {
    const userId = c.get('userId');
    const taskId = c.req.param('id');
    const data = c.req.valid('json');

    // Verify task exists and belongs to user
    const existing = await prisma.task.findFirst({
      where: { id: taskId, userId },
    });

    if (!existing) {
      return c.json({ error: 'Task not found' }, 404);
    }

    // Verify new category belongs to user (if changing)
    if (data.categoryId && data.categoryId !== existing.categoryId) {
      const category = await prisma.category.findFirst({
        where: { id: data.categoryId, userId },
      });
      if (!category) {
        return c.json({ error: 'Category not found' }, 404);
      }
    }

    const task = await prisma.task.update({
      where: { id: taskId },
      data,
      include: {
        category: {
          select: { id: true, name: true, color: true },
        },
      },
    });

    return c.json({ task });
  }
);

// DELETE /api/tasks/:id - Delete task
tasks.delete('/:id', async (c) => {
  const userId = c.get('userId');
  const taskId = c.req.param('id');

  // Verify task exists and belongs to user
  const existing = await prisma.task.findFirst({
    where: { id: taskId, userId },
  });

  if (!existing) {
    return c.json({ error: 'Task not found' }, 404);
  }

  await prisma.task.delete({
    where: { id: taskId },
  });

  return c.json({ message: 'Task deleted successfully' });
});

export default tasks;
```
