---
type: "CODE"
title: "Build Form Component"
---

Create form with React Hook Form:

```typescript
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { createTaskSchema } from '../schemas/task';
import { useCreateTask } from '../hooks/useCreateTask';

function TaskForm({ onSuccess }: { onSuccess?: () => void }) {
  const { mutate, isPending } = useCreateTask();
  const { register, handleSubmit, formState: { errors }, setError } = useForm({
    resolver: zodResolver(createTaskSchema),
    mode: 'onBlur',
  });

  const onSubmit = (data: CreateTaskInput) => {
    mutate(data, {
      onSuccess: () => onSuccess?.(),
      onError: (error: any) => {
        if (error.details) {
          Object.entries(error.details).forEach(([field, msg]) => {
            setError(field as keyof CreateTaskInput, {
              type: 'server',
              message: String(msg),
            });
          });
        }
      },
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <label>Title</label>
        <input {...register('title')} />
        {errors.title && <p className="text-red-600">{errors.title.message}</p>}
      </div>

      <div>
        <label>Priority</label>
        <select {...register('priority')}>
          <option value="low">Low</option>
          <option value="medium">Medium</option>
          <option value="high">High</option>
        </select>
      </div>

      <button type="submit" disabled={isPending}>
        {isPending ? 'Creating...' : 'Create'}
      </button>
    </form>
  );
}
```
