// Add to src/routes/tasks.ts

const bulkCreateSchema = z.object({
  tasks: z.array(createTaskSchema).min(1).max(50),
});

const bulkStatusSchema = z.object({
  taskIds: z.array(z.string().cuid()).min(1).max(50),
  status: taskStatusEnum,
});

// POST /api/tasks/bulk - Create multiple tasks
tasks.post(
  '/bulk',
  zValidator('json', bulkCreateSchema),
  async (c) => {
    const userId = c.get('userId');
    const { tasks: taskInputs } = c.req.valid('json');

    // Collect all unique categoryIds
    const categoryIds = [...new Set(
      taskInputs
        .map(t => t.categoryId)
        .filter((id): id is string => id !== undefined)
    )];

    // Verify all categories belong to user
    if (categoryIds.length > 0) {
      const validCategories = await prisma.category.findMany({
        where: { id: { in: categoryIds }, userId },
        select: { id: true },
      });

      const validIds = new Set(validCategories.map(c => c.id));
      const invalidId = categoryIds.find(id => !validIds.has(id));

      if (invalidId) {
        return c.json({ error: `Category ${invalidId} not found` }, 404);
      }
    }

    // Create all tasks in a transaction
    const created = await prisma.task.createMany({
      data: taskInputs.map(task => ({ ...task, userId })),
    });

    return c.json({
      message: `Created ${created.count} tasks`,
      count: created.count,
    }, 201);
  }
);

// PUT /api/tasks/bulk/status - Update status of multiple tasks
tasks.put(
  '/bulk/status',
  zValidator('json', bulkStatusSchema),
  async (c) => {
    const userId = c.get('userId');
    const { taskIds, status } = c.req.valid('json');

    // Update only tasks owned by user
    const result = await prisma.task.updateMany({
      where: {
        id: { in: taskIds },
        userId, // Owner check!
      },
      data: { status },
    });

    return c.json({
      message: `Updated ${result.count} tasks`,
      count: result.count,
    });
  }
);