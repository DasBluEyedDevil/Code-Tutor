---
type: "CODE"
title: "TaskList Component"
---

Create the main TaskList component that manages a list of tasks with TypeScript typing:

```typescript
import React, { useState, useEffect } from 'react';

interface Task {
  id: string;
  title: string;
  description: string;
  completed: boolean;
  createdAt: string;
}

interface TaskListProps {
  userId: string;
  onTaskUpdate?: (task: Task) => void;
}

const TaskList: React.FC<TaskListProps> = ({ userId, onTaskUpdate }) => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Fetch tasks on component mount and when userId changes
  useEffect(() => {
    const fetchTasks = async () => {
      try {
        setLoading(true);
        setError(null);
        const response = await fetch(`/api/tasks?userId=${userId}`, {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        
        if (!response.ok) {
          throw new Error(`Failed to fetch tasks: ${response.statusText}`);
        }
        
        const data = await response.json();
        setTasks(data.tasks);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Unknown error');
      } finally {
        setLoading(false);
      }
    };

    fetchTasks();
  }, [userId]);

  const handleToggleTask = async (taskId: string) => {
    const task = tasks.find(t => t.id === taskId);
    if (!task) return;

    try {
      const response = await fetch(`/api/tasks/${taskId}`, {
        method: 'PATCH',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        },
        body: JSON.stringify({ completed: !task.completed })
      });

      if (!response.ok) {
        throw new Error('Failed to update task');
      }

      const updatedTask: Task = await response.json();
      setTasks(tasks.map(t => t.id === taskId ? updatedTask : t));
      onTaskUpdate?.(updatedTask);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unknown error');
    }
  };

  const handleDeleteTask = async (taskId: string) => {
    try {
      const response = await fetch(`/api/tasks/${taskId}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      });

      if (!response.ok) {
        throw new Error('Failed to delete task');
      }

      setTasks(tasks.filter(t => t.id !== taskId));
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Unknown error');
    }
  };

  if (loading) {
    return <div className="task-list-loading">Loading tasks...</div>;
  }

  if (error) {
    return <div className="task-list-error">Error: {error}</div>;
  }

  return (
    <div className="task-list">
      <h2>Your Tasks ({tasks.length})</h2>
      {tasks.length === 0 ? (
        <p className="no-tasks">No tasks yet. Create one to get started!</p>
      ) : (
        <ul className="tasks">
          {tasks.map(task => (
            <li key={task.id}>
              <div className="task-item">
                <input
                  type="checkbox"
                  checked={task.completed}
                  onChange={() => handleToggleTask(task.id)}
                  className="task-checkbox"
                />
                <div className="task-content">
                  <h3 className={task.completed ? 'completed' : ''}>
                    {task.title}
                  </h3>
                  <p>{task.description}</p>
                  <small>{new Date(task.createdAt).toLocaleDateString()}</small>
                </div>
                <button
                  onClick={() => handleDeleteTask(task.id)}
                  className="delete-btn"
                >
                  Delete
                </button>
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default TaskList;
```
