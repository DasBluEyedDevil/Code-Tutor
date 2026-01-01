---
type: "THEORY"
title: "Loading States and Optimistic Updates"
---

A responsive UI requires proper loading states and, for better user experience, optimistic updates that show changes immediately before server confirmation.

Loading States:

```jsx
// src/components/common/LoadingSpinner.jsx
export default function LoadingSpinner({ size = 'medium', className = '' }) {
  const sizeClasses = {
    small: 'w-4 h-4',
    medium: 'w-8 h-8',
    large: 'w-12 h-12',
  };

  return (
    <div className={`flex justify-center items-center ${className}`}>
      <div
        className={`${sizeClasses[size]} border-4 border-blue-200 border-t-blue-600 rounded-full animate-spin`}
      />
    </div>
  );
}

// Button with loading state
export function LoadingButton({ loading, children, ...props }) {
  return (
    <button disabled={loading} {...props}>
      {loading ? (
        <span className="flex items-center gap-2">
          <LoadingSpinner size="small" />
          Processing...
        </span>
      ) : (
        children
      )}
    </button>
  );
}
```

Skeleton Loading for Lists:

```jsx
// src/components/common/TaskSkeleton.jsx
export default function TaskSkeleton() {
  return (
    <div className="border rounded-lg p-4 animate-pulse">
      <div className="h-4 bg-gray-200 rounded w-3/4 mb-2" />
      <div className="h-3 bg-gray-200 rounded w-1/2 mb-4" />
      <div className="flex justify-between">
        <div className="h-3 bg-gray-200 rounded w-1/4" />
        <div className="h-3 bg-gray-200 rounded w-1/6" />
      </div>
    </div>
  );
}

// Usage
{loading ? (
  <div className="grid gap-4">
    {[1, 2, 3].map(i => <TaskSkeleton key={i} />)}
  </div>
) : (
  <TaskList tasks={tasks} />
)}
```

Optimistic Updates:
Optimistic updates immediately reflect user actions in the UI, then sync with the server. If the server request fails, we roll back.

```jsx
async function handleToggleComplete(task) {
  // Optimistically update UI immediately
  const previousTasks = tasks;
  const optimisticStatus = task.status === 'COMPLETED' ? 'PENDING' : 'COMPLETED';
  
  setTasks(prev => prev.map(t => 
    t.id === task.id ? { ...t, status: optimisticStatus } : t
  ));

  try {
    // Actually update on server
    await taskService.toggleComplete(task.id, task.status);
  } catch (err) {
    // Rollback on failure
    setTasks(previousTasks);
    setError('Failed to update task status');
  }
}

async function handleDelete(taskId) {
  if (!window.confirm('Delete this task?')) return;

  // Optimistically remove from UI
  const previousTasks = tasks;
  setTasks(prev => prev.filter(t => t.id !== taskId));

  try {
    await taskService.deleteTask(taskId);
    // Success - no action needed, UI already updated
  } catch (err) {
    // Rollback on failure
    setTasks(previousTasks);
    setError('Failed to delete task');
  }
}
```

Optimistic updates make your application feel instant. The key is always saving the previous state so you can rollback on error. This pattern is especially effective for toggle operations and deletions where the user expects immediate feedback.