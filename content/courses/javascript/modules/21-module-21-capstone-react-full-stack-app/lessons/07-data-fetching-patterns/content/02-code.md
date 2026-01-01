---
type: "CODE"
title: "Fetch with useQuery Hook"
---

Load and cache data from your API:

```typescript
import { useQuery } from '@tanstack/react-query';
import { apiClient } from '../lib/api';

export function useTasks(status?: string) {
  return useQuery({
    queryKey: ['tasks', { status }], // Cache key includes filters
    queryFn: async () => {
      const response = await apiClient.request('GET', '/api/tasks', {
        params: { status },
      });
      return response.tasks;
    },
    staleTime: 1000 * 60 * 5, // 5 minutes
    gcTime: 1000 * 60 * 10, // 10 minutes
  });
}

// In your component:
function TasksList() {
  const { data: tasks, isLoading, error } = useTasks('pending');

  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error: {(error as Error).message}</div>;

  return (
    <ul>
      {tasks?.map(task => (
        <li key={task.id}>{task.title}</li>
      ))}
    </ul>
  );
}
```
