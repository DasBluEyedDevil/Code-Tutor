---
type: "EXAMPLE"
title: "React Query Integration"
---

Create custom hooks using React Query for data fetching and caching:

```typescript
// packages/web/src/hooks/use-tasks.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient, ApiError } from '../lib/api-client';
import { Task, TaskFilters, CreateTaskInput, UpdateTaskInput } from '@app/shared';

// Fetch all tasks with filters
export function useTasks(filters?: TaskFilters) {
  return useQuery(
    ['tasks', filters],
    () => apiClient.getTasks(filters),
    {
      staleTime: 5 * 60 * 1000, // 5 minutes
      cacheTime: 10 * 60 * 1000, // 10 minutes
      retry: 1,
      onError: (error: ApiError) => {
        console.error('Failed to fetch tasks:', error.message);
      },
    }
  );
}

// Fetch single task
export function useTask(id: string | null) {
  return useQuery(
    ['task', id],
    () => (id ? apiClient.getTask(id) : null),
    {
      enabled: !!id,
      staleTime: 5 * 60 * 1000,
    }
  );
}

// Create task mutation
export function useCreateTask() {
  const queryClient = useQueryClient();

  return useMutation(
    (input: CreateTaskInput) => apiClient.createTask(input),
    {
      onSuccess: (newTask) => {
        // Invalidate tasks list to refetch
        queryClient.invalidateQueries('tasks');
        // Optionally add to cache
        queryClient.setQueryData(['task', newTask.id], newTask);
      },
      onError: (error: ApiError) => {
        console.error('Failed to create task:', error.message);
      },
    }
  );
}

// Update task mutation
export function useUpdateTask() {
  const queryClient = useQueryClient();

  return useMutation(
    ({ id, input }: { id: string; input: UpdateTaskInput }) =>
      apiClient.updateTask(id, input),
    {
      onSuccess: (updatedTask) => {
        queryClient.invalidateQueries('tasks');
        queryClient.setQueryData(['task', updatedTask.id], updatedTask);
      },
      onError: (error: ApiError) => {
        console.error('Failed to update task:', error.message);
      },
    }
  );
}

// Delete task mutation
export function useDeleteTask() {
  const queryClient = useQueryClient();

  return useMutation(
    (id: string) => apiClient.deleteTask(id),
    {
      onSuccess: (_, id) => {
        queryClient.invalidateQueries('tasks');
        queryClient.removeQueries(['task', id]);
      },
      onError: (error: ApiError) => {
        console.error('Failed to delete task:', error.message);
      },
    }
  );
}
```
