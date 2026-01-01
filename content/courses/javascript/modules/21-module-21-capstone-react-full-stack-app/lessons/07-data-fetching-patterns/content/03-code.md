---
type: "CODE"
title: "Mutations with Optimistic Updates"
---

Update data instantly with automatic rollback on error:

```typescript
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '../lib/api';

export function useUpdateTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ taskId, updates }: any) =>
      apiClient.request('PUT', `/api/tasks/${taskId}`, updates),

    onMutate: async ({ taskId, updates }) => {
      // Cancel pending queries
      await queryClient.cancelQueries({ queryKey: ['tasks'] });
      
      // Snapshot old data
      const oldTasks = queryClient.getQueryData(['tasks']);
      
      // Optimistically update cache
      queryClient.setQueryData(['tasks'], (old: any) =>
        old?.map((t: any) => t.id === taskId ? { ...t, ...updates } : t)
      );
      
      return { oldTasks };
    },

    onError: (err, vars, context: any) => {
      // Rollback on error
      queryClient.setQueryData(['tasks'], context.oldTasks);
    },

    onSuccess: () => {
      // Revalidate with server
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },
  });
}
```
