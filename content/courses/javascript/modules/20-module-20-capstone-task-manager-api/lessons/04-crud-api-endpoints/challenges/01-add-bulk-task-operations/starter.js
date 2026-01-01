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
    // Your implementation here
  }
);

// PUT /api/tasks/bulk/status - Update status of multiple tasks
tasks.put(
  '/bulk/status',
  zValidator('json', bulkStatusSchema),
  async (c) => {
    // Your implementation here
  }
);