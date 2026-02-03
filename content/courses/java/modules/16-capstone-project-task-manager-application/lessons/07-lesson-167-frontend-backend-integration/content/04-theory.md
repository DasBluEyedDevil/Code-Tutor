---
type: "THEORY"
title: "React Path: Full CRUD Operations and Error Handling"
---

REACT PATH (continued)

Connecting all CRUD operations from the React UI to the backend API, with proper loading states and error handling.

```jsx
// src/pages/TasksPage.jsx
import { useState, useEffect, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { taskService } from '../services/taskService';

export default function TasksPage() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const loadTasks = useCallback(async () => {
    try {
      setLoading(true);
      const data = await taskService.getTasks();
      setTasks(data.content);
    } catch (err) {
      setError('Failed to load tasks');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => { loadTasks(); }, [loadTasks]);

  async function handleDelete(taskId) {
    if (!window.confirm('Delete this task?')) return;
    const previous = tasks;
    setTasks(prev => prev.filter(t => t.id !== taskId));
    try {
      await taskService.deleteTask(taskId);
    } catch (err) {
      setTasks(previous);
      setError('Failed to delete task');
    }
  }

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">My Tasks</h1>
        <Link to="/tasks/new" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          + New Task
        </Link>
      </div>
      {error && <div className="bg-red-100 text-red-700 p-3 rounded">{error}</div>}
      {loading ? <div>Loading...</div> : (
        tasks.length === 0
          ? <p className="text-center text-gray-500 py-12">No tasks yet.</p>
          : <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
              {tasks.map(task => (
                <div key={task.id} className="bg-white rounded-lg shadow p-4">
                  <h3 className="font-semibold">{task.title}</h3>
                  <p className="text-sm text-gray-500">{task.status} - {task.priority}</p>
                  <div className="flex gap-2 mt-3">
                    <Link to={`/tasks/${task.id}/edit`} className="text-blue-600 text-sm hover:underline">Edit</Link>
                    <button onClick={() => handleDelete(task.id)} className="text-red-600 text-sm hover:underline">Delete</button>
                  </div>
                </div>
              ))}
            </div>
      )}
    </div>
  );
}
```

Centralized Error Handling:
```javascript
// src/utils/errorHandler.js
export function getErrorMessage(error) {
  if (!error.response) return 'Unable to connect to server.';
  switch (error.response.status) {
    case 400: return error.response.data.detail || 'Invalid request.';
    case 401: return 'Session expired. Please log in again.';
    case 403: return 'You do not have permission for this action.';
    case 404: return 'Resource not found.';
    case 422: return error.response.data.detail || 'Validation failed.';
    default: return 'An unexpected error occurred.';
  }
}
```

Optimistic Updates:
Notice the delete handler uses optimistic updates -- the task is removed from the UI immediately, and if the API call fails, the previous state is restored. This makes the application feel instant.
