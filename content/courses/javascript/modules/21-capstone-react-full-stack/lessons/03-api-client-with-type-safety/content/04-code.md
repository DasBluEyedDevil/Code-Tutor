---
type: "EXAMPLE"
title: "Using the API Client in Components"
---

Example React component using the type-safe API client:

```typescript
// packages/web/src/pages/TasksPage.tsx
import React, { useState } from 'react';
import { useTasks, useCreateTask } from '../hooks/use-tasks';
import { TaskFilters } from '@app/shared';

export function TasksPage() {
  const [filters, setFilters] = useState<TaskFilters>({
    page: 1,
    limit: 10,
  });

  // Fetch tasks with type safety
  const { data, isLoading, error } = useTasks(filters);

  // Create task mutation
  const createTask = useCreateTask();

  const handleCreateTask = async (title: string) => {
    try {
      await createTask.mutateAsync({
        title,
        priority: 'medium',
      });
      // Success handled in hook
    } catch (err) {
      // Error handled in hook
    }
  };

  const handleFilterChange = (newFilters: Partial<TaskFilters>) => {
    setFilters((prev) => ({ ...prev, ...newFilters }));
  };

  if (isLoading) return <div>Loading...</div>;
  if (error instanceof Error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h1>My Tasks</h1>

      {/* Type-safe task list - data.items is guaranteed to be Task[] */}
      {data?.items.map((task) => (
        <div key={task.id}>
          <h3>{task.title}</h3>
          <p>{task.description}</p>
          <p>Status: {task.status}</p>
          <p>Priority: {task.priority}</p>
          {task.category && <p>Category: {task.category.name}</p>}
        </div>
      ))}

      {/* Pagination info - all fields typed */}
      <p>
        Page {data?.page} of {data?.totalPages}
      </p>

      {/* Mutation state handling */}
      {createTask.isLoading && <p>Creating task...</p>}
      {createTask.error && (
        <p>Error: {(createTask.error as Error).message}</p>
      )}
    </div>
  );
}
```
